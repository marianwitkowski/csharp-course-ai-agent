# Indeks bazy wiedzy — mapa kursu C# / .NET

> **Po co ten plik?** To **źródło prawdy** dla struktury kursu: ile jest modułów, ile lekcji, w jakiej kolejności i na czym każda się opiera. Skille `program-kursu` i `lekcja` czytają go w pierwszej kolejności. Jeśli inny plik podaje inną liczbę lekcji — to błąd dokumentacji, nie zmiana programu.

> **Status gotowych lekcji sokratejskich:** 7/47 w `wiedza/lekcje/` (moduły 1–2 kompletne; moduły 3–14 w przygotowaniu).

> **Uwaga o środowisku:** kurs jest **konsolowy i wieloplatformowy**. Cały kod działa tak samo na macOS, Linuksie i Windows, uruchamiany przez `dotnet`. Nie ma tu Windows Forms, WPF, WinUI ani Web Forms — dlaczego, wyjaśnia sekcja „Czego w kursie nie ma".

> **Uwaga o komendach:** przykłady w lekcjach są w wersji macOS/Linux (ścieżki z `/`). Na Windows separator to `\`, ale sama komenda `dotnet` jest identyczna na każdym systemie. Agent tłumaczy ścieżki w trakcie sesji.

---

## Katalogi bazy

| Katalog | Zawartość | Rola |
| --- | --- | --- |
| `wiedza/lekcje/` | 47 lekcji sokratejskich + `SZABLON-LEKCJI.md` | **scenariusze prowadzenia** — to czytasz w pierwszej kolejności |
| `wiedza/przyklady/kod/` | minimalne, działające programy `.cs` | materiał do eksperymentów i inspiracja na ćwiczenia |
| `wiedza/AKTUALIZACJE.md` | delta „.NET Framework (2020) → .NET 10 (2026)" | prostuje to, co uczeń znajdzie w starszych poradnikach |

W tym kursie **nie ma katalogu `zrodlo/`**. Materiały źródłowe autora leżą poza
repozytorium (`.kb/`, wyłączone z kontroli wersji — patrz `NOTICE.md`), a cała
treść dydaktyczna została napisana od nowa wprost w `wiedza/lekcje/`.

---

## Program — 14 modułów, 47 lekcji

### Moduł 1 — Wprowadzenie i środowisko (3)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 1.1 | Czym jest C# i .NET — do czego to służy | — | `[ogólne]` |
| 1.2 | Pierwszy program — `dotnet run`, `Console.WriteLine` | `01-hello.cs` | `[moduł 1]` |
| 1.3 | Edytor, terminal, `.editorconfig` — workflow ucznia | — | `[moduł 1]` |

### Moduł 2 — Zmienne i typy (4)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 2.1 | Zmienne i typy proste (`int`, `double`, `string`, `bool`, `var`) | `02-zmienne.cs` | `[moduł 2]` |
| 2.2 | Stałe i typy wyliczeniowe (`const`, `enum`) | `03-const-enum.cs` | — |
| 2.3 | Konwersje typów — jawne, `Parse`, `TryParse` | `04-konwersje.cs` | `[moduł 2]` |
| 2.4 | Operatory i wyrażenia | `05-operatory.cs` | — |

### Moduł 3 — Rozmowa z użytkownikiem (2)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 3.1 | Wypisywanie — `WriteLine`, interpolacja `$"..."`, formatowanie | — | `[moduł 3]` |
| 3.2 | Wejście — `Console.ReadLine`, walidacja przez `TryParse` | — | `[moduł 3]` |

### Moduł 4 — Decyzje (3)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 4.1 | `if` / `else if` / `else` | — | — |
| 4.2 | Operatory logiczne i operator warunkowy `?:` | — | — |
| 4.3 | `switch` — instrukcja i wyrażenie | — | `[moduł 4]` — `switch` jako wyrażenie |

### Moduł 5 — Pętle (3)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 5.1 | `while` i `do...while` | — | — |
| 5.2 | `for` | — | — |
| 5.3 | `foreach`, `break`, `continue` | — | — |

### Moduł 6 — Kolekcje (4)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 6.1 | Tablice — stały rozmiar, indeksowanie od zera | — | `[moduł 6]` — indeksy `^1` i zakresy `..` |
| 6.2 | `List<T>` — kolekcja, która rośnie | — | — |
| 6.3 | `Dictionary<TKey,TValue>` — klucz → wartość | — | `[moduł 6]` — `TryGetValue` |
| 6.4 | Tablice wielowymiarowe i tablice tablic | — | — |

### Moduł 7 — Metody (3)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 7.1 | Metody — parametry, wartość zwracana, `void` | — | — |
| 7.2 | Przeciążanie, parametry domyślne i nazwane | — | — |
| 7.3 | `ref`, `out`, zasięg zmiennych | — | — |

### Moduł 8 — Klasy i obiekty (4)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 8.1 | Klasa jako własny typ — pola i obiekty | — | — |
| 8.2 | Konstruktory | — | `[moduł 8]` — konstruktory podstawowe |
| 8.3 | Właściwości — `get`/`set`, właściwości automatyczne | — | — |
| 8.4 | Modyfikatory dostępu i enkapsulacja | — | — |

### Moduł 9 — Programowanie obiektowe (4)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 9.1 | Dziedziczenie — `:` i `base` | — | — |
| 9.2 | `virtual` / `override` — polimorfizm | — | — |
| 9.3 | Klasy i metody abstrakcyjne | — | — |
| 9.4 | Składowe statyczne i klasy statyczne | — | — |

### Moduł 10 — Interfejsy (3)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 10.1 | Interfejs jako kontrakt | — | — |
| 10.2 | `ToString()`, `IComparable`, `IEnumerable` | — | — |
| 10.3 | Kompozycja zamiast dziedziczenia — kiedy co | — | — |

### Moduł 11 — Wyjątki (2)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 11.1 | `try` / `catch` / `finally` | — | — |
| 11.2 | `throw`, własne wyjątki, `using` i `IDisposable` | — | — |

### Moduł 12 — Pliki i dane (4)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 12.1 | Pliki tekstowe — `File`, `StreamReader` / `StreamWriter` | — | `[moduł 12]` |
| 12.2 | Ścieżki i katalogi — `Path`, `Directory` | — | — |
| 12.3 | JSON — `System.Text.Json` | — | `[moduł 12]` — `System.Text.Json` zamiast Newtonsoft |
| 12.4 | Argumenty wiersza poleceń (`args`) | — | — |

### Moduł 13 — LINQ (3)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 13.1 | LINQ — `Where`, `Select`, `OrderBy` | — | — |
| 13.2 | Agregacje i grupowanie — `Count`, `Sum`, `Max`, `GroupBy` | — | — |
| 13.3 | Wyrażenia lambda i `Func<>` — co siedzi pod LINQ | — | — |

### Moduł 14 — Projekt i dalsze kroki (5)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 14.1 | Wybór projektu i rozpisanie na kroki — `dotnet new console`, `.csproj` | — | `[moduł 14]` |
| 14.2 | Implementacja — od szkieletu do działania | — | — |
| 14.3 | Testy xUnit, README, `dotnet publish` | — | `[moduł 14]` |
| 14.4 | AI w pracy programisty — jak korzystać i jak weryfikować | — | `[moduł 14]` |
| 14.5 | Mapa ekosystemu — co dalej (ASP.NET Core, EF Core, Blazor, WPF) | — | `[moduł 14]` |

---

## Podsumowanie liczbowe

| Moduł | Lekcje |
| --- | --- |
| 1. Wprowadzenie i środowisko | 3 |
| 2. Zmienne i typy | 4 |
| 3. Rozmowa z użytkownikiem | 2 |
| 4. Decyzje | 3 |
| 5. Pętle | 3 |
| 6. Kolekcje | 4 |
| 7. Metody | 3 |
| 8. Klasy i obiekty | 4 |
| 9. Programowanie obiektowe | 4 |
| 10. Interfejsy | 3 |
| 11. Wyjątki | 2 |
| 12. Pliki i dane | 4 |
| 13. LINQ | 3 |
| 14. Projekt i dalsze kroki | 5 |
| **Razem** | **47** |

**Ten plik jest źródłem prawdy dla liczby 47.** Jeśli inny plik podaje inną liczbę — to błąd dokumentacji.

---

## Zależności między modułami — czego nie wolno przestawić

Kolejność nie jest przypadkowa. Trzy miejsca są sztywne:

- **8 przed 9 przed 10.** Nie da się uczyć dziedziczenia bez klasy, ani interfejsu bez metody wirtualnej. To najdłuższy łańcuch w kursie.
- **7 przed 8.** Metoda w klasie to ta sama metoda co samodzielna, tylko z `this`. Uczeń, który nie rozumie parametrów i wartości zwracanej, w module 8 utknie na czymś innym, niż mu się wydaje.
- **11 przed 12.** Każda operacja na pliku może się nie udać. Bez wyjątków lekcje 12.1–12.4 uczyłyby ignorowania błędów.

Reszta ma pewien luz: moduł 13 (LINQ) można przesunąć za 12 albo przed 12,
zależnie od tego, na czym uczniowi zależy.

---

## Czego w kursie nie ma (świadome decyzje)

Program XL („C# (.NET) Developer XL") wymienia znacznie więcej niż te 47 lekcji.
Poniższe tematy są **świadomie** poza kursem dla początkujących — nie jako
przeoczenie, tylko dlatego, że każdy z nich wymaga fundamentu, który ten kurs
dopiero buduje.

| Temat | Dlaczego pominięty | Gdzie wspomniany |
| --- | --- | --- |
| Windows Forms, WPF, WinUI | Działają **tylko na Windows** — kurs musi działać też na macOS i Linuksie. Poza tym GUI odciąga uwagę od języka: uczeń debuguje układ kontrolek zamiast logiki | 14.5 |
| ASP.NET Web Forms, GridView, ObjectDataSource | Technologia wycofana — **nie istnieje** w .NET Core ani w .NET 5+. Uczenie jej w 2026 to uczenie ślepej uliczki | 14.5 — jednym zdaniem, jako kontekst historyczny |
| ASP.NET Core, MVC, Razor, Blazor | Wymagają rozumienia HTTP, cyklu żądanie-odpowiedź i asynchroniczności. Osobny kurs | 14.5 |
| Bazy danych, SQL, Entity Framework Core | Wymagają SQL, a SQL to osobny język. Kurs uczy C#, nie dwóch języków naraz | 14.5 |
| Wzorce: MVC, MVVM, Onion, DDD, DI/IoC | Poziom architektoniczny. Wzorzec rozwiązuje problem, którego początkujący jeszcze nie ma — bez tego problemu wzorzec to pusty rytuał | 14.5 |
| Typy generyczne własne (`class Pudelko<T>`) | Uczeń **używa** `List<T>` i `Dictionary<K,V>` od modułu 6, ale własnych generyków nie pisze — potrzeba pojawia się dopiero przy bibliotekach | 14.5 |
| `async` / `await`, wielowątkowość | Sensowne dopiero przy sieci, plikach o dużym rozmiarze i UI. Wprowadzone za wcześnie daje kod, który „działa dziwnie" bez widocznej przyczyny | 14.5 |
| Metody rozszerzające | Wymagają klas statycznych (9.4) i pewności w czytaniu sygnatur. Uczeń **korzysta** z nich w module 13 (LINQ to metody rozszerzające), ale swoich nie pisze | 13.1 — jedno zdanie |
| `record`, `struct`, typy nullowalne (`string?`) | Każdy z nich to wariant czegoś, co uczeń dopiero co poznał. Wprowadzone równolegle z klasą rozmywają obraz | 14.5 |
| Refleksja, `unsafe`, wskaźniki | Nigdy dla początkującego | — |
| Visual Studio (pełne IDE) | Tylko Windows, ciężkie, ukrywa `dotnet` za przyciskami. Kurs uczy narzędzi wiersza poleceń, bo one działają wszędzie i pokazują, co się naprawdę dzieje | 1.3 — wzmianka jako alternatywa dla użytkowników Windows |

**Uczeń pytający o którykolwiek z tych tematów** dostaje jedno zdanie, co to
jest, i odesłanie do lekcji **14.5** („mapa ekosystemu"). Nie rozwijaj — to
najkrótsza droga do rozjechania programu.
