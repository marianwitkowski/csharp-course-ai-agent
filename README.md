# Kurs języka C# i .NET z tutorem AI

Interaktywny kurs podstaw **C#** dla **kompletnie początkujących**, prowadzony przez agenta AI (działającego w Claude Code/Codex) metodą **sokratejską** — uczeń sam dochodzi do rozwiązań przez pytania naprowadzające.

Od `Console.WriteLine("Cześć")` do własnego narzędzia wiersza poleceń z testami: **49 lekcji w 14 modułach**.

## Czym ten kurs jest, a czym nie jest

To kurs **fundamentów języka C#** z osobistym tutorem AI — **pierwszy etap** drogi do zawodowego .NET, nie cała droga. Po jego ukończeniu uczeń umie samodzielnie zaprojektować, napisać, uruchomić, przetestować i poprawić niewielką aplikację konsolową, trzymać ją w Gicie i krytycznie korzystać z asystenta AI. Umie też przeczytać dokumentację i większość kodu C#, jaki spotka.

Nie jest to jeszcze przygotowanie do pracy jako junior .NET developer. Drugi etap — osobny kurs albo własna nauka według mapy z lekcji 14.7 — powinien objąć co najmniej: `async`/`await` w praktyce (podstawy daje opcjonalny moduł 15), HTTP i korzystanie z API, ASP.NET Core, wstrzykiwanie zależności, SQL i Entity Framework Core, konfigurację i logowanie, testy integracyjne, podstawy Dockera i bezpieczeństwa aplikacji webowych. Każda z tych rzeczy zakłada fundament, który buduje ten kurs, i żadna nie ma sensu bez niego.

## Dla kogo

- Osoby, które **nigdy nie programowały** — kurs nie zakłada znajomości żadnego innego języka
- Chcące uczyć się w swoim tempie, z prowadzącym, który nie podaje gotowców
- Pracujące na **dowolnym systemie** — macOS, Linux, Windows; kurs jest konsolowy i wieloplatformowy
- Mające zainstalowane lub gotowe zainstalować Claude Code

## Jak zacząć

> 🚀 **Szybki start:** zobacz **[QUICKSTART.md](QUICKSTART.md)** — przewodnik krok po kroku z przykładami.

W katalogu kursu uruchom Claude Code i napisz:

```
ucz mnie C#
```

Agent `csharp-tutor` przeprowadzi Cię przez:
1. Sprawdzenie środowiska (.NET SDK 10.0+, edytor)
2. Krótki wywiad (cel, dostępny czas)
3. Wygenerowanie programu kursu dopasowanego do Ciebie
4. Pierwszą lekcję

## Struktura projektu

```
.
├── .claude/
│   ├── agents/csharp-tutor.md          # główny agent
│   └── skills/                         # specjalistyczne umiejętności
│       ├── setup-dotnet/               # sprawdzenie środowiska, instalacja .NET
│       ├── program-kursu/              # generator programu
│       ├── lekcja/                     # prowadzenie lekcji
│       ├── cwiczenie/                  # generator ćwiczeń (3 poziomy)
│       ├── review-kodu/                # sokratejski review kodu
│       ├── quiz/                       # quizy powtórkowe między lekcjami
│       ├── reset-kursu/                # reset miękki/pełny z backupem
│       ├── pomoc/                      # lista komend w czacie
│       └── postep/                     # śledzenie postępu (narzędzie w C#)
├── wiedza/                             # baza wiedzy kursu
│   ├── INDEX.md                        # struktura 49 lekcji — źródło prawdy
│   ├── lekcje/                         # gotowe lekcje + SZABLON-LEKCJI.md
│   ├── przyklady/kod/                  # minimalne przykłady .cs
│   ├── przyklady/zepsute/              # 10 programów z jednym błędem — ćwiczenia „napraw"
│   └── AKTUALIZACJE.md                 # delta: .NET Framework → .NET 10
├── narzedzia/
│   ├── sprawdz-frontmatter.rb          # walidacja nagłówków YAML lekcji, skilli i agenta (dla autora)
│   └── sprawdz-przyklady.sh            # kompilacja wszystkich przykładów .cs i postep.cs
├── .github/workflows/walidacja.yml     # to samo w GitHub Actions przy każdym push i PR
├── kurs/
│   ├── JAK-PISAC-KOD.md                # ⬅ przeczytaj na początku: workflow ćwiczeń
│   ├── program.md                      # Twój program kursu (powstanie po onboardingu)
│   ├── lekcje/                         # notatki z każdej lekcji
│   ├── zadania/                        # Twój kod — jeden plik .cs na ćwiczenie
│   └── projekt/                        # Twój program z modułu 14
├── postep/
│   ├── student.json                    # Twój stan: lekcje, mocne strony, do powtórki
│   ├── backups/                        # automatyczne kopie przed każdym zapisem
│   └── archiwum/                       # backupy po resetach (nigdy nie kasowane automatycznie)
└── .editorconfig                       # konwencje formatowania — czyta je edytor
```

