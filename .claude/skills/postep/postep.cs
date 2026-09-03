// Narzędzie postep — atomowe operacje na postep/student.json.
//
// Helper agenta csharp-tutor. Uczeń go nie uruchamia i nie czyta.
//
// Uruchomienie (z dowolnego miejsca w repozytorium kursu):
//
//     dotnet run .claude/skills/postep/postep.cs -- <komenda> [argumenty]
//
// To aplikacja jednoplikowa (.NET 10) — bez projektu, bez .csproj, bez
// zależności spoza biblioteki standardowej. Artefakty budowania trafiają do
// pamięci podręcznej SDK, nie do repozytorium.
//
// Każda operacja modyfikująca wykonuje ten sam protokół:
//   1. Wczytuje student.json (lub tworzy domyślny przy `init`).
//   2. Sprawdza wersję schematu.
//   3. Backupuje obecny plik do postep/backups/student.{TS}.json.
//   4. Modyfikuje strukturę w pamięci.
//   5. Zapisuje do postep/student.json.tmp.
//   6. Waliduje (ponowne parsowanie z dysku).
//   7. Atomowo podmienia .tmp → student.json.
//
// Model stanu trzymamy jako JsonNode, a nie jako klasę z polami. Dzięki temu
// pola zapisane przez nowszą wersję narzędzia przechodzą przez odczyt i zapis
// nietknięte — starsza wersja nie skasuje stanu, którego nie rozumie.
//
// Komendy:
//
//   init --imie X --cel Y --tempo Z [--system S --dotnet-cmd C --dotnet-version V --shell SH --edytor E]
//   read [--field <sciezka.kropkowa>]
//   set --field <sciezka> --value <wartosc>
//   add-lekcja --id X.Y --trudnosc 1-5
//   add-cwiczenie --lekcja X.Y --poziom warmup|main|star|fix
//   add-mocna-strona "tekst"
//   add-do-powtorki --temat T --lekcja X.Y
//   review-do-powtorki --temat T --wynik ok|zle
//   due
//   remove-do-powtorki --temat T
//   update-srodowisko [--system S] [--dotnet-cmd C] [--dotnet-version V] [--shell SH] [--edytor E]
//   add-notatka "tekst"
//   end-session
//   recovery
//
// Schemat 2 (2026-09): pole `sciezka` ("pelna" | "skrocona") i harmonogram
// powtórek — każdy wpis `do_powtorki` ma `poziom` (0-4) i `next_review`.
// Plik w schemacie 1 jest migrowany w locie przy pierwszym zapisie.

using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;

const int WersjaSchematu = 2;
const int MaxMocnychStron = 7;
const int MaxNotatek = 40; // wpisy "parking:" muszą przeżyć kilka modułów

// Odstępy powtórek w dniach, indeksowane poziomem opanowania.
// ponytail: stała tablica zamiast SM-2 — wystarcza, dopóki nikt nie mierzy skuteczności.
int[] odstepyPowtorek = { 1, 3, 7, 14, 30 };

// UnrelaxedJsonEscaping: bez tego polskie znaki zapisałyby się jako ą.
// Plik ma być czytelny dla człowieka, nie wklejany do HTML-a.
var opcjeZapisu = new JsonSerializerOptions
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
};

var argumenty = new List<string>(args);

string? katalogZFlagi = null;
if (argumenty.Count >= 2 && (argumenty[0] == "-root" || argumenty[0] == "--root"))
{
    katalogZFlagi = argumenty[1];
    argumenty.RemoveRange(0, 2);
}

if (argumenty.Count == 0)
{
    Uzycie();
    return 2;
}

var komenda = argumenty[0];
var reszta = argumenty.Skip(1).ToArray();

var znaneKomendy = new[]
{
    "init", "read", "set", "add-lekcja", "add-cwiczenie", "add-mocna-strona",
    "add-do-powtorki", "review-do-powtorki", "due", "remove-do-powtorki",
    "update-srodowisko", "add-notatka", "end-session", "recovery",
};

