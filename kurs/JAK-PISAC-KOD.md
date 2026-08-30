# Jak pisać i uruchamiać kod — instrukcja dla ucznia

Ten dokument odpowiada na trzy pytania:
1. **Gdzie** pisać kod?
2. **W czym** pisać kod (edytor)?
3. **Jak** uruchomić kod, żeby zobaczyć, co robi?

Przeczytaj raz, na początku kursu. Potem wracaj, gdy o czymś zapomnisz.

---

## 1. Gdzie pisać kod — struktura katalogów

Twój kod żyje w katalogu `kurs/zadania/`. Każde ćwiczenie to **jeden plik**:

```
kurs/
└── zadania/
    ├── 01-hello.cs
    ├── 02-zmienne-a.cs
    ├── 02-zmienne-b.cs
    ├── 02-zmienne-c.cs
    ├── 02-zmienne-ZADANIA.md    ← treść zadań, zapisuje agent
    └── 05-petle.cs
```

To wszystko. Nie ma tu projektu, solucji, pliku konfiguracyjnego ani folderu na każde zadanie.

### Reguły nazewnictwa

- Plik: `NN-krotki-temat.cs`, przy kilku zadaniach z jednej lekcji z literą: `-a`, `-b`, `-c`
- **Numerację i nazwę poda Ci agent** — nie musisz tego wymyślać sam

### Jeden plik = jeden program

Każdy plik `.cs` w tym katalogu jest samodzielnym programem. Trzy rozwiązania jednego ćwiczenia (🔥 ⭐ ⚡) to trzy pliki, nie trzy fragmenty w jednym.

Brzmi to zwyczajnie, a jeszcze do niedawna tak nie było: żeby uruchomić najprostszy program w C#, trzeba było założyć projekt złożony z kilku plików. Od .NET 10 wystarczy jeden plik i jedna komenda. Jeśli zobaczysz gdzieś instrukcję każącą klikać w kreatorze „nowy projekt konsolowy" — to instrukcja sprzed tej zmiany.

### Eksperymenty na boku

Chcesz coś sprawdzić „na brudno" — utwórz `kurs/zadania/99-notatnik.cs`. To plik do niszczenia, nie do oceny.

---

## 2. W czym pisać kod — edytor

### Rekomendacja: VS Code

1. Pobierz z https://code.visualstudio.com/
2. Zainstaluj rozszerzenie **C# Dev Kit** (Extensions → wyszukaj „C# Dev Kit")
3. Przy pierwszym otwarciu pliku `.cs` rozszerzenie dociągnie narzędzia → **zgódź się**
4. **Otwórz cały katalog kursu**, nie pojedynczy plik: `File → Open Folder` → `claude-agent-csharp-course`

Punkt 4 jest ważniejszy, niż wygląda. Przy otwartym całym katalogu edytor widzi plik `.editorconfig` i wie, jak formatować Twój kod. Przy otwartym pojedynczym pliku — nie wie.

### Jedno ustawienie warte pięciu sekund

Włącz **„Format on Save"** (Settings → wyszukaj „format on save"). Od tej chwili Twój kod formatuje się sam przy każdym zapisie i nigdy nie będziesz walczyć z wcięciami.

### Inne opcje

- **Rider** (JetBrains) — pełne IDE, bezpłatne do użytku niekomercyjnego
- **Visual Studio** — tylko Windows, kilkanaście gigabajtów. Kurs go nie potrzebuje; jeśli już go masz, możesz używać
- **NIE używaj:** Worda, TextEdit na macOS w trybie sformatowanym, Notatnika Windows w domyślnej konfiguracji — wstawiają znaki, których kompilator nie zrozumie (najczęściej cudzysłowy typograficzne „ ” zamiast zwykłych " ")

### Co musi umieć Twój edytor

- Zapisywać w **UTF-8** (dla polskich znaków `ą ę ć`)
- Kolorować składnię C#
- Najlepiej: formatować przy zapisie

---

## 3. Jak uruchomić kod — terminal

### Co to terminal?

Okienko, w którym wpisujesz **komendy tekstowe** zamiast klikać.

