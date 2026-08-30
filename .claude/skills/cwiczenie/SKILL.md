---
name: cwiczenie
description: Generuje 1-3 ćwiczenia w C# do samodzielnego rozwiązania przez ucznia, dopasowane do bieżącej lekcji i poziomu trudności. Trzy poziomy: rozgrzewka, główne, gwiazdka. Użyj na końcu lekcji lub gdy uczeń prosi o "więcej zadań".
---

# Cel

Dać uczniowi **konkretne, mierzalne** zadanie do napisania w C# — samodzielnie, bez podpowiedzi w kodzie.

# Trzy poziomy

Dla każdej lekcji wygeneruj zestaw 3 ćwiczeń:

| Poziom        | Cel                                              | Czas       | Wskazówka                |
| ------------- | ------------------------------------------------ | ---------- | ------------------------ |
| Rozgrzewka 🔥 | Sprawdzenie, czy uczeń rozumie składnię          | 5-10 min   | „Powinno być łatwe"      |
| Główne ⭐     | Czy umie złożyć z poznanych klocków              | 15-20 min  | „Pomyśl, zanim napiszesz" |
| Gwiazdka ⚡   | Wyzwanie — łączy bieżącą lekcję z poprzednimi    | 20-30 min  | „Może być trudne, to OK" |

Uczeń wybiera, ile robi. Minimum: rozgrzewka + główne.

**Sekcja „Krok 5 — Ćwiczenie" w pliku lekcji zwykle podaje już trzy propozycje.** Zacznij od nich — są dopasowane do materiału. Generuj nowe tylko, gdy uczeń prosi o więcej albo tamte okazały się źle wycelowane.

# Struktura katalogu

Każde ćwiczenie to **jeden plik `.cs`** bezpośrednio w `kurs/zadania/`:

```
kurs/zadania/
├── 01-hello.cs
├── 02-zmienne-a.cs
├── 02-zmienne-b.cs
├── 02-zmienne-c.cs
└── 02-zmienne-ZADANIA.md      ← zapisuje agent
```

Nazwa pliku: `NN-temat.cs`, gdzie `NN` to kolejny numer ćwiczenia (nie numer lekcji — jedna lekcja może dać kilka plików). Trzy poziomy z jednej lekcji → sufiksy `-a`, `-b`, `-c`.

**Uruchamianie — z katalogu `kurs/zadania/`:**
```sh
dotnet run 02-zmienne-a.cs
```

**Jeden plik = jeden program.** Nie da się umieścić trzech rozwiązań w jednym pliku — każde ma własny ciąg instrukcji najwyższego poziomu. To zresztą dobrze: uczeń widzi trzy osobne, kompletne programy.

**Nie twórz projektów.** Żadnego `dotnet new console`, żadnego `.csproj`, żadnej solucji — do lekcji 14.1, gdzie projekt jest tematem lekcji.

# Format ćwiczenia

Zapisz w `kurs/zadania/NN-temat-ZADANIA.md`:

```markdown
# Lekcja N.M: [temat] — ćwiczenia

## 🔥 Rozgrzewka
**Cel:** [jednolinijkowo, co ćwiczy]
**Plik:** `NN-temat-a.cs`
**Zadanie:** [opis w 1-2 zdaniach]
**Oczekiwany wynik:**

    $ dotnet run NN-temat-a.cs
    Cześć, Anno!
    Masz 32 lata.

## ⭐ Główne
[jw.]

## ⚡ Gwiazdka
[jw.]
```

# Zasady dobrego ćwiczenia

- **Konkretny, sprawdzalny wynik.** Nie „napisz program o kotach", tylko „wypisz 5 razy 'miau', każde w nowej linii".
- **Pokaż oczekiwane wyjście dosłownie** — jako blok tekstu z `$ dotnet run ...` i wypisanymi liniami. Uczeń sam porówna.
- **Realistyczny kontekst.** Nie `int x = 5; int y = 7; policz z`. Lepiej: „w koszyku jest 5 jabłek i 7 gruszek — ile owoców razem?"
- **Wymaga myślenia, nie kopiowania.** Jeśli rozwiązanie to przykład z lekcji z podmienionymi liczbami — ćwiczenie za łatwe.
- **Tylko biblioteka standardowa.** Żadnych pakietów z NuGet w całym kursie; w projekcie (moduł 14) uczeń może sięgnąć po zewnętrzną bibliotekę, jeśli sam uzasadni, po co.
- **Nie wprowadzaj konstrukcji spoza dotychczasowych lekcji.** Zajrzyj do `wiedza/INDEX.md`, żeby sprawdzić, co uczeń już miał. Typowe wpadki: `List<T>` przed 6.2, `foreach` przed 5.3, LINQ gdziekolwiek przed 13.1, własna klasa przed 8.1, `try/catch` przed 11.1.
- **Od modułu 3 każde ćwiczenie czytające wejście musi używać `TryParse`, nie `Parse`.** `int.Parse(Console.ReadLine())` wywala program przy pierwszej literówce użytkownika — to nauka złego nawyku od pierwszego dnia.
- **Od modułu 11 każde ćwiczenie dotykające plików musi obsłużyć wyjątek.** Plik może nie istnieć i to nie jest przypadek egzotyczny.
- **Od modułu 14 dołączaj do gwiazdki wymóg testu** — jeden test xUnit z dwoma przypadkami.