if (!znaneKomendy.Contains(komenda))
{
    Console.Error.WriteLine($"BŁĄD: nieznana komenda \"{komenda}\"");
    Console.Error.WriteLine();
    Uzycie();
    return 2;
}

int kodWyjscia;
try
{
    var katalog = katalogZFlagi ?? ZnajdzKatalogGlowny();
    var plikStudent = Path.Combine(katalog, "postep", "student.json");
    var katalogBackupow = Path.Combine(katalog, "postep", "backups");
    var plikTmp = Path.Combine(katalog, "postep", "student.json.tmp");

    switch (komenda)
    {
        case "init": Init(reszta); break;
        case "read": Read(reszta); break;
        case "set": Set(reszta); break;
        case "add-lekcja": AddLekcja(reszta); break;
        case "add-cwiczenie": AddCwiczenie(reszta); break;
        case "add-mocna-strona": AddMocnaStrona(reszta); break;
        case "add-do-powtorki": AddDoPowtorki(reszta); break;
        case "review-do-powtorki": ReviewDoPowtorki(reszta); break;
        case "due": Due(); break;
        case "remove-do-powtorki": RemoveDoPowtorki(reszta); break;
        case "update-srodowisko": UpdateSrodowisko(reszta); break;
        case "add-notatka": AddNotatka(reszta); break;
        case "end-session": EndSession(); break;
        case "recovery": Recovery(); break;
    }

    kodWyjscia = 0;

    // ===== Komendy =====

    void Init(string[] a)
    {
        var f = Flagi(a, out _);
        foreach (var wymagana in new[] { "imie", "cel", "tempo" })
        {
            if (!f.ContainsKey(wymagana))
            {
                throw new InvalidOperationException($"brak wymaganego argumentu --{wymagana}");
            }
        }

        if (File.Exists(plikStudent))
        {
            throw new InvalidOperationException(
                $"{plikStudent} już istnieje. Aby zmienić pola, użyj `set` / `update-srodowisko`; " +
                "aby zacząć od nowa — skill `reset-kursu`");
        }

        var sciezka = f.GetValueOrDefault("sciezka", "pelna");
        if (sciezka is not ("pelna" or "skrocona"))
        {
            throw new InvalidOperationException(
                $"sciezka musi być pelna albo skrocona (dostałem \"{sciezka}\")");
        }

        var stan = new JsonObject
        {
            ["schema_version"] = WersjaSchematu,
            ["imie"] = f["imie"],
            ["cel"] = f["cel"],
            ["tempo_godz_tydz"] = f["tempo"],
            ["sciezka"] = sciezka,
            ["rozpoczeto"] = Dzisiaj(),
            ["ostatnia_sesja"] = Dzisiaj(),
            ["liczba_sesji"] = 1,
            ["aktualna_lekcja"] = "1.1",
            ["srodowisko"] = new JsonObject
            {
                ["system"] = f.GetValueOrDefault("system", ""),
                ["dotnet_cmd"] = f.GetValueOrDefault("dotnet-cmd", ""),
                ["dotnet_version"] = f.GetValueOrDefault("dotnet-version", ""),
                ["shell"] = f.GetValueOrDefault("shell", ""),
                ["edytor"] = f.GetValueOrDefault("edytor", ""),
            },
            ["ukonczone_lekcje"] = new JsonArray(),
            ["ukonczone_cwiczenia"] = new JsonArray(),
            ["mocne_strony"] = new JsonArray(),
            ["do_powtorki"] = new JsonArray(),
            ["notatki_tutora"] = new JsonArray(),
        };

        Directory.CreateDirectory(Path.GetDirectoryName(plikStudent)!);
        ZapiszAtomowo(stan); // init nie ma czego backupować
        Console.WriteLine($"OK: utworzono postep/student.json (schema v{WersjaSchematu})");
    }

    void Read(string[] a)
    {
        var f = Flagi(a, out _);
        var surowe = WczytajSurowo();

        // Bez --field oddajemy plik bajt w bajt — zachowuje kolejność pól
        // i formatowanie dokładnie takie, jakie jest na dysku.
        if (!f.TryGetValue("field", out var pole) || pole.Length == 0)
        {
            Console.Out.Write(surowe);
            return;
        }

        var stan = Sparsuj(surowe);
        var wartosc = PobierzSciezke(stan, pole);
        Console.WriteLine(wartosc is null ? "null" : wartosc.ToJsonString(opcjeZapisu));
    }

    void Set(string[] a)
    {
        var f = Flagi(a, out _);
        if (!f.TryGetValue("field", out var pole) || pole.Length == 0)
        {
            throw new InvalidOperationException("brak wymaganego argumentu --field");
        }
        var wartosc = f.GetValueOrDefault("value", "");

        var stan = WczytajStan();
        UstawSciezke(stan, pole, wartosc);
        Zapisz(stan);
        Console.WriteLine($"OK: {pole} = \"{wartosc}\"");
    }

    void AddLekcja(string[] a)
    {
        var f = Flagi(a, out _);
        if (!f.TryGetValue("id", out var id) || id.Length == 0)
        {
            throw new InvalidOperationException("brak wymaganego argumentu --id");
        }
        if (!f.TryGetValue("trudnosc", out var tekstTrudnosci)
            || !int.TryParse(tekstTrudnosci, out var trudnosc)
            || trudnosc < 1 || trudnosc > 5)
        {
            throw new InvalidOperationException("trudnosc musi być liczbą 1-5");
        }

        var stan = WczytajStan();
        var lekcje = Tablica(stan, "ukonczone_lekcje");

        // Upsert po id — chroni przed duplikatami przy powtórzeniu komendy.
        // Gdy uczeń powtarza lekcję, bierzemy najświeższą ocenę trudności.
        var istniejaca = lekcje.FirstOrDefault(w => w?["id"]?.GetValue<string>() == id);
        if (istniejaca is not null)
        {
            istniejaca["data"] = Dzisiaj();
            istniejaca["trudnosc_subiektywna"] = trudnosc;
            stan["ostatnia_sesja"] = Dzisiaj();
            Zapisz(stan);
            Console.WriteLine($"OK: zaktualizowano lekcję {id} (trudność {trudnosc})");
            return;
        }

        lekcje.Add((JsonNode)new JsonObject
        {
            ["id"] = id,
            ["data"] = Dzisiaj(),
            ["trudnosc_subiektywna"] = trudnosc,
        });
        stan["ostatnia_sesja"] = Dzisiaj();
        Zapisz(stan);
        Console.WriteLine($"OK: dopisano lekcję {id} (trudność {trudnosc})");
    }

    void AddCwiczenie(string[] a)
    {
        var f = Flagi(a, out _);
        if (!f.TryGetValue("lekcja", out var lekcja) || lekcja.Length == 0)
        {
            throw new InvalidOperationException("brak wymaganego argumentu --lekcja");
        }
        var poziom = f.GetValueOrDefault("poziom", "");
        if (poziom is not ("warmup" or "main" or "star" or "fix"))
        {
            throw new InvalidOperationException(
                $"poziom musi być warmup, main, star albo fix (dostałem \"{poziom}\")");
        }

        var stan = WczytajStan();
        var cwiczenia = Tablica(stan, "ukonczone_cwiczenia");

        // Upsert po (lekcja, poziom) — idempotentny przy powtórzeniu.
        var istniejace = cwiczenia.FirstOrDefault(w =>
            w?["lekcja"]?.GetValue<string>() == lekcja &&
            w?["poziom"]?.GetValue<string>() == poziom);
        if (istniejace is not null)
        {
            istniejace["data"] = Dzisiaj();
            Zapisz(stan);
            Console.WriteLine($"OK: zaktualizowano ćwiczenie {lekcja}/{poziom}");
            return;
        }

        cwiczenia.Add((JsonNode)new JsonObject
        {
            ["lekcja"] = lekcja,
            ["poziom"] = poziom,
            ["data"] = Dzisiaj(),
        });
        Zapisz(stan);
        Console.WriteLine($"OK: dopisano ćwiczenie {lekcja}/{poziom}");
    }

    void AddMocnaStrona(string[] a)
    {
        _ = Flagi(a, out var pozycyjne);
        var tekst = string.Join(" ", pozycyjne).Trim();
        if (tekst.Length == 0)
        {
            throw new InvalidOperationException("podaj tekst mocnej strony jako argument");
        }

        var stan = WczytajStan();
        var mocne = Tablica(stan, "mocne_strony");
        if (mocne.Any(w => w?.GetValue<string>() == tekst))
        {
            Console.WriteLine($"INFO: \"{tekst}\" już jest na liście — pomijam");
            return;
        }

        mocne.Add((JsonNode)JsonValue.Create(tekst));
        PrzytnijDoOstatnich(stan, "mocne_strony", MaxMocnychStron);
        Zapisz(stan);
        Console.WriteLine($"OK: dopisano mocną stronę: \"{tekst}\"");
    }

    void AddDoPowtorki(string[] a)
    {
        var f = Flagi(a, out _);
        if (!f.TryGetValue("temat", out var temat) || temat.Length == 0 ||
            !f.TryGetValue("lekcja", out var lekcja) || lekcja.Length == 0)
        {
            throw new InvalidOperationException("wymagane argumenty: --temat i --lekcja");
        }

        var stan = WczytajStan();
        var powtorki = Tablica(stan, "do_powtorki");
        if (powtorki.Any(w => w?["temat"]?.GetValue<string>() == temat))
        {
            Console.WriteLine($"INFO: temat \"{temat}\" już w do_powtorki — pomijam");
            return;
        }

        powtorki.Add((JsonNode)new JsonObject
        {
            ["temat"] = temat,
            ["lekcja"] = lekcja,
            ["data_zauwazenia"] = Dzisiaj(),
            ["poziom"] = 0,
            ["next_review"] = ZaDni(odstepyPowtorek[0]),
        });
        Zapisz(stan);
        Console.WriteLine($"OK: dopisano do_powtorki: {temat} (powtórka {ZaDni(odstepyPowtorek[0])})");
    }

    // Wynik powtórki przesuwa termin: ok → dłuższy odstęp, zle → od początku.
    // Po przejściu wszystkich odstępów temat znika z listy jako opanowany.
    void ReviewDoPowtorki(string[] a)
    {
        var f = Flagi(a, out _);
        if (!f.TryGetValue("temat", out var temat) || temat.Length == 0)
        {
            throw new InvalidOperationException("brak wymaganego argumentu --temat");
        }
        var wynik = f.GetValueOrDefault("wynik", "");
        if (wynik is not ("ok" or "zle"))
        {
            throw new InvalidOperationException($"wynik musi być ok albo zle (dostałem \"{wynik}\")");
        }

        var stan = WczytajStan();
        var powtorki = Tablica(stan, "do_powtorki");
        var wpis = powtorki.FirstOrDefault(w => w?["temat"]?.GetValue<string>() == temat) as JsonObject;
        if (wpis is null)
        {
            throw new InvalidOperationException($"tematu \"{temat}\" nie ma w do_powtorki");
        }

        var poziom = wpis["poziom"]?.GetValue<int>() ?? 0;
        if (wynik == "zle")
        {
            wpis["poziom"] = 0;
            wpis["next_review"] = ZaDni(odstepyPowtorek[0]);
            Zapisz(stan);
            Console.WriteLine($"OK: {temat} — od nowa, powtórka {wpis["next_review"]}");
            return;
        }

        poziom++;
        if (poziom >= odstepyPowtorek.Length)
        {
            powtorki.Remove(wpis);
            Zapisz(stan);
            Console.WriteLine($"OK: {temat} — opanowane po {odstepyPowtorek.Length} powtórkach, usunięto z do_powtorki");
            return;
        }

        wpis["poziom"] = poziom;
        wpis["next_review"] = ZaDni(odstepyPowtorek[poziom]);
        Zapisz(stan);
        Console.WriteLine($"OK: {temat} — poziom {poziom}, następna powtórka {wpis["next_review"]}");
    }

    // Tylko odczyt: tematy, których termin powtórki minął albo jest dziś.
    void Due()
    {
        var stan = WczytajStan();
        var dzisiaj = Dzisiaj();
        var zalegle = Tablica(stan, "do_powtorki")
            .Where(w => string.CompareOrdinal(w?["next_review"]?.GetValue<string>() ?? dzisiaj, dzisiaj) <= 0)
            .Select(w => w!.DeepClone())
            .ToArray();

        Console.WriteLine(new JsonArray(zalegle).ToJsonString(opcjeZapisu));
        Console.Error.WriteLine($"INFO: {zalegle.Length} tematów do powtórki na dziś ({dzisiaj})");
    }

    void RemoveDoPowtorki(string[] a)
    {
        var f = Flagi(a, out _);
        if (!f.TryGetValue("temat", out var temat) || temat.Length == 0)
        {
            throw new InvalidOperationException("brak wymaganego argumentu --temat");
        }

        var stan = WczytajStan();
        var powtorki = Tablica(stan, "do_powtorki");
        var zostaja = powtorki
            .Where(w => w?["temat"]?.GetValue<string>() != temat)
            .Select(w => w!.DeepClone())
            .ToArray();
        var usuniete = powtorki.Count - zostaja.Length;

        stan["do_powtorki"] = new JsonArray(zostaja);
        Zapisz(stan);
        Console.WriteLine($"OK: usunięto {usuniete} wpisów do_powtorki o temacie \"{temat}\"");
    }

    void UpdateSrodowisko(string[] a)
    {
        var f = Flagi(a, out _);

        // Aktualizujemy tylko pola faktycznie podane w wierszu poleceń —
        // brak flagi nie może wyzerować istniejącej wartości.
        var mapowanie = new Dictionary<string, string>
        {
            ["system"] = "system",
            ["dotnet-cmd"] = "dotnet_cmd",
            ["dotnet-version"] = "dotnet_version",
            ["shell"] = "shell",
            ["edytor"] = "edytor",
        };

        var stan = WczytajStan();
        if (stan["srodowisko"] is not JsonObject srodowisko)
        {
            srodowisko = new JsonObject();
            stan["srodowisko"] = srodowisko;
        }

        var zmienione = new List<string>();
        foreach (var (flaga, klucz) in mapowanie)
        {
            if (f.TryGetValue(flaga, out var wartosc))
            {
                srodowisko[klucz] = wartosc;
                zmienione.Add($"{klucz}=\"{wartosc}\"");
            }
        }

        if (zmienione.Count == 0)
        {
            throw new InvalidOperationException("nic do zaktualizowania — podaj co najmniej jedno pole");
        }

        zmienione.Sort(StringComparer.Ordinal);
        Zapisz(stan);
        Console.WriteLine($"OK: zaktualizowano środowisko: {string.Join(", ", zmienione)}");
    }

    void AddNotatka(string[] a)
    {
        _ = Flagi(a, out var pozycyjne);
        var tekst = string.Join(" ", pozycyjne).Trim();
        if (tekst.Length == 0)
        {
            throw new InvalidOperationException("podaj treść notatki jako argument");
        }

        var stan = WczytajStan();
        Tablica(stan, "notatki_tutora").Add((JsonNode)JsonValue.Create(tekst));
        PrzytnijDoOstatnich(stan, "notatki_tutora", MaxNotatek);
        Zapisz(stan);
        Console.WriteLine("OK: dopisano notatkę tutora");
    }

    void EndSession()
    {
        var stan = WczytajStan();
        stan["ostatnia_sesja"] = Dzisiaj();
        var sesje = stan["liczba_sesji"]?.GetValue<int>() ?? 0;
        stan["liczba_sesji"] = sesje + 1;
        Zapisz(stan);
        Console.WriteLine($"OK: zakończono sesję #{sesje + 1}");
    }

    void Recovery()
    {
        if (!Directory.Exists(katalogBackupow))
        {
            throw new InvalidOperationException("brak backupów do przywrócenia (postep/backups/ nie istnieje)");
        }

        // Znacznik czasu ma stałą szerokość, więc sortowanie po nazwie
        // malejąco daje najnowszy backup na początku.
        var backupy = Directory
            .GetFiles(katalogBackupow, "student.*.json")
            .OrderByDescending(Path.GetFileName, StringComparer.Ordinal)
            .ToArray();

        if (backupy.Length == 0)
        {
            throw new InvalidOperationException("brak backupów do przywrócenia (postep/backups/ jest puste)");
        }

        foreach (var backup in backupy)
        {
            string dane;
            JsonObject stan;
            try
            {
                dane = File.ReadAllText(backup);
                stan = Sparsuj(dane);
            }
            catch (Exception)
            {
                continue; // uszkodzony backup — próbujemy starszego
            }

            if (File.Exists(plikStudent))
            {
                var uszkodzony = Path.Combine(
                    Path.GetDirectoryName(plikStudent)!,
                    $"student.broken.{ZnacznikCzasu()}.json");
                File.Move(plikStudent, uszkodzony);
                Console.WriteLine($"INFO: stary plik przeniesiony do {Path.GetFileName(uszkodzony)}");
            }

            File.WriteAllText(plikStudent, dane);
            Console.WriteLine($"OK: przywrócono z {Path.GetFileName(backup)}");
            Console.WriteLine($"     imię: {LubZnak(stan["imie"]?.GetValue<string>())}");
            Console.WriteLine($"     aktualna lekcja: {LubZnak(stan["aktualna_lekcja"]?.GetValue<string>())}");
            Console.WriteLine($"     ukończonych lekcji: {(stan["ukonczone_lekcje"] as JsonArray)?.Count ?? 0}");
            return;
        }

        throw new InvalidOperationException("żaden z backupów nie parsuje się jako JSON");
    }

    // ===== Odczyt, zapis, backup =====

    string WczytajSurowo()
    {
        if (!File.Exists(plikStudent))
        {
            throw new InvalidOperationException($"{plikStudent} nie istnieje. Najpierw `init`");
        }
        return File.ReadAllText(plikStudent);
    }

    JsonObject WczytajStan()
    {
        var stan = Sparsuj(WczytajSurowo());

        var wersja = stan["schema_version"]?.GetValue<int>() ?? 0;
        if (wersja > WersjaSchematu)
        {
            throw new InvalidOperationException(
                $"schema_version={wersja} jest nowsza niż obsługiwana ({WersjaSchematu}). " +
                "Zaktualizuj narzędzie postep");
        }
        stan["schema_version"] = WersjaSchematu;

        // Migracja 1 → 2: brakujące pola dostają wartości domyślne. Wpisy
        // do_powtorki bez terminu są traktowane jako zaległe od dziś, żeby
        // stary stan ucznia od razu trafił do harmonogramu.
        if (stan["sciezka"] is null)
        {
            stan["sciezka"] = "pelna";
        }
        foreach (var wpis in Tablica(stan, "do_powtorki").OfType<JsonObject>())
        {
            wpis["poziom"] ??= 0;
            wpis["next_review"] ??= Dzisiaj();
        }

        return stan;
    }

    JsonObject Sparsuj(string dane)
    {
        try
        {
            return JsonNode.Parse(dane) as JsonObject
                ?? throw new InvalidOperationException("plik nie zawiera obiektu JSON");
        }
        catch (JsonException e)
        {
            throw new InvalidOperationException(
                $"{plikStudent} nie jest poprawnym JSON-em ({e.Message}). " +
                "Uruchom `recovery`, aby przywrócić z backupu");
        }
    }

    void Backup()
    {
        if (!File.Exists(plikStudent))
        {
            return; // nie ma czego backupować
        }
        Directory.CreateDirectory(katalogBackupow);
        File.Copy(plikStudent, Path.Combine(katalogBackupow, $"student.{ZnacznikCzasu()}.json"));
    }

    void ZapiszAtomowo(JsonObject stan)
    {
        File.WriteAllText(plikTmp, stan.ToJsonString(opcjeZapisu) + Environment.NewLine);

        // Walidacja: plik na dysku musi się parsować, zanim podmienimy oryginał.
        try
        {
            JsonNode.Parse(File.ReadAllText(plikTmp));
        }
        catch (JsonException e)
        {
            throw new InvalidOperationException(
                $"zapisany plik nie parsuje się ({e.Message}) — oryginał nietknięty");
        }

        File.Move(plikTmp, plikStudent, overwrite: true);
    }

    void Zapisz(JsonObject stan)
    {
        Backup();
        ZapiszAtomowo(stan);
    }

    // ===== Drobiazgi =====

    // Zwraca tablicę spod klucza, tworząc ją, jeśli jej nie ma. Chroni przed
    // plikiem po ręcznej edycji, w którym ktoś skasował pustą listę.
    JsonArray Tablica(JsonObject stan, string klucz)
    {
        if (stan[klucz] is JsonArray istniejaca)
        {
            return istniejaca;
        }
        var nowa = new JsonArray();
        stan[klucz] = nowa;
        return nowa;
    }

    void PrzytnijDoOstatnich(JsonObject stan, string klucz, int ile)
    {
        var tablica = Tablica(stan, klucz);
        if (tablica.Count <= ile)
        {
            return;
        }
        var ostatnie = tablica.Skip(tablica.Count - ile).Select(w => w!.DeepClone()).ToArray();
        stan[klucz] = new JsonArray(ostatnie);
    }
}
catch (Exception e)
{
    Console.Error.WriteLine($"BŁĄD: {e.Message}");
    kodWyjscia = 1;
}