## Program kursu — 14 modułów, 49 lekcji

| Moduł | Temat | Lekcje |
| --- | --- | --- |
| 1 | Wprowadzenie i środowisko | 2 |
| 2 | Zmienne i typy — `int`, `double`, `string`, `bool`, `const`, `enum`, konwersje | 4 |
| 3 | Rozmowa z użytkownikiem — wypisywanie, interpolacja, wejście | 2 |
| 4 | Decyzje — `if`, operatory logiczne, `switch` | 3 |
| 5 | Pętle — `while`, `for`, `foreach` | 3 |
| 6 | Kolekcje — tablice, `List<T>`, `Dictionary<K,V>` | 4 |
| 7 | Metody — parametry, przeciążanie, `ref` i `out` | 3 |
| 8 | Klasy i obiekty — konstruktory, właściwości, enkapsulacja, `null` i `string?` | 5 |
| 9 | Programowanie obiektowe — dziedziczenie, polimorfizm, `abstract`, `static` | 4 |
| 10 | Interfejsy — kontrakt, `IComparable`, kompozycja | 3 |
| 11 | Wyjątki — `try`/`catch`, własne wyjątki, `using` | 2 |
| 12 | Pliki i dane — pliki tekstowe, ścieżki, JSON, argumenty CLI | 4 |
| 13 | LINQ — filtrowanie, agregacje, lambdy | 3 |
| 14 | Projekt i dalsze kroki — własne narzędzie, Git, testy, gałęzie i scalanie, AI, mapa ekosystemu | 7 |
| 15 | **Dodatek po kursie (opcjonalny):** `Task`, `async`/`await`, `Task.WhenAll` — na własnym projekcie, z testami | 2 |

Źródłem prawdy dla struktury jest [`wiedza/INDEX.md`](wiedza/INDEX.md). Moduł 15 nie wlicza się do 49 lekcji kursu — to dwie lekcje dla tych, którzy idą dalej w stronę ASP.NET Core.

> **Stan gotowych scenariuszy:** wszystkie **49 lekcji** ma napisane pełne scenariusze sokratejskie w `wiedza/lekcje/` — każdy komunikat kompilatora i każdy pokazany wynik został wcześniej uruchomiony na .NET 10. Źródłem prawdy dla struktury kursu jest `wiedza/INDEX.md`.

**Czego w kursie nie ma:** aplikacji okienkowych (Windows Forms, WPF, WinUI), aplikacji webowych (ASP.NET Core, Blazor), baz danych i Entity Framework, wzorców architektonicznych (MVC, MVVM, DDD, DI), własnych typów generycznych, wielowątkowości. `async`/`await` jest w **dodatku** (moduł 15), nie w rdzeniu. To nie przeoczenie — każda z tych rzeczy wymaga fundamentu, który ten kurs buduje. Pełna lista wraz z uzasadnieniami jest w `wiedza/INDEX.md`; mapa dalszych kroków czeka w lekcji 14.7.

## Dlaczego konsola, a nie okienka

