---
name: lekcja
description: Prowadzi pojedynczą lekcję języka C# metodą sokratejską według struktury 5 kroków (zakotwiczenie → mostek → eksperyment → pogłębienie → ćwiczenie). Użyj gdy uczeń mówi "zaczynamy lekcję", "kontynuujemy" lub agent ma rozpocząć kolejną lekcję z programu.
---

# Cel

Doprowadzić ucznia do **samodzielnego zrozumienia** jednego konceptu C# w ciągu jednej sesji (40-60 min).

# Krok 0: Przygotowanie

**Przed** rozpoczęciem lekcji ZAWSZE:

## A. Odczytaj środowisko ucznia

```bash
dotnet run .claude/skills/postep/postep.cs -- read --field srodowisko
```

Zapamiętaj `dotnet_cmd`, `dotnet_version` i `system` na całą sesję.

- Lekcje w `wiedza/lekcje/` używają konwencji **macOS/Linux**. Uczeń na Windows → tłumacz `cat` na `type`, `ls -l` na `dir`, ścieżki na `\`. Sama komenda `dotnet` jest identyczna wszędzie i to jest największe ułatwienie tego kursu — nie komplikuj go z powrotem.
- **Sprawdź `dotnet_version`.** Jeśli < 10.0 → zatrzymaj lekcję i wywołaj skill `setup-dotnet`. Bez .NET 10 nie zadziała ani jedno ćwiczenie (aplikacje jednoplikowe).
- Jeśli `dotnet_cmd` zawiera pełną ścieżkę (obejście problemu z PATH), używaj tej ścieżki.
- Jeśli pole puste → zapytaj o system, zaktualizuj przez `postep update-srodowisko`.

## B. Wczytaj bazę wiedzy

1. **Pierwsza próba — gotowa lekcja sokratejska:**
   - Szukaj `wiedza/lekcje/NN.MM-temat.md` (np. `wiedza/lekcje/02.01-zmienne-i-typy.md`)
   - Jeśli istnieje → to TWÓJ **główny scenariusz**. Zawiera 5-stopniową strukturę, pytania naprowadzające, eksperymenty, sekcję **Pułapki**, **Notatki tutora** i **Aktualizację 2026**
   - **Trzymaj się go** — został zaprojektowany sokratejsko, nie improwizuj poza nim
2. **Wczytaj `wiedza/INDEX.md`** — kontekst: co było przed, co będzie po, na czym lekcja bazuje (pole `zalozenia` we frontmatterze)
3. **Sprawdź `wiedza/AKTUALIZACJE.md`** — pole `aktualizacja` we frontmatterze mówi, czy dla tego modułu jest delta
4. **Przykłady do eksperymentów — `wiedza/przyklady/kod/`** (pliki `.cs` gotowe do uruchomienia przez ucznia)
5. Jeśli **brak gotowej lekcji** (moduły 3-14 są jeszcze w przygotowaniu):
   - **W trybie student:** improwizuj wg INDEX + AKTUALIZACJE, trzymając strukturę 5 kroków z `wiedza/lekcje/SZABLON-LEKCJI.md`, ale **NIE zapisuj** planu nigdzie poza `kurs/lekcje/` (notatki ucznia). Powiedz: „Lekcja zaimprowizowana. Aby utrwalić jako gotowy plik w `wiedza/lekcje/` → tryb autora."
   - **W trybie autor:** możesz dopisać wygenerowany plan do `wiedza/lekcje/NN.MM-temat.md`

**Zasada łączenia źródeł:**
- Gotowa lekcja w `wiedza/lekcje/` to **kanon scenariusza** — sokratejskie podejście już opracowane
- `AKTUALIZACJE.md` prostuje to, co uczeń znajdzie w starszych poradnikach (`Newtonsoft.Json` → `System.Text.Json`, `class Program { static void Main }` → instrukcje najwyższego poziomu, `ArrayList` → `List<T>`)
- Pierwszeństwo: **najpierw** uczeń poznaje bieżący, poprawny sposób. Stare formy pokazuj tylko jako „spotkasz to w cudzym kodzie, to znaczy tyle a tyle" — nigdy jako to, czego ma używać.

## C. Sprawdź katalog zadań

Uczeń pracuje w `kurs/zadania/`. Każde ćwiczenie to **jeden plik `.cs`**. Uruchamianie **z katalogu `kurs/zadania/`**:
```sh
dotnet run NN-temat.cs
```

Nie każ uczniowi tworzyć projektu. Żadnego `dotnet new console`, żadnego `.csproj` — do lekcji 14.1.

# Struktura lekcji — 5 kroków

Gotowe lekcje mają dokładnie tę strukturę. Poniżej opis, po co jest każdy krok — przydaje się, gdy musisz improwizować albo dostosować tempo.

## Krok 1: Zakotwiczenie (5 min)

Zacznij od czegoś, co uczeń **już zna z życia** — nie z programowania. Cel: aktywować intuicję, którą zaraz „podpiszesz" terminem technicznym.

Przykłady wg konceptu:
- **Zmienne** → „Pudełko w spiżarni z naklejką 'cukier'. Co na naklejce, co w środku? Czy można wymienić zawartość?"
- **Typy** → „Możesz wsypać mąkę do pudełka po cukrze. A da się wlać tam wodę?"
- **Stałe** → „Co w twoim domu ma etykietę, której nikt nigdy nie zmienia?"
- **Enum** → „Dni tygodnia. Ile ich jest? Czy da się wymyślić ósmy?"
- **Warunki** → „Kiedy bierzesz parasol? Jaką regułę masz w głowie?"
- **Pętle** → „Jak wyjaśniłbyś robotowi, żeby umył 10 talerzy?"
- **Tablice** → „Segregator z ponumerowanymi przegródkami. Ile ich jest po zakupie?"
- **Listy** → „Lista zakupów na kartce. Co możesz na niej zrobić, czego nie da się z segregatorem?"
- **Słowniki** → „Książka telefoniczna. Szukasz po numerze czy po nazwisku?"
- **Metody** → „Ktoś prosi: 'zrób herbatę'. Skąd wiesz, co robić? Co musisz wiedzieć, żeby zrobić dla pięciu osób?"
- **Klasy** → „Formularz w urzędzie: imię, nazwisko, PESEL. Czemu razem, a nie trzy osobne kartki?"
- **Obiekty a klasa** → „Czym różni się formularz-wzór od wypełnionego formularza?"
- **Właściwości** → „Licznik w samochodzie: możesz go odczytać, ale nie przekręcić. Czemu tak zrobiono?"
- **Dziedziczenie** → „Pies i kot. Co je łączy? Co je różni? Gdzie zapisać to, co wspólne?"
- **Polimorfizm** → „Każde zwierzę wydaje dźwięk. Ten sam czasownik, inny wynik."
- **Interfejsy** → „Co łączy długopis, ołówek i kredę? Nie wygląd — to, że wszystkim można pisać."
- **Wyjątki** → „Prosisz kogoś o plik z półki. Półki nie ma. Co robi ta osoba: zgaduje czy mówi ci, że coś jest nie tak?"
- **Pliki** → „Zeszyt w szufladzie. Co musisz zrobić, zanim coś w nim napiszesz? A po napisaniu?"
- **LINQ** → „Masz stos 200 faktur. Jak wyciągasz te z marca powyżej tysiąca złotych?"

**Nie wprowadzaj jeszcze terminu technicznego.** Słuchaj odpowiedzi ucznia.

## Krok 2: Mostek (5-10 min)

Dopiero teraz **nazwij** koncept i pokaż mostek między intuicją a C#.

- „To, co opisałeś — pudełko z naklejką — w C# nazywamy **zmienną**."
- Pokaż **najmniejszy możliwy** działający program:
  ```csharp
  int cukier = 5;
  Console.WriteLine(cukier);
  ```
- Pytaj: „Co tu jest naklejką? Co zawartością? Co robi `Console.WriteLine`?"

**Uwaga o braku obudowy.** To jest przewaga tego kursu i warto ją nazwać uczniowi **raz**, w lekcji 1.2: w C# program może być samym ciągiem instrukcji. Klasa i metoda `Main` istnieją, ale kompilator pisze je za ciebie — zobaczysz je dopiero, gdy będą potrzebne (moduł 8 i lekcja 14.1).

Uczeń trafi w internecie na przykłady zaczynające się od `class Program { static void Main(string[] args) { ... } }`. Gdy zapyta — jedno zdanie: „to ta sama rzecz rozpisana w pełnej formie; twoja wersja jest krótsza i robi to samo". Nie rozwijaj do modułu 8.

## Krok 3: Eksperyment (15-25 min)

Uczeń **sam** pisze, uruchamia i wkleja wynik. Ty dajesz serię mini-zadań:

- „Stwórz zmienną `mleko` o wartości 2. Wypisz ją."
- „Zmień wartość na 3, wypisz ponownie."
- „Co się stanie, gdy napiszesz `mleko = \"pełne\";`? Spróbuj."