return kodWyjscia;

// ===== Funkcje niezależne od ścieżek =====

// Szuka w górę katalogu zawierającego wiedza/ i .claude/.
//
// Rozpoznajemy projekt po katalogach, które są w repozytorium — a nie po
// postep/, bo ten powstaje dopiero przy pierwszym `init`. Na świeżym klonie
// jeszcze go nie ma i szukanie po nim nie znalazłoby niczego.
static string ZnajdzKatalogGlowny()
{
    var katalog = Directory.GetCurrentDirectory();
    while (true)
    {
        if (Directory.Exists(Path.Combine(katalog, "wiedza")) &&
            Directory.Exists(Path.Combine(katalog, ".claude")))
        {
            return katalog;
        }

        var rodzic = Path.GetDirectoryName(katalog);
        if (string.IsNullOrEmpty(rodzic) || rodzic == katalog)
        {
            throw new InvalidOperationException(
                "nie znalazłem katalogu głównego projektu (oczekiwane: wiedza/ + .claude/); podaj -root");
        }
        katalog = rodzic;
    }
}

// Rozdziela `--klucz wartosc` od argumentów pozycyjnych.
// Flaga bez wartości (na końcu albo przed kolejną flagą) dostaje "".
static Dictionary<string, string> Flagi(string[] a, out List<string> pozycyjne)
{
    var wynik = new Dictionary<string, string>(StringComparer.Ordinal);
    pozycyjne = new List<string>();

    for (var i = 0; i < a.Length; i++)
    {
        if (!a[i].StartsWith("--", StringComparison.Ordinal))
        {
            pozycyjne.Add(a[i]);
            continue;
        }

        var nazwa = a[i][2..];
        if (i + 1 < a.Length && !a[i + 1].StartsWith("--", StringComparison.Ordinal))
        {
            wynik[nazwa] = a[i + 1];
            i++;
        }
        else
        {
            wynik[nazwa] = "";
        }
    }

    return wynik;
}