Program szkolenia, z którego wyrósł ten kurs, obejmował Windows Forms i ASP.NET Web Forms. Tutaj ich nie ma i to jest decyzja, nie zaniedbanie:

- **Windows Forms i WPF działają wyłącznie na Windows.** Kurs, który połowie uczniów nie ruszy, nie jest kursem dla początkujących
- **ASP.NET Web Forms nie istnieje w nowoczesnym .NET.** Nie został przeniesiony z .NET Framework i nie zostanie
- **GUI odciąga uwagę od języka.** Uczeń debuguje układ kontrolek zamiast logiki — a uczy się właśnie logiki

Konsola pokazuje dokładnie to, co program robi, i nic więcej. Co postawić na tym fundamencie, jest tematem lekcji 14.7.

## Zanim zaczniesz pierwszą lekcję

Przeczytaj **[`kurs/JAK-PISAC-KOD.md`](kurs/JAK-PISAC-KOD.md)** — 5 minut, ale wyjaśnia:
- gdzie zapisywać kod (`kurs/zadania/NN-temat.cs` — jeden plik na ćwiczenie)
- jak uruchamiać programy (`dotnet run NN-temat.cs` — z katalogu `kurs/zadania`)
- jak czytać komunikaty kompilatora (`CSxxxx`) i wyjątki
- czym `dotnet build` różni się od `dotnet run`
- cały workflow ćwiczenia od początku do końca
- najczęstsze pułapki początkujących

## Metoda

Kurs nie tłumaczy — pyta. To brzmi jak slogan, więc poniżej konkretnie, jak to działa w praktyce.

### Rytm 3-krokowy

Czysta metoda sokratejska dla kogoś, kto nigdy nie programował, kończy się frustracją: gdy nie masz jeszcze modelu tego, jak działa program, kolejne pytania niczego nie odsłaniają, tylko podnoszą napięcie. Dlatego agent działa w rytmie z wyjściem awaryjnym:

1. **Pytanie naprowadzające** — punkt wyjścia
2. **Utknąłeś? Jeden konkretny fakt w 1-2 zdaniach** — nie wykład
3. **Kolejne pytanie**, już oparte na tym fakcie

Różnica na przykładzie:

> **Źle** — pętla pytań bez wyjścia
> — Czemu `Console.WriteLine(wiek + 5)` wypisuje `305`?
> — Jakiego typu jest `wiek`?
> — Nie wiem.
> — A co oznacza dodawanie? *(uczeń tkwi w miejscu)*

> **Dobrze** — pytanie, fakt, pytanie
> — Czemu `Console.WriteLine(wiek + 5)` wypisuje `305`?
> — Pokaż mi linię, w której tworzysz `wiek`.
> — `string wiek = "30";`
> — **`wiek` jest tekstem, nie liczbą. `+` między tekstem a liczbą nie dodaje — dokleja.** Jaką metodą zamienisz `"30"` na liczbę, zanim dodasz 5?

### Kiedy agent przestaje pytać

Przejście do wyjaśnienia nie jest kwestią wyczucia — agent ma listę sygnałów: dwa razy z rzędu „nie wiem", wprost wyrażona prośba o odpowiedź, pytanie o pojęcie spoza dotychczasowych lekcji, oznaki zniechęcenia, około pięciu minut bez postępu w jednym miejscu, a także walka z komunikatem kompilatora — te w C# bywają długie i naszpikowane nazwami typów, więc agent najpierw tłumaczy komunikat na polski, dopiero potem pyta. Po trzech-czterech nieudanych cyklach cofa się o poziom niżej, pokazuje **fragment** rozwiązania do dokończenia albo proponuje przerwę.

### Budowa lekcji — 5 kroków