| System       | Jak otworzyć                                     |
| ------------ | ------------------------------------------------ |
| **macOS**    | Cmd+Space → „Terminal" → Enter                   |
| **Linux**    | Ctrl+Alt+T (większość dystrybucji)               |
| **Windows**  | Win+X → „Terminal" / „Windows PowerShell"        |

Możesz też użyć terminala wbudowanego w VS Code: `Terminal → New Terminal` (Ctrl+`). To zwykle najwygodniejsze — kod i terminal w jednym oknie.

**Dobra wiadomość:** komendy `dotnet ...` są **identyczne na wszystkich systemach**. Różnice dotyczą tylko samej powłoki:

| W tej instrukcji (macOS/Linux) | Windows PowerShell |
| --- | --- |
| `cat plik.cs` | `type plik.cs` |
| `ls -l` | `dir` |
| `export X=y` | `$env:X="y"` |

Agent dopasuje komendy do Twojego systemu — powiedz mu na początku, jakiego używasz.

### Krok po kroku — uruchomienie programu

#### 1. Przejdź do katalogu z zadaniami

```sh
cd ~/Projects/claude-agent-csharp-course/kurs/zadania
```

`cd` = „change directory". Sprawdź, gdzie jesteś:
```sh
pwd
```

**To ważne:** wszystkie zadania uruchamiasz **z katalogu `kurs/zadania`**.

#### 2. Uruchom program

```sh
dotnet run 01-hello.cs
```

To, co program wypisze przez `Console.WriteLine`, pojawi się **w terminalu**, pod komendą.

> **Pierwsze uruchomienie potrwa kilka sekund.** .NET buduje program i zapamiętuje wynik; kolejne razy będą natychmiastowe. To nie jest zawieszenie.

> **Gdzie wylądował zbudowany program?** Poza Twoim katalogiem, w pamięci podręcznej .NET. Dlatego po `dotnet run` nie pojawiają się żadne nowe pliki — masz w katalogu tylko to, co sam napisałeś.

#### 3. Sprawdzenie bez uruchamiania

```sh
dotnet build 01-hello.cs
```

Buduje program i mówi, czy się kompiluje — ale **go nie uruchamia**. Przydaje się, gdy chcesz tylko sprawdzić, czy nie masz literówki. Agent używa wyłącznie tej komendy; `dotnet run` należy do Ciebie.

#### 4. Coś się nie zgadza? Czytaj komunikat

Są **dwa różne momenty**, w których coś może pójść nie tak.

**A. Program się nie skompilował** — nie uruchomił się w ogóle:

```
/Users/anna/kurs/zadania/01-hello.cs(1,30): error CS1002: ; expected
```

Czytaj tak:
- `01-hello.cs(1,30)` — plik, **linia 1, znak 30**. To pierwsze, na co patrzysz
- `error CS1002` — kod błędu. Stały, niezależny od Twojego kodu. Można go wpisać w wyszukiwarkę
- `; expected` — czego kompilator oczekiwał w tym miejscu

**Zacznij od współrzędnych.** Idź w kodzie dokładnie tam i przeczytaj tę linię.

**B. Program się uruchomił i przerwał w trakcie** (wyjątek):

```
Unhandled exception. System.FormatException: The input string 'abc' was not in a correct format.
   at System.Number.ThrowFormatException[TChar](ReadOnlySpan`1 value)
   at Program.<Main>$(String[] args) in /Users/anna/kurs/zadania/04-konwersje.cs:line 3
```

Czytaj tak:
- pierwsza linia — **co** się stało: tekst `'abc'` nie miał formatu liczby
- linia z nazwą **Twojego** pliku (`04-konwersje.cs:line 3`) — **gdzie**. Pomiń linie z `System.` — to wnętrzności .NET, nie Twój kod
- reszta — na razie ignoruj

Komunikaty C# bywają długie. **To nie jest katastrofa, to informacja.** Twoją rolą jest najpierw **przeczytać samodzielnie**, a dopiero potem pokazać agentowi.

---

## 4. Cały workflow — od zadania do review

```
┌─────────────────────────────────────────────────────────────┐
│ 1. Agent prowadzi lekcję w czacie Claude Code               │
│    → odpowiadasz na pytania naprowadzające                  │
├─────────────────────────────────────────────────────────────┤
│ 2. Agent daje ćwiczenie („napisz program, który...")        │
│    → mówi, gdzie zapisać: kurs/zadania/NN-temat.cs          │
├─────────────────────────────────────────────────────────────┤
│ 3. Otwórz VS Code obok Claude Code                          │
│    → utwórz plik, napisz kod, ZAPISZ (Cmd+S / Ctrl+S)       │
├─────────────────────────────────────────────────────────────┤
│ 4. W terminalu, z katalogu kurs/zadania:                    │
│    dotnet run NN-temat.cs                                   │
│    → patrzysz na wynik                                      │
├─────────────────────────────────────────────────────────────┤
│ 5. Sam(a) oceń: czy wynik jest taki, jakiego oczekiwałeś?   │
│    → nie? czytaj komunikat i poprawiaj                      │
├─────────────────────────────────────────────────────────────┤
│ 6. Gdy działa — wracasz do Claude Code i piszesz:           │
│    „skończyłem rozgrzewkę z lekcji 2.1"                     │
│    → wklejasz kod i wynik, agent robi review przez pytania  │
└─────────────────────────────────────────────────────────────┘
```

**Agent nie uruchomi Twojego programu.** To nie ograniczenie techniczne — to część metody. Patrzenie na wynik i czytanie komunikatów jest tym, czego się właśnie uczysz.

**Wskazówka praktyczna:** trzymaj **dwa okna obok siebie** — Claude Code po lewej, VS Code z wbudowanym terminalem po prawej.

---

## 5. Formatowanie — o czym nie musisz myśleć

Kompilatorowi jest obojętne, czy Twój kod ma wcięcia:

```csharp
Console.WriteLine("Start");if(true){Console.WriteLine("A");}
```

Ten kod działa. Ale przeczytasz go za tydzień?

W repozytorium leży plik `.editorconfig` z konwencjami tego kursu: cztery spacje wcięcia, klamry w nowej linii, kodowanie UTF-8. **Edytor go czyta.** Przy włączonym „Format on Save" Twój kod układa się sam przy każdym zapisie.

**Praktycznie:** włącz Format on Save raz i zapomnij o temacie. Formatowanie nie jest tematem tego kursu i nie powinno zajmować Ci ani minuty.

### Konwencja nazw — to warto znać

| Co | Jak | Przykład |
| --- | --- | --- |
| zmienne, parametry | `camelCase` | `liczbaKotow` |
| stałe | `PascalCase` | `StawkaVat` |
| klasy, metody, właściwości | `PascalCase` | `Console`, `WriteLine` |
| nigdy | `snake_case` | ~~`liczba_kotow`~~ |

Polskie nazwy zmiennych są w porządku (`cenaBrutto`, `liczbaKotow`). Polskie znaki w nazwach (`żółw`) — lepiej nie.

---

## 6. Najczęstsze pułapki początkujących

### ❌ Zapomniałem zapisać plik przed `dotnet run`

**Najczęstszy błąd świata.** Poprawiasz kod, uruchamiasz, widzisz stary wynik i szukasz błędu tam, gdzie go nie ma. Cmd+S / Ctrl+S **zawsze** przed uruchomieniem.

### ❌ Uruchamiam z niewłaściwego katalogu

```
Couldn't find a project to run. Ensure a project exists in /Users/anna/Projects/claude-agent-csharp-course,
or pass the path to the project using --project.
```

Ten komunikat jest mylący i warto go rozszyfrować raz: .NET nie znalazł pliku, który podałeś, więc **przestawił się na szukanie projektu** w bieżącym katalogu — i o projekcie ci mówi. Ty żadnego projektu nie masz i nie potrzebujesz.

Prawdziwa przyczyna jest prostsza: jesteś poza `kurs/zadania` albo pomyliłeś nazwę pliku. Sprawdź `pwd`, potem `ls`, wróć na miejsce przez `cd`.

### ❌ Brakujący średnik

```
error CS1002: ; expected
```
Kompilator poda współrzędne. Idź tam.

### ❌ Wielkość liter

`console` ≠ `Console`, `writeline` ≠ `WriteLine`. C# rozróżnia wielkie i małe litery **wszędzie**.

### ❌ Polskie znaki wypisują się jako krzaczki

Sprawdź, czy edytor zapisuje w UTF-8 (w VS Code widać w prawym dolnym rogu).

### ❌ Skopiowałem kod z internetu i nie działa

Dwie najczęstsze przyczyny:
1. **Cudzysłowy typograficzne** — strona zamieniła `"` na `„ ”`. Wygląda podobnie, kompilator odmawia. Przepisz ręcznie
2. **Kod ze starszego C#** — otoczony `class Program { static void Main... }`. Zwykle działa, ale nie musisz tak pisać

### ❌ Program nie chce się zatrzymać

**Ctrl+C** przerywa działający program.

### ❌ Zobaczyłem w internecie zupełnie inny kod niż piszę

To normalne i nie znaczy, że robisz źle. C# zmienił się mocno po 2020 roku: dziś program może być samym ciągiem instrukcji, bez klasy i bez projektu. Starsze materiały pokazują dłuższą formę. **Obie działają**; Twoja jest krótsza.

---

## 7. Komendy terminala — minimum, które Ci wystarczy

| Komenda | Co robi |
| --- | --- |
| `pwd` | Pokaż, w którym katalogu jestem |
| `ls` (Windows: `dir`) | Pokaż pliki w bieżącym katalogu |
| `cd nazwa` | Wejdź do podkatalogu |
| `cd ..` | Wyjdź o katalog wyżej |
| `cd ~` | Wróć do katalogu domowego |
| **Ctrl+C** | Przerwij działający program |
| **strzałka w górę** | Powtórz ostatnią komendę |

Strzałka w górę to ta, której będziesz używać najczęściej: poprawka w kodzie → strzałka w górę → Enter.

---

## 8. Komendy .NET, które poznasz w tym kursie

| Komenda | Co robi | Od lekcji |
| --- | --- | --- |
| `dotnet --version` | Pokaż wersję SDK | 1.1 |
| `dotnet run nazwa.cs` | Uruchom program | 1.2 |
| `dotnet build nazwa.cs` | Sprawdź, czy się kompiluje — bez uruchamiania | 1.3 |
| `dotnet new console -o nazwa` | Załóż projekt | 14.1 |
| `dotnet test` | Uruchom testy | 14.3 |
| `dotnet publish` | Zbuduj program do rozdania innym | 14.3 |

Nie musisz ich pamiętać teraz. Agent poda właściwą, gdy przyjdzie na nią czas. Pierwsze trzy wystarczą Ci przez trzynaście modułów.

---

## 9. Nie ma trybu interaktywnego — i co z tego wynika

W niektórych językach można otworzyć konsolę i pisać kod linijka po linijce. **C# tego nie ma** — jest językiem kompilowanym: program musi zostać zbudowany w całości, zanim ruszy.

Wygląda to na utrudnienie, ale ma zaletę: kompilator sprawdza cały Twój kod **przed** uruchomieniem i wyłapuje literówki, niezgodne typy i brakujące fragmenty. W językach interpretowanych dowiedziałbyś się o nich dopiero wtedy, gdy program dojdzie do tej linii — czasem po godzinie działania.

Do szybkich eksperymentów masz dwie drogi:
1. Plik roboczy (`99-notatnik.cs`) — zmieniasz, uruchamiasz, kasujesz
2. **dotnet fiddle** (https://dotnetfiddle.net) — piszesz w przeglądarce, klikasz Run. Wygodne do jednorazowego „co zwróci ta funkcja?". Nie ma dostępu do plików ani sieci

---

## Pytania? Pisz do agenta

W Claude Code możesz w każdej chwili powiedzieć:

- *„jak mam to uruchomić?"*
- *„nie wiem, gdzie zapisać kod"*
- *„co znaczy ten komunikat?"* (wklej cały, razem z kodem `CSxxxx`)
- *„nie chce się skompilować"*

Agent wróci do odpowiedniego fragmentu tej instrukcji albo poprowadzi Cię krok po kroku.