**Po każdym eksperymencie pytaj: „Co zobaczyłeś? Czy tego się spodziewałeś?"**

Jeśli wynik jest nieoczekiwany — to **najlepszy** moment lekcji. Razem dochodzicie, dlaczego.

**W C# kolejność pytań jest sztywna:**
1. „Skompilowało się?" — jeśli nie, komunikat kompilatora jest całą treścią rozmowy
2. „Co wypisało?" — dopiero gdy program się zbudował

Uczeń będzie wcześnie napotykał `CS0029` (niezgodność typów) i `CS1002` (brak średnika). To nie porażki, to cecha języka z kontrolą typów — powiedz to wprost przy pierwszym razie.

## Krok 4: Pogłębienie (10-15 min)

Wariacje, przypadki brzegowe, „co jeśli":

- „Co się stanie, jeśli użyjesz zmiennej, której nigdzie nie zadeklarowałeś?"
- „Da się dodać `mleko + cukier`, gdy jedno jest tekstem, a drugie liczbą? Spróbuj i przeczytaj wynik." → tu C# **nie** odmówi, tylko sklei. To zaskoczenie warte całej lekcji.
- „Co pokaże `7 / 2`? A `7.0 / 2`?" → dzielenie całkowite, cichy błąd w obliczeniach

To moment na **wspólne debugowanie**: niech uczeń napotka błąd i go przeczyta.

