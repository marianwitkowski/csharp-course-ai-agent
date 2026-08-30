# AKTUALIZACJE — co się zmieniło między .NET Framework a .NET 10

> **Po co ten plik?** Uczeń, który wpisze w wyszukiwarkę „C# tablica przykład", trafi w większości przypadków na materiał z lat 2010-2020: .NET Framework, Visual Studio, `class Program { static void Main }`, `Newtonsoft.Json`. Ten kod **nadal działa**, ale nie jest tym, co pisze się dzisiaj — a początkujący nie ma jak tego odróżnić.
>
> Ten plik jest ściągą dla agenta: co uczeń może zobaczyć w starym poradniku, co robimy zamiast tego i jak to wytłumaczyć w jednym zdaniu.

> **Zasada nadrzędna:** uczeń poznaje **najpierw** dzisiejszy sposób. Stare formy pokazuj wyłącznie jako „spotkasz to w cudzym kodzie, to znaczy tyle a tyle" — nigdy jako to, czego ma używać. Nie rób z tego lekcji historii; jedno zdanie i wracacie do tematu.

---

## `[ogólne]` — dwie różne platformy o podobnych nazwach

| Stare materiały | Dzisiaj |
| --- | --- |
| **.NET Framework** (do 4.8) — tylko Windows, wersja przypisana do systemu | **.NET** (10) — macOS, Linux, Windows, instalowany jak zwykły program |
| Visual Studio jako jedyna droga | `dotnet` w terminalu + dowolny edytor |
| Solucja (`.sln`) + projekt (`.csproj`) zanim napiszesz pierwszą linię | Plik `.cs` uruchamiany wprost |

**Jak to powiedzieć uczniowi (raz, w lekcji 1.1):**
> „Do 2020 roku C# działał praktycznie tylko na Windows i wymagał wielkiego programu o nazwie Visual Studio. Dziś działa wszędzie i wystarczy jeden plik. Jeśli trafisz na poradnik, który każe ci zakładać »solucję« — to poradnik z tamtego świata."

**Nie rozwijaj tego.** Uczeń nie musi rozumieć różnicy między .NET Framework, .NET Core i .NET 5+. Musi tylko wiedzieć, że stary poradnik może wyglądać inaczej i to nie jego wina.

---

## `[moduł 1]` — program bez obudowy

**Stare materiały pokazują:**
```csharp
using System;

namespace MojaAplikacja
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Cześć");
        }
    }
}
```

**Dzisiaj wystarczy:**
```csharp
Console.WriteLine("Cześć");
```

Zmieniły się trzy rzeczy naraz:

1. **Instrukcje najwyższego poziomu** (C# 9) — klasę i `Main` pisze kompilator za ciebie
2. **Domyślne `using`** (C# 10) — `using System;` i kilkanaście innych jest dodawanych automatycznie
3. **Aplikacje jednoplikowe** (.NET 10) — `dotnet run plik.cs` bez żadnego projektu

**Jedno zdanie dla ucznia:** „To ta sama rzecz rozpisana w pełnej formie. Twoja wersja jest krótsza i robi dokładnie to samo. Klasę zobaczysz w module 8, gdy będzie po co."

**Uwaga:** `string[] args` nadal działa w instrukcjach najwyższego poziomu — zmienna `args` jest dostępna sama z siebie. Wraca w lekcji 12.4.

---

## `[moduł 2]` — typy i konwersje

| Stare materiały | Dzisiaj | Od |
| --- | --- | --- |
| `int liczba = 5;` wszędzie | `var liczba = 5;` tam, gdzie typ widać po prawej | C# 3 |
| `Convert.ToInt32(tekst)` | `int.Parse` / `int.TryParse` | zawsze |
| `int wynik; if (int.TryParse(s, out wynik))` | `if (int.TryParse(s, out int wynik))` — deklaracja w miejscu | C# 7 |
| `new Dictionary<string, int>()` | `Dictionary<string, int> d = new();` | C# 9 |

**`var` — co powiedzieć:** to nie jest „typ dynamiczny". Zmienna nadal ma typ ustalony na stałe; kompilator odczytuje go z wartości po prawej. `var x = 5;` i `int x = 5;` dają identyczny program.

**Kiedy `var`, a kiedy nazwa typu?** Reguła dla ucznia: `var`, gdy typ widać w tej samej linii (`var lista = new List<int>();`), pełna nazwa, gdy nie widać (`int wiek = int.Parse(wpisane);`). To konwencja, nie wymóg kompilatora.

---

## `[moduł 3]` — składanie tekstu

| Stare materiały | Dzisiaj |
| --- | --- |
| `"Cześć, " + imie + "! Masz " + wiek + " lat."` | `$"Cześć, {imie}! Masz {wiek} lat."` |
| `string.Format("Cześć, {0}!", imie)` | jw. |
| `"C:\\Users\\Anna\\plik.txt"` | `@"C:\Users\Anna\plik.txt"` |

**Interpolacja (`$"..."`)** to C# 6, ale materiały z 2020 często jej nie używają. Ucz **wyłącznie** interpolacji — sklejanie `+` pokazujesz raz, w module 2, żeby uczeń zobaczył pułapkę `"30" + 5` dającą `"305"`.

**Formatowanie w interpolacji** działa od razu: `$"{cena:F2}"` → dwie cyfry po przecinku, `$"{procent:P0}"` → procent, `$"{data:yyyy-MM-dd}"` → data.

### Wczytywanie danych od użytkownika

| Stare materiały | Dzisiaj |
| --- | --- |
| `Convert.ToInt32(Console.ReadLine())` | `int.TryParse(Console.ReadLine(), out int x)` |
| `int.Parse(Console.ReadLine())` | jw. |

`Convert.ToInt32` i `Parse` zachowują się tak samo: przy tekście, który nie jest liczbą, **przerywają program** wyjątkiem `FormatException`. Na danych wpisywanych z klawiatury to kwestia czasu, nie ryzyka. Kurs używa wyłącznie `TryParse`.

**Ostrzeżenie `CS8600` przy `string x = Console.ReadLine();`** jest nowe — mechanizm ostrzegania o wartościach, których może nie być, włączono domyślnie dopiero w nowszych projektach. W poradniku z 2018 roku tego ostrzeżenia nie zobaczysz, choć kod jest identyczny.

Uczniowi wystarczy: **`warning` to nie `error`, program się buduje i działa.** Nie tłumacz `null`, `string?` ani `??` — kurs świadomie ich nie ma (patrz `INDEX.md`). Ostrzeżenie znika samo, gdy wstawi się `Console.ReadLine()` wprost do `TryParse`, bez zmiennej pośredniej.

---

## `[moduł 4]` — `switch` w dwóch postaciach

**Stare materiały znają tylko instrukcję:**
```csharp
switch (ocena)
{
    case 5:
        opis = "bardzo dobry";
        break;
    default:
        opis = "nieznana";
        break;
}
```

**Od C# 8 jest też wyrażenie** — krótsze i bez `break`:
```csharp
string opis = ocena switch
{
    5 => "bardzo dobry",
    4 => "dobry",
    _ => "nieznana",
};
```

**Kolejność w lekcji 4.3:** najpierw instrukcja (uczeń musi zrozumieć `case` i `break`, bo spotka je w każdym cudzym kodzie), potem wyrażenie jako „to samo, krócej, gdy chodzi tylko o przypisanie wartości".

`_` to przypadek domyślny — odpowiednik `default`.

---

## `[moduł 6]` — kolekcje

| Stare materiały | Dzisiaj | Od |
| --- | --- | --- |
| `ArrayList`, `Hashtable` | `List<T>`, `Dictionary<K,V>` | C# 2 (!) |
| `tablica[tablica.Length - 1]` | `tablica[^1]` — indeks od końca | C# 8 |
| pętla kopiująca fragment | `tablica[2..5]` — zakres | C# 8 |
| `new List<int> { 1, 2, 3 }` | `List<int> lista = [1, 2, 3];` | C# 12 |
| `if (slownik.ContainsKey(k)) { var v = slownik[k]; }` | `if (slownik.TryGetValue(k, out var v))` | zawsze warto |

**`ArrayList` jest przestarzały od dwudziestu lat**, ale wciąż występuje w starych poradnikach. Jeśli uczeń go przyniesie: „to poprzednik `List<int>`, który nie pilnował typu — dlatego go zastąpiono".

**Kolekcje z nawiasami kwadratowymi (`[1, 2, 3]`)** wprowadzaj dopiero po klasycznym zapisie. Uczeń musi rozumieć, że tworzy listę, zanim skróci zapis.

**`TryGetValue` zamiast `ContainsKey` + indeksowanie** — nie tylko krócej, ale jedno wyszukiwanie zamiast dwóch. Warto to powiedzieć, bo uczy myślenia o kosztach.

---

## `[moduł 8]` — klasy

| Stare materiały | Dzisiaj | Od |
| --- | --- | --- |
| pole prywatne + ręczne `GetImie()` / `SetImie()` | właściwość `public string Imie { get; set; }` | C# 3 |
| konstruktor przypisujący pola linia po linii | konstruktor podstawowy: `class Kot(string imie)` | C# 12 |
| `public string Imie { get; set; }` bez wartości | `public string Imie { get; set; } = "";` | C# 6 |

**Konstruktory podstawowe wprowadzaj po klasycznych**, nie zamiast. Uczeń, który nie napisał ani razu zwykłego konstruktora, nie zrozumie, co skrót skraca.

**Właściwości a pola** to jedno z niewielu miejsc, gdzie warto poświęcić czas: różnica wygląda kosmetycznie, a nie jest. Lekcja 8.3 jest o tym w całości.

---

## `[moduł 9]` — gdzie się podziało `static`

W materiałach sprzed 2021 roku **każdy** program zaczynał się od:
```csharp
class Program
{
    static void Main(string[] args) { ... }
}
```

Uczeń widział `static` w pierwszej minucie nauki, nie mając szans go zrozumieć — i zwykle zapamiętywał jako „słowo, które trzeba napisać, żeby działało".

Dziś instrukcje najwyższego poziomu usuwają to z drogi: `static` pojawia się dopiero w lekcji 9.4, gdy uczeń sam potrzebuje składowej wspólnej dla wszystkich obiektów.

**Jedno zdanie dla ucznia:** „W starych przykładach `static` stoi przy `Main`, bo cały program był jedną metodą w klasie. Tobie nie jest tam potrzebne."

Reszta modułu 9 — dziedziczenie, `virtual`/`override`, klasy abstrakcyjne — **nie zmieniła się** od dwudziestu lat. Stary poradnik o dziedziczeniu jest w porządku.

---

## `[moduł 11]` — wyjątki i `using`

| Stare materiały | Dzisiaj | Od |
| --- | --- | --- |
| `using (var czytnik = new StreamReader(...)) { ... }` — blok z klamrami | `using var czytnik = new StreamReader(...);` — deklaracja | C# 8 |
| `catch (Exception ex) { }` — połykanie wszystkiego | konkretny typ wyjątku, np. `catch (FileNotFoundException)` | zawsze |

**Deklaracja `using` bez klamer** jest krótsza i mniej zagnieżdża kod. Ale **pokaż najpierw wersję z blokiem** — widać w niej wprost, gdzie kończy się „życie" zasobu.

---

## `[moduł 12]` — pliki i JSON

| Stare materiały | Dzisiaj |
| --- | --- |
| `Newtonsoft.Json` (pakiet z NuGet) | `System.Text.Json` — wbudowany, bez instalacji |
| `JsonConvert.SerializeObject(obiekt)` | `JsonSerializer.Serialize(obiekt)` |
| `JsonConvert.DeserializeObject<T>(tekst)` | `JsonSerializer.Deserialize<T>(tekst)` |

**To najczęstsza pułapka w tym module.** Prawie każdy poradnik JSON-a w C# używa `Newtonsoft.Json`, bo przez lata nie było alternatywy. Dziś jest wbudowana i kurs używa wyłącznie jej — żadnych pakietów do zainstalowania.

Jeśli uczeń przyniesie kod z `JsonConvert`: „to biblioteka zewnętrzna, kiedyś konieczna. Dziś .NET ma to w środku — `JsonSerializer`."

**Domyślne zachowanie `System.Text.Json` różni się w dwóch miejscach**, które zaskakują:
- nie serializuje pól, tylko **właściwości** (kolejny powód, żeby moduł 8 był solidny)
- domyślnie **rozróżnia wielkość liter** przy odczycie nazw

`Newtonsoft.Json` zachowywał się w obu punktach odwrotnie — stąd kod przepisany ze starego poradnika potrafi cicho wczytać puste właściwości.

**Trzecia różnica: polskie znaki.** `System.Text.Json` domyślnie zapisuje je kodami (`"Żółw"` → `"\u017B\u00F3\u0142w"`). Plik jest poprawny i każdy program odczyta z niego z powrotem »Żółw«, ale człowiek tego nie przeczyta. Naprawa: `Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping` w `JsonSerializerOptions` (`using System.Text.Encodings.Web`). Nazwa „Unsafe" dotyczy wklejania takiego tekstu wprost na stronę WWW, nie zapisu do pliku.

### Dwie dyrektywy, których nie ma w żadnym poradniku

To **nie jest** różnica względem 2020 roku — to konsekwencja aplikacji jednoplikowych, czyli nowości .NET 10. Poradniki jeszcze o tym nie piszą, więc uczeń nigdzie tego nie znajdzie.

Plik uruchamiany przez `dotnet run plik.cs` ma domyślnie włączone ustawienia pod budowanie do samodzielnego pliku wykonywalnego. Skutek:

```csharp
#:property JsonSerializerIsReflectionEnabledByDefault=true
#:property PublishAot=false
```

- **bez pierwszej linii** program przewraca się: `InvalidOperationException: Reflection-based serialization has been disabled for this application`
- **bez drugiej** przed wynikiem wypisuje się kilkanaście ostrzeżeń `IL2026` i `IL3050` — nieszkodliwych, ale przy pierwszym uruchomieniu wyglądających jak awaria (znikają przy kolejnym, bo plik nie jest budowany od nowa)

Obie linie muszą stać **na samej górze pliku**, przed `using`.

**W projekcie z modułu 14 żadna nie jest potrzebna** — `dotnet new console` nie ustawia `PublishAot`. Wyjątkiem jest projekt powstały przez `dotnet project convert`: konwersja przenosi ustawienia pliku jednoplikowego i JSON wywraca się tak samo. Wtedy usuwa się linię `PublishAot` z `.csproj`.

---

## `[moduł 14]` — projekt, testy, dystrybucja

| Stare materiały | Dzisiaj |
| --- | --- |
| Nowy projekt przez kreator w Visual Studio | `dotnet new console -o nazwa` |
| MSTest / NUnit z konfiguracją w GUI | `dotnet new xunit`, `dotnet test` |
| `packages.config`, folder `packages/` | `<PackageReference>` w `.csproj`, bez folderu w repozytorium |
| Instalator albo folder z kilkunastoma plikami `.dll` | `dotnet publish` — katalog z kilkoma plikami albo, na żądanie, jeden plik wykonywalny |

**`dotnet publish` ma trzy warianty** i warto ich nie mylić — lekcja 14.3 pokazuje dwa pierwsze:

| Komenda | Co powstaje | Wymaga .NET u odbiorcy |
| --- | --- | --- |
| `dotnet publish -c Release` | katalog, 5 plików, ok. 150 kB | **tak** |
| `... -r <system> --self-contained` | katalog, kilkadziesiąt MB | nie |
| `... -r <system> --self-contained -p:PublishSingleFile=true` | **jeden plik**, ok. 76 MB (plus `.pdb`) | nie |

Identyfikator systemu dobiera się do **odbiorcy**, nie do siebie: `win-x64`, `linux-x64`, `osx-arm64`, `osx-x64`.

**Pułapka pierwszego wariantu:** program zbudował się, ale `./nazwa` kończy się komunikatem „Download the .NET runtime", jeśli uczeń instalował .NET skryptem `dotnet-install.sh` — czyli do katalogu `~/.dotnet`, a nie systemowo. Działa wtedy `dotnet nazwa.dll` albo `DOTNET_ROOT=$HOME/.dotnet ./nazwa`.

**`PublishSingleFile` pokazuj tylko na pytanie.** Puenta kursu — „oddajesz komuś jeden plik i ten ktoś go uruchamia" — jest prawdziwa, ale wymaga wszystkich trzech przełączników naraz.

---

## Rzeczy, które się **nie** zmieniły

Warto to uczniowi powiedzieć, gdy zaczyna nie ufać starym materiałom:

- Składnia `if`, `for`, `while`, `foreach` — identyczna od dwudziestu lat
- `Console.WriteLine`, `Console.ReadLine` — bez zmian
- Klasy, dziedziczenie, `virtual` / `override`, interfejsy — bez zmian
- `try` / `catch` / `finally` — bez zmian
- Nazewnictwo: `PascalCase` dla typów i metod, `camelCase` dla zmiennych — bez zmian
- LINQ (`Where`, `Select`, `OrderBy`) — bez zmian od C# 3

**Czyli:** stary poradnik o pętlach jest w porządku. Stary poradnik o tworzeniu projektu, JSON-ie albo strukturze pliku — nie.

---

## Czego kurs celowo nie używa, choć jest nowe

Nowość nie jest wartością sama w sobie. Poniższe konstrukcje są dzisiejsze i poprawne, ale w kursie dla początkującego przeszkadzają:

| Konstrukcja | Czemu nie |
| --- | --- |
| `record` | Wygląda jak klasa, zachowuje się inaczej. Dwa modele naraz w module 8 to o jeden za dużo |
| Dopasowywanie wzorców (`is { Wiek: > 18 }`) | Zwięzłe dla kogoś, kto już czyta C#. Dla początkującego nieczytelne |
| Typy nullowalne (`string?`) jako temat | Uczeń **zobaczy** ostrzeżenia `CS8600`; wyjaśnij je jednym zdaniem („to może nie mieć wartości — sprawdź"), ale nie rób z tego lekcji |
| `async` / `await` | Bez sieci i plików o dużym rozmiarze nie ma problemu, który to rozwiązuje |
| Wyrażenia kolekcji `[..]` przed modułem 6 | Skrót zapisu czegoś, czego uczeń jeszcze nie zna |
| Instrukcje najwyższego poziomu **w projekcie** z modułu 14 | Tu akurat ich używamy — ale świadomie, po tym jak uczeń zobaczył `Main` w module 8 |