static JsonNode? PobierzSciezke(JsonObject stan, string kropkowa)
{
    JsonNode? biezacy = stan;
    foreach (var czesc in kropkowa.Split('.'))
    {
        if (biezacy is not JsonObject obiekt)
        {
            throw new InvalidOperationException(
                $"pole \"{kropkowa}\" nie istnieje (fragment \"{czesc}\" nie jest obiektem)");
        }
        if (!obiekt.ContainsKey(czesc))
        {
            throw new InvalidOperationException(
                $"pole \"{kropkowa}\" nie istnieje (brak klucza \"{czesc}\")");
        }
        biezacy = obiekt[czesc];
    }
    return biezacy;
}

// `set` działa wyłącznie na polach tekstowych. Liczby (liczba_sesji) i listy
// mają własne komendy — celowo, żeby nie dało się zepsuć typu przez literówkę.
static void UstawSciezke(JsonObject stan, string kropkowa, string wartosc)
{
    var czesci = kropkowa.Split('.');
    var rodzic = stan;

    for (var i = 0; i < czesci.Length - 1; i++)
    {
        if (rodzic[czesci[i]] is not JsonObject nastepny)
        {
            throw new InvalidOperationException(
                $"pole \"{kropkowa}\" nie istnieje (fragment \"{czesci[i]}\" nie jest obiektem)");
        }
        rodzic = nastepny;
    }

    var ostatni = czesci[^1];
    if (!rodzic.ContainsKey(ostatni))
    {
        throw new InvalidOperationException(
            $"pole \"{kropkowa}\" nie istnieje (brak klucza \"{ostatni}\")");
    }
    if (rodzic[ostatni] is JsonValue biezaca && biezaca.GetValueKind() != JsonValueKind.String)
    {
        throw new InvalidOperationException(
            $"pole \"{kropkowa}\" nie jest tekstem — `set` działa tylko na polach tekstowych");
    }

    rodzic[ostatni] = wartosc;
}