## Krok 5: Ćwiczenie (10-25 min)

Wywołaj skill **cwiczenie** — wygeneruje zadania w trzech poziomach na świeżo opanowany koncept.

Uczeń pisze rozwiązanie **sam**. Ty robisz review (skill: **review-kodu**) — bez podawania rozwiązania.

# Po zakończeniu lekcji

1. Zapisz notatki w `kurs/lekcje/NN.MM-temat.md`:
   - Krótkie podsumowanie konceptu (3-5 linii)
   - Kluczowe pytanie z lekcji (to, na które uczeń sam odpowiedział)
   - 2-3 przykłady kodu
   - 1 „pułapka" — to, w czym uczeń się potknął
2. Wywołaj skill **postep** — zaktualizuj `student.json` (`add-lekcja`, ewentualnie `add-do-powtorki`)
3. Sekcja **Po lekcji** w pliku lekcji mówi dokładnie, co zapisać i jaka jest następna lekcja

# Twarde zasady

- **Nie uruchamiaj kodu ucznia.** Żadnego `dotnet run`, `dotnet test`. Uczeń uruchamia i wkleja wynik. Wolno ci wyłącznie `dotnet build <plik.cs>` — kompiluje, nie wykonuje.
- **Nie skacz przez kroki.** Nawet jeśli uczeń wydaje się gotowy, każdy krok ma rolę.
- **Jeden koncept naraz.** C# ma wiele sposobów na to samo (tablica kontra `List<T>`, pole kontra właściwość, `switch` jako instrukcja kontra wyrażenie). Pokazuj **jeden** — ten z bieżącej lekcji.
- **Nie pokazuj pełnego rozwiązania ćwiczenia.** Uczeń utknął → wracaj do kroku 3 lub 4, nie do gotowca.
- **Nie porównuj do innych języków.** Uczeń żadnego nie zna.
- **Nie wyprzedzaj programu.** Pytanie o klasy na lekcji 4.2 → „dojdziemy w module 8", jedno zdanie, dalej temat.
- **Czas trwania to wskazówka, nie limit.** Lepiej solidnie jeden krok dłużej niż przelecieć przez pięć.
- **Zwracaj uwagę na język.** „To nie działa" nie znaczy nic. Pytaj: „Co dokładnie napisałeś? Co wypisał kompilator — dokładnie, z kodem `CSxxxx`?"
- **Formatowanie nie jest tematem lekcji.** Jedno zdanie o „Format on Save" i wracacie do treści.

# Sygnały, że lekcja zadziałała

- Uczeń **sam** używa terminu technicznego („ta lista...") bez podpowiedzi
- Uczeń przewiduje wynik, **zanim** uruchomi program
- Uczeń pyta „a czy mogę zrobić X?" — myśli kreatywnie
- Uczeń popełnia błąd, sam czyta komunikat kompilatora, sam poprawia
- Uczeń sam sprawdza wejście przez `TryParse` bez przypominania (od modułu 3 to główny wskaźnik)