# Przykłady (lekcja 2.1: zmienne i typy)

🔥 **Rozgrzewka:** Zadeklaruj `imie` (twoje imię), `wiek` (twój wiek) i `wzrost` (w metrach, np. 1.78). Wypisz każdą w osobnej linii.

⭐ **Główne:** Napisz program-wizytówkę. Zmienne `imie`, `nazwisko`, `wiek`, `miasto`. Wypisz jedną linią:
```
Cześć, jestem Anna Kowalska, mam 32 lata i mieszkam w Krakowie.
```

⚡ **Gwiazdka:** Ojciec ma 45 lat, syn 12. Zadeklaruj obie zmienne. Wypisz różnicę wieku. Potem wypisz, ile lat będzie miał syn, gdy ojciec skończy 60. Na koniec: policz, jaki procent życia ojca przeżył syn — i sprawdź, czy wynik nie wychodzi zerem. (Bez `if`, bez wczytywania — same zmienne i arytmetyka.)

> Ostatnia część gwiazdki celowo wpycha ucznia w dzielenie całkowite: `12 / 45` da `0`. To najlepszy moment, żeby ta pułapka zadziałała na własnej skórze.

# Co po wygenerowaniu

- Pokaż uczniowi tylko 🔥 i ⭐ (gwiazdkę odsłoń, gdy skończy oba)
- Powiedz dokładnie:
  - **gdzie** ma zapisać kod: `kurs/zadania/NN-temat-a.cs`
  - **jak** uruchomić — z komendą wg `srodowisko.dotnet_cmd`:
    ```sh
    cd kurs/zadania
    dotnet run NN-temat-a.cs
    ```
  - **że zapisuje plik przed uruchomieniem** (Cmd+S / Ctrl+S) — najczęstszy błąd świata
- Jeśli uczeń wygląda na zagubionego co do workflow — przypomnij: „Zajrzyj do `kurs/JAK-PISAC-KOD.md`, sekcja 4 — cały cykl krok po kroku."
- **Uczeń sam uruchamia kod i wkleja wynik.** Ty potem robisz review (skill: `review-kodu`)

# Reguła komend — ZAWSZE z `student.json`

Przed wypisaniem jakiejkolwiek komendy:
```bash
dotnet run .claude/skills/postep/postep.cs -- read --field srodowisko
```
Użyj `dotnet_cmd` i konwencji z `system` (Windows: `type` zamiast `cat`, ścieżki z `\`). Jeśli puste → zapytaj o system i zaktualizuj.

# Twarde zasady

- **Nigdy nie pisz rozwiązania** w `ZADANIA.md`. Tylko opis i oczekiwane wyjście.
- **Nie twórz nawet szkieletu** w pliku `.cs` — to plik ucznia. W C# z instrukcjami najwyższego poziomu nie ma zresztą czego szkicować: pierwsza linia, którą uczeń napisze, jest już całym programem. (Sam pusty plik jeszcze nim nie jest — daje `CS5001`, bo nie ma ani jednej instrukcji do wykonania.)
- **Nie twórz projektów ani plików `.csproj`.** Ćwiczenia to pojedyncze pliki `.cs`. Projekt pojawia się raz, w lekcji 14.1.
- **Nie uruchamiaj rozwiązania ucznia**, żeby „sprawdzić, czy wychodzi". Poproś o wklejenie wyniku.
- Jeśli uczeń prosi „daj mi szablon" — odmów miękko: „Wypisz mi w czacie, jakie zmienne będą ci potrzebne i jakiego typu. Kod napiszesz, jak będziesz miał to na papierze."