static string Dzisiaj() => DateTime.Now.ToString("yyyy-MM-dd");

static string ZaDni(int dni) => DateTime.Now.AddDays(dni).ToString("yyyy-MM-dd");

// Mikrosekundy w nazwie backupu — bez nich szybka sekwencja komend
// (add-lekcja + set + end-session w tej samej sekundzie) nadpisałaby backup.
static string ZnacznikCzasu()
{
    var teraz = DateTime.Now;
    return $"{teraz:yyyy-MM-dd-HH-mm-ss}-{teraz.Ticks % 10_000_000 / 10:D6}";
}

static string LubZnak(string? s) => string.IsNullOrEmpty(s) ? "?" : s;

static void Uzycie()
{
    Console.Error.WriteLine(
        "użycie: dotnet run .claude/skills/postep/postep.cs -- [-root <katalog>] <komenda> [argumenty]");
    Console.Error.WriteLine();
    Console.Error.WriteLine("komendy:");
    foreach (var nazwa in new[]
             {
                 "add-cwiczenie", "add-do-powtorki", "add-lekcja", "add-mocna-strona",
                 "add-notatka", "due", "end-session", "init", "read", "recovery",
                 "remove-do-powtorki", "review-do-powtorki", "set", "update-srodowisko",
             })
    {
        Console.Error.WriteLine($"  {nazwa}");
    }
}