| Krok | Co się dzieje |
| --- | --- |
| **1. Zakotwiczenie** | Pytanie o coś z życia, nie z programowania. Zmienne: „pudełko w spiżarni z naklejką — co na naklejce, co w środku?". Konwersje: „7 cukierków na 2 dzieci, a 7 litrów wody do 2 butelek — czemu inny wynik?" |
| **2. Mostek** | Dopiero teraz pada termin techniczny i najmniejszy działający program |
| **3. Eksperyment** | Piszesz, uruchamiasz, wklejasz wynik. Po każdym kroku pytanie: „czy tego się spodziewałeś?" |
| **4. Pogłębienie** | Przypadki brzegowe i celowe psucie kodu, żeby zobaczyć komunikaty błędów |
| **5. Ćwiczenie** | Zadania w trzech poziomach: 🔥 rozgrzewka, ⭐ główne, ⚡ gwiazdka — a od modułu 4 także 🔧 naprawa: dostajesz cudzy program z jednym błędem i objawem w nagłówku, bez przyczyny |

Najcenniejszy moment lekcji to ten, w którym program **działa**, ale wypisuje coś innego, niż zakładałeś. Materiały są tak napisane, żeby to prowokować — lekcja o zmiennych kończy się pytaniem, czemu `7 / 2` daje `3`, i **nie odpowiada na nie**. Odpowiedź przychodzi lekcję później, gdy pytanie już zdążyło uwierać.

### Czego agent nie zrobi

**Nie uruchomi Twojego kodu.** Żadnego `dotnet run`, `dotnet test`. Wolno mu wyłącznie `dotnet build` — czyli sprawdzić, czy kod się kompiluje, nie wykonując go.

To nie jest ograniczenie techniczne, tylko sedno metody. Patrzenie na wynik i czytanie komunikatów jest tą umiejętnością, której się właśnie uczysz; oddanie jej agentowi wydrążyłoby kurs z treści. Praktyczna konsekwencja: rozmowa toczy się wokół tego, co **Ty** widzisz na ekranie i wklejasz do czatu.

**Nie napisze rozwiązania ćwiczenia.** Minimalny przykład dla zrozumienia pojęcia — tak. Kod, który ma być odpowiedzią na zadanie — nie.

**Nie wyprzedzi programu — ale nie zignoruje pytania.** Pytanie o klasy na lekcji 4.2 dostanie nazwę, numer modułu, jedno zdanie „po co" i najwyżej trzylinijkowy przykład do przeczytania — nie minilekcję. Agent zapisuje temat i lekcja, która go wprowadza, zaczyna się od „pytałeś o to w 4.2".

### Postęp między sesjami

Stan nauki mieszka w `postep/student.json`: ukończone lekcje z Twoją subiektywną oceną trudności, zrobione ćwiczenia, mocne strony, tematy do powtórki. Dzięki temu każda sesja zaczyna się tam, gdzie skończyła poprzednia, a po przerwie dłuższej niż tydzień agent sam zaproponuje krótki quiz, zanim ruszycie dalej.

Tematy do powtórki mają **harmonogram**: pierwsza powtórka następnego dnia, potem po 3, 7, 14 i 30 dniach. Zaliczysz pięć razy — temat znika jako opanowany; potkniesz się — wraca na początek. Na starcie każdej sesji agent sprawdza, co jest „na dziś", i zaczyna od dwóch-trzech pytań, zanim przejdzie do lekcji. To nie egzamin, tylko sposób, żeby to, czego nauczyłeś się w module 2, nadal siedziało w głowie w module 12.

Plik jest Twój — możesz go czytać i edytować. Nie trafia do repozytorium.

## Jedno ćwiczenie = jeden plik

Kurs opiera się na **aplikacjach jednoplikowych** — możliwości, która pojawiła się w .NET 10:

```sh
cd kurs/zadania
dotnet run 02-zmienne.cs
```

Bez projektu, bez `.csproj`, bez solucji, bez Visual Studio. Pierwsza lekcja to jedna linia w jednym pliku:

```csharp
Console.WriteLine("Cześć! To mój pierwszy program.");
```

To jest cały program. Starsze materiały pokazują tę samą rzecz otoczoną `namespace`, `class Program` i `static void Main(string[] args)` — kompilator dopisuje to dziś za Ciebie. Klasa pojawia się w kursie w module 8, gdy jest po co ją wprowadzać, a projekt raz, w lekcji 14.1.

