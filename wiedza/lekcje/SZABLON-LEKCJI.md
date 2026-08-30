---
lekcja: X.Y
tytul: Krótki, konkretny tytuł
modul: NN-nazwa-modulu
przyklady: NN-plik.cs (jeśli w wiedza/przyklady/kod/ jest coś na temat)
aktualizacja: [moduł NN], [ogólne]
czas_min: 30-60
zalozenia: co uczeń musi już umieć (lekcje poprzedzające)
---

<!-- KONWENCJA KOMEND:
     Komendy pisane w wersji macOS/Linux. Różnice na Windows dotyczą wyłącznie
     powłoki (`type` zamiast `cat`, `dir` zamiast `ls`, ścieżki z `\`).
     Sama komenda `dotnet` jest identyczna na wszystkich systemach.
     Uczeń pracuje w `kurs/zadania/` — każde ćwiczenie to JEDEN plik `.cs`.
     Uruchamianie: `dotnet run NN-temat.cs` z katalogu `kurs/zadania`.
     AGENT NIGDY NIE URUCHAMIA KODU UCZNIA — uruchamia uczeń i wkleja wynik. -->

# Lekcja X.Y — Tytuł

## Cel
Po lekcji uczeń:
- konkret 1
- konkret 2
- konkret 3

## Krok 1 — Zakotwiczenie (3-5 min)
**Pytanie wejściowe:**
> „Coś z życia, co aktywuje intuicję — bez terminologii technicznej."

**Co chcesz usłyszeć:** krótkie naprowadzenie, co odpowiedź ma uchwycić.
**Czego NIE rób:** nie wprowadzaj jeszcze terminu technicznego.

## Krok 2 — Mostek (3-5 min)
**Mostek konceptualny:** „To, co opisałeś — [intuicja] — w C# nazywamy [termin]."

**Minimalny przykład (2-3 linijki — w C# z instrukcjami najwyższego poziomu
plik naprawdę może mieć trzy linijki, korzystaj z tego):**
```csharp
// najprostsze, co pokazuje koncept
```

**Pytania:**
- „Co tu jest [X]? Co [Y]?"
- „Co robi linia 2?"

## Krok 3 — Eksperyment (10-15 min)
Mini-zadania (uczeń sam pisze i uruchamia — `dotnet run NN-temat.cs`):
1. zadanie 1
2. zadanie 2
3. zadanie 3

**Po każdym pytaj:** „Co zobaczyłeś? Czy się spodziewałeś?"
**Gdy kompilator odmówi:** „Wklej dokładnie, co powiedział — razem z kodem `CSxxxx`" → czytacie komunikat razem.

## Krok 4 — Pogłębienie (10-15 min)
Wariacje i „co jeśli":
- „Co się stanie, gdy...?"
- „A jeśli zamiast X dasz Y?"

**Typowe błędy do sprowokowania:**
- konkretny kod `CSxxxx`, np. `CS0029` (niezgodność typów)
- wynik zaskakujący, ale bez błędu — np. dzielenie całkowite dające `0`

> Najcenniejszy moment lekcji to ten, w którym program **działa**, ale wypisuje
> coś innego, niż uczeń zakładał. Zaplanuj przynajmniej jeden taki moment.

## Krok 5 — Ćwiczenie
→ Wywołaj skill `cwiczenie`. Sugerowany kontekst:
- temat: [konkretny temat]
- elementy do użycia: [...]

Propozycje (agent może wziąć wprost):
- 🔥 [rozgrzewka]
- ⭐ [główne]
- ⚡ [gwiazdka]

## Pułapki
- Pułapka 1 — opis krótko + jak ją rozpoznać
- Pułapka 2

## Aktualizacja 2026 (z `AKTUALIZACJE.md`)
Co uczeń może zobaczyć w starym poradniku i co robimy zamiast tego:
- punkt 1 (z sekcji `[moduł NN]`)
- punkt 2

**Kolejność:** najpierw dzisiejszy sposób. Stara forma tylko jako „spotkasz to
w cudzym kodzie, to znaczy tyle a tyle" — jedno zdanie, bez rozwijania.

## Notatki tutora
- Jeśli uczeń mówi X → reaguj Y
- Częsta dygresja, której unikać
- Co powiedzieć, gdy uczeń wydaje się znudzony / przytłoczony

## Po lekcji
- Notatka w `kurs/lekcje/X.Y-temat.md` (3-5 zdań o tym, co zostało zrozumiane + 1 pułapka)
- `postep`: dopisz lekcję `X.Y` do `ukonczone_lekcje` z subiektywną trudnością (1-5)
- `aktualna_lekcja` ustaw na kolejną
- Jeśli uczeń się potknął → wpis do `do_powtorki`

---

# Checklista — zanim uznasz lekcję za gotową

> **Skąd ta lista.** Przegląd pierwszych sześciu napisanych lekcji wykrył **21 usterek**. Żadna nie była przypadkiem: wszystkie należały do siedmiu klas poniżej i wszystkie powtarzały się między lekcjami. Ta lista istnieje, żeby wyłapać je **w trakcie pisania**, a nie po fakcie — sprawdzanie 40 gotowych lekcji jest wielokrotnie droższe.
>
> Przechodź ją po napisaniu lekcji, punkt po punkcie. To nie jest lista życzeń, tylko rejestr błędów, które już popełniono.

## 1. Każde twierdzenie o zachowaniu — uruchomione, nie zgadnięte

**Najgroźniejsza klasa. Wystąpiła 5 razy, raz odwracając sens całego ćwiczenia.**

Dotyczy **wszystkiego**, co lekcja obiecuje, że uczeń zobaczy: kod błędu `CSxxxx`, treść komunikatu, wypisany wynik, liczba zgłoszonych błędów, zachowanie przy danych brzegowych.

```sh
# Do katalogu roboczego, nie do repozytorium:
cd "$SCRATCH" && printf '<kod z lekcji>\n' > t.cs
dotnet build t.cs 2>&1 | grep "error CS"     # komunikaty kompilatora
dotnet run t.cs                               # wynik działania
```

Przyłapane przypadki:
- ćwiczenie twierdziło, że liczba błędów **spada** przy naprawianiu — rośnie (błąd składni zasłania resztę)
- eksperyment z `char` miał dać komunikat o niezgodności typów — daje `CS1012` o długości literału
- „`3.14` **niekoniecznie** zadziała" — zadziała twardym `false`
- pułapka podawała komunikat, którego .NET nie wypisuje

**Zasada:** jeśli nie widziałeś tego wyjścia na ekranie w tej sesji, nie wpisuj go do lekcji.

## 2. Zero konstrukcji spoza dotychczasowego materiału

**Wystąpiła 4 razy, w tym raz w samej lekcji o niewyprzedzaniu programu.**

Sprawdź w `INDEX.md`, co uczeń już miał. Potem:

```sh
grep -nE 'if \(|for \(|while \(|foreach|\$"|List<|\.Parse|TryParse|ReadLine|class |new ' <plik lekcji>
```

Każde trafienie musi być albo (a) w materiale, albo (b) w notatce **zakazującej** użycia. Nic pośredniego — adnotacja „przepisz, wyjaśnimy później" to porażka, nie rozwiązanie.

> **Spodziewaj się szumu.** Na lekcji, której tematem jest jedna z tych konstrukcji, trafień będzie kilkanaście — lekcja 2.3 daje ich 26, bo cała jest o `Parse` i `TryParse`. To normalne. Grep jest znajdywaczem, nie sędzią: przeglądasz trafienia i szukasz tych, które **nie są tematem tej lekcji**. Nie odrzucaj listy dlatego, że jest długa.

**Zanim uznasz konstrukcję za nieuniknioną, poszukaj obejścia.** `TryParse` wyglądał na niemożliwy bez `if` — a wystarczyło przypisać zwracany `bool` do zmiennej. Wersja bez `if` okazała się dydaktycznie **lepsza**, bo `bool` przestał być schowany w warunku.

## 3. Odwołania do innych lekcji — sprawdzone w INDEX.md

**Wystąpiła 2 razy.** Lekcja zapowiadała temat „na lekcję 5.1", gdy jest on sercem lekcji 2.4.

```sh
grep -nE 'lekcj[ięa] [0-9]|moduł[uie]* [0-9]' <plik lekcji>
```

Każdy numer skonfrontuj z `INDEX.md`. Numer wstecz — sprawdź, czy tamta lekcja faktycznie to zawiera. Numer w przód — sprawdź, czy tamta lekcja to obejmuje i czy nie jest bliżej/dalej, niż piszesz.

## 4. Plik przykładowy zgodny z lekcją

**Wystąpiła 2 razy — przeoczona w trzech wcześniejszych przeglądach, bo skanowałem tylko `wiedza/lekcje/`.**

Plik z `wiedza/przyklady/kod/` to kod, który uczeń **uruchamia**. Jeśli lekcja pokazuje jeden zapis, a plik obok drugi, uczeń widzi sprzeczność.

```sh
grep -nE '\$"|if \(|TryParse|foreach|List<' wiedza/przyklady/kod/*.cs
for f in wiedza/przyklady/kod/*.cs; do dotnet build "$f" >/dev/null 2>&1 || echo "NIE KOMPILUJE: $f"; done
```

Po **każdej** zmianie w lekcji sprawdź, czy jej plik przykładowy nadal jest spójny.

## 5. Bez porównań do innych języków

**Wystąpiła 2 razy** — mimo że zakaz stoi w definicji agenta.

```sh
grep -nE 'w innych języka|w niektórych języka|innym języku|jak w [A-Z]' <plik lekcji>
```

Uczeń nie zna żadnego innego języka. „W niektórych językach `true` to `1`" nie niesie mu żadnej informacji, a sugeruje, że powinien coś wiedzieć. Pisz wprost o C#.

## 6. Treść lekcji, nie zapis twoich rozważań

**Wystąpiła raz** i przeszłaby prosto do ucznia:

> „Niech spróbuje `if (a = b)` — nie, jeszcze nie zna `if`. Zamiast tego:"

Przeczytaj lekcję tak, jakbyś ją miał czytać na głos. Zdania, w których się wahasz, poprawiasz albo tłumaczysz sam sobie, mają zniknąć — decyzja ma być podjęta, nie opisana.

## 7. Po każdej zmianie — przejrzyj resztę pliku

**Wystąpiła 4 razy naraz, po jednej przeróbce kroku 3.**

Zmiana w kroku środkowym unieważnia zwykle: sekcję **Pułapki**, **Notatki tutora**, treść **ćwiczeń** (odsyłają do wzorca z lekcji) i **frontmatter**. Po każdej większej edycji przeczytaj plik od początku, nie tylko zmieniony fragment.

## 8. Metody biblioteki standardowej też są materiałem

Punkt 2 łapie konstrukcje **języka** (`foreach`, `List<>`, `try`). Nie łapie **metod**, a te wyciekają równie łatwo — `ToUpper`, `StartsWith`, `AddRange`, `Substring`, `Math.Min` potrafią pojawić się w ćwiczeniu jako coś oczywistego, choć żadna lekcja ich nie pokazywała.

Reguła: metoda spoza wcześniejszych lekcji jest dopuszczalna **tylko** wtedy, gdy jest jawnie oznaczona jako do sprawdzenia w dokumentacji („kurs jej nie pokazywał — zajrzyj, co zwraca"). Nigdy jako odsyłacz do lekcji, w której jej nie ma.

## 9. Jeden język cytowanych komunikatów w obrębie lekcji

.NET tłumaczy komunikaty kompilatora na język systemu — uczeń z polskim systemem zobaczy `CS0029` po polsku, uczeń z angielskim po angielsku. **Kod `CSxxxx` jest ten sam i to on jest treścią.**

W jednej lekcji nie mieszaj: albo wszystkie cytaty po polsku, albo wszystkie po angielsku. Gdy cytujesz wersję angielską, a uczeń pracuje po polsku (albo odwrotnie), powiedz mu jednym zdaniem, że treść się zgadza mimo innego brzmienia.

## 10. Ćwiczenia sprawdzasz tak samo jak kroki lekcji

Punkty 1-9 dotyczą scenariusza. **Treści zadań z kroku 5 podlegają im wszystkim tak samo** — to tam najłatwiej o błąd, bo autor pisze je z pamięci, na końcu, patrząc na wersję kodu, którą ma w głowie, a nie na tę, którą uczeń zobaczy na ekranie.

Dla każdego z trzech poziomów sprawdź:

- **Da się to rozwiązać wyłącznie materiałem do tej lekcji włącznie?** Rozpisz w myślach rozwiązanie i wypisz użyte konstrukcje oraz metody. Każda musi być wcześniej wprowadzona.
- **Jeśli nie da się — czy to celowa ściana i czy jest oznaczona?** Kurs świadomie stawia zadania, których uczeń nie domknie (motywacja do następnej lekcji), ale **musi to być napisane wprost**: „tak ma być, brakuje ci klocka z lekcji N". Nieoznaczona ściana to nie ćwiczenie, tylko cicha porażka ucznia.
- **Czy zapowiedziany wynik ćwiczenia zgadza się z kodem, który uczeń faktycznie napisze?** Jeśli zadanie opiera się na klasie z kroku 3, weź tę klasę w wersji z kroku 3 — nie w tej, którą pamiętasz.
- **Czy podsumowanie pod listą ćwiczeń wymienia tylko to, co w nich naprawdę jest?** Zdanie „gwiazdka łączy X, Y i Z" bywa pisane przed ostateczną wersją zadania i zostaje niezgodne z treścią.

## Ostatni przebieg — komendy zbiorcze

```sh
L=wiedza/lekcje/NN.MM-temat.md
grep -nE 'if \(|for \(|while \(|foreach|\$"|List<|\.Parse|TryParse|ReadLine' "$L"
grep -nE '\.[A-Z][a-zA-Z]+\(' "$L"        # metody biblioteki — każdą sprawdź, czy była (punkt 8)
grep -nE 'lekcj[ięa] [0-9]|moduł[uie]* [0-9]' "$L"
grep -nE 'w innych języka|w niektórych języka|innym języku' "$L"
grep -nE 'error CS|warning CS' "$L"        # jeden język w całej lekcji (punkt 9)
head -9 "$L"     # frontmatter: zalozenia, przyklady, aktualizacja, czas_min — aktualne?
```

Sprawdź jeszcze, czy pole `zalozenia` we frontmatterze wymienia **wszystkie** lekcje, do których treść się odwołuje — rozjazd między nim a `grep`-iem odsyłaczy to najczęstsza usterka porządkowa.

Lekcja jest gotowa, gdy wszystkie dziewięć punktów przechodzi, a każdy wynik pokazany uczniowi widziałeś na własnym ekranie.