**Konsekwencja:** kurs wymaga **.NET SDK 10.0 lub nowszego**. Na starszym nie zadziała ani jedno ćwiczenie — ale zawiedzie głośno, komunikatem, a nie po cichu błędnym wynikiem.

## Lista komend

Komendy wpisujesz w Claude Code — to **frazy w języku naturalnym**, nie formalne komendy. Agent rozpozna intencję, nawet jeśli sformułujesz to inaczej. Poniżej wersje kanoniczne.

### 🚀 Start i kontynuacja

| Komenda | Co zrobi agent |
| --- | --- |
| `ucz mnie C#` | Start kursu — onboarding albo powitanie i kontynuacja |
| `zacznij lekcję` | To samo, bardziej formalnie |
| `kontynuujemy` | Kolejna lekcja z programu (`kurs/program.md`) |
| `pokaż program kursu` | Wyświetla zawartość `kurs/program.md` |
| `zmień program kursu` | Edycja programu (np. zmiana celu, tempa) |

### 📚 W trakcie lekcji

| Komenda | Co zrobi agent |
| --- | --- |
| `nie rozumiem [konceptu]` | Wraca do podstaw konceptu nowym kątem |
| `daj mi przykład` | Pokazuje minimalny przykład kodu (nie rozwiązanie) |
| `co to znaczy [termin]?` | Wyjaśnia termin przez pytania naprowadzające |
| `powtórzmy tę lekcję` | Wraca do bieżącej lekcji od początku |

### ✏️ Ćwiczenia i review

| Komenda | Co zrobi agent |
| --- | --- |
| `daj mi zadanie` | Generuje ćwiczenie z bieżącej lekcji (3 poziomy) |
| `daj mi więcej zadań` | Dodatkowe ćwiczenia na opanowany koncept |
| `sprawdź moje zadanie` | Sokratejski review kodu z `kurs/zadania/` |
| `skończyłem [rozgrzewkę/główne/gwiazdkę]` | Review konkretnego rozwiązania |
| `nie chce się skompilować` | Wspólne czytanie komunikatu kompilatora |
| `nie działa mi` | Pomoc w debugowaniu — agent pyta o wynik i oczekiwania |
| `pokaż gwiazdkę` | Odsłania zadanie ⚡ (po ukończeniu pozostałych) |

### 🎯 Quizy i powtórki

| Komenda | Co zrobi agent |
| --- | --- |
| `quiz` | Szybki quiz (3 pytania) z ostatnich lekcji |
| `quiz pełny` | Pełny quiz (5-7 pytań) z całości materiału |
| `quiz słabe` | Powtórki na dziś — tematy z `do_powtorki`, których termin nadszedł (harmonogram 1 → 3 → 7 → 14 → 30 dni) |
| `powtórzmy [temat]` | Krótka powtórka konkretnego konceptu |

### 📊 Postęp

| Komenda | Co zrobi agent |
| --- | --- |
| `pokaż postępy` | Podsumowanie ze `student.json` |
| `gdzie skończyliśmy?` | Przypomnienie aktualnej lekcji i ostatniej sesji |
| `co mam do powtórki?` | Lista tematów z pola `do_powtorki` |
| `co umiem najlepiej?` | Lista z pola `mocne_strony` |

### 🔄 Reset i backup

| Komenda | Co zrobi agent |
| --- | --- |
| `zresetuj kurs` | Reset **miękki** — czyści postęp i program (z backupem) |
| `pełny reset kursu` | Reset **pełny** — czyści wszystko, też Twój kod (z backupem) |
| `cofnij reset` | Przywrócenie ostatniego stanu z `postep/archiwum/` |
| `pokaż backupy` | Wypisuje katalogi w `postep/archiwum/` |

### 🛠️ Środowisko i pomoc

| Komenda | Co zrobi agent |
| --- | --- |
| `sprawdź .NET` | Weryfikacja `dotnet --version` (skill `setup-dotnet`) |
| `jak uruchomić kod?` | Odsyła do `kurs/JAK-PISAC-KOD.md`, sekcja 3-4 |
| `lista komend` | **Wyświetla tę listę w czacie** (skill `pomoc`) |
| `pomoc` / `help` / `co mogę zrobić?` | To samo co `lista komend` |
| `krótka pomoc` | Skrócona wersja — tylko najważniejsze |

### ⚙️ Tryb pracy (dla autora kursu)

| Komenda | Co zrobi agent |
| --- | --- |
| `tryb autora` | Włącza tryb modyfikacji lekcji i skilli (wymaga potwierdzenia pełną frazą) |
| `tryb student` | Powrót do trybu nauki (domyślny) |

> 💡 **Wskazówka:** Nie musisz pamiętać dokładnych fraz. „Zrób mi quiz", „wyczyść wszystko", „co robiłam ostatnio" — agent dopyta o szczegóły.

## Wymagania

- **System operacyjny:** macOS, Linux lub Windows. Komendy `dotnet` są identyczne na każdym z nich; różnią się wyłącznie komendy powłoki (`cat` kontra `type`, `ls` kontra `dir`), które agent tłumaczy automatycznie
- **.NET SDK 10.0 lub nowszy** — to twarda granica. Cały kurs opiera się na aplikacjach jednoplikowych (`dotnet run plik.cs`), które pojawiły się dopiero w .NET 10. Sprawdzenie i instalacja przez skill `setup-dotnet`
- **Claude Code** (https://claude.com/code)
- **Edytor tekstu** — rekomendacja: VS Code + rozszerzenie C# Dev Kit (działa identycznie na każdym systemie)

> **Czego NIE potrzebujesz:** Visual Studio. To kilkanaście gigabajtów, tylko na Windows, i nic w tym kursie tego nie wymaga.

## Model

Agent jest przypięty do **Sonnet** — w nagłówku `.claude/agents/csharp-tutor.md`:

```yaml
model: sonnet
```

Wybór podyktowany kosztem: kurs to dziesiątki długich sesji, a instrukcje w skillach są na tyle szczegółowe, że nie wymagają od modelu domyślania się.

Chcesz inny model — zmień tę jedną linię (`opus`, `haiku`, albo `inherit`, żeby użyć modelu bieżącej sesji Claude Code). Kurs był budowany na Sonnecie; na innych modelach powinien działać, bo instrukcje są jawne i nie polegają na niedopowiedzeniach. Jeśli przesiadka coś zepsuje, najbardziej podatne są dwie rzeczy: **trzymanie rytmu 3-krokowego** (model gadatliwszy chętniej tłumaczy, zamiast pytać) i **respektowanie zakazu uruchamiania kodu ucznia**.

## Materiały źródłowe

Treść dydaktyczna powstała na podstawie własnych materiałów autora ze szkoleń „Programista C# i .NET" — programu zajęć i przykładów kodu. Materiały te leżą poza repozytorium i nie są publikowane; do repozytorium trafia wyłącznie treść napisana od nowa.

Przykłady zostały **zmodernizowane**: oryginały pisano dla .NET Framework i Visual Studio na Windows, tutaj działają na .NET 10 i na każdym systemie. Aneks [`wiedza/AKTUALIZACJE.md`](wiedza/AKTUALIZACJE.md) opisuje, co się między jednym a drugim zmieniło — i co uczeń zobaczy, gdy trafi w internecie na starszy poradnik.

Szczegóły w pliku [`NOTICE.md`](NOTICE.md).

## Licencja

[MIT](LICENSE) — Copyright (c) 2026 Marian Witkowski.

Wolno używać, zmieniać, tłumaczyć i rozpowszechniać, także komercyjnie, **pod warunkiem zachowania informacji o autorstwie** (noty copyright i treści licencji) w każdej kopii i w każdej istotnej części. Wyjaśnienie po polsku, co to znaczy w praktyce — w pliku [`NOTICE.md`](NOTICE.md).
