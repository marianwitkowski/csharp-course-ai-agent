# Weryfikacja lekcji 7.4 i kroku H z 14.1 w prawdziwym VS Code

**Po co:** lekcja 7.4 (debugger) i krok H w 14.1 opisują interfejs VS Code — F9, F5, F10, F11,
panele Variables i Call Stack — a nikt jeszcze nie nacisnął tych klawiszy na kursowym pliku.
Testy behawioralne biegły z terminala. Ten protokół zamyka lukę w ok. 15 minut.

**Największa niewiadoma:** czy F5 na **aplikacji jednoplikowej** (`dotnet run plik.cs`, bez
`.csproj`) uruchamia debugger z C# Dev Kit. Źródła z 2025 mówią, że tak (Run and Debug →
wybór debuggera C#, SDK tworzy tymczasowy projekt); oficjalne ogłoszenie z maja 2025 wymieniało
to jako planowane. Rozstrzyga jedno naciśnięcie F5.

**Jak wypełniać:** odhaczaj `[ ]`, a w polach „zapisz:" wpisuj **dosłownie** to, co widzisz
(tekst listy wyboru, nazwa wpisu na stosie, numer linii). Nie poprawiaj niczego w lekcji po drodze —
zbierz obserwacje, resztę zrobi tutor autorski na ich podstawie (sekcja „Co zmienić" na końcu).

---

## 0. Warunki wstępne

- [ ] VS Code z rozszerzeniem **C# Dev Kit** (`ms-dotnettools.csdevkit`; instaluje też rozszerzenie C#).
      Sprawdzenie z terminala: `code --list-extensions | grep -i dotnettools`
      zapisz wersje (Extensions → C# → wersja): C# ........... C# Dev Kit ...........
- [ ] `dotnet --version` → 10.0.x (zapisz: ...........)
- [ ] VS Code otwarty na folderze repozytorium: `code .` z katalogu kursu (nie na pojedynczym pliku).
- [ ] Przy pierwszym otwarciu pliku `.cs` rozszerzenie dociąga narzędzia — poczekaj, aż pasek stanu
      na dole przestanie pokazywać ładowanie.
- [ ] Wersje do raportu: VS Code (Code → About): ..........., system: ...........

## 1. Plik jednoplikowy — lekcja 7.4, krok 3.A (pętla)

Plik z kroku 2 lekcji: utwórz `kurs/zadania/21-debugger.cs`:
```csharp
int suma = 0;
for (int i = 1; i <= 5; i++)
{
    suma = suma + i;
}
Console.WriteLine(suma);
```
- [ ] 1.1 Zwykłe uruchomienie z `kurs/zadania`: `dotnet run 21-debugger.cs` → `15`.
- [ ] 1.2 W VS Code kliknij w linię `suma = suma + i;` (linia 4), naciśnij **F9**.
      Czy przy numerze linii pojawiła się czerwona kropka? tak / nie
- [ ] 1.3 Naciśnij **F5**. Zapisz, co się stało — jedno z:
      - (a) program zatrzymał się, linia 4 podświetlona → idź do 1.4
      - (b) pojawiła się lista wyboru („Select debugger" / „Wybierz środowisko" itp.).
        Zapisz **wszystkie pozycje listy** dosłownie: ...........................................
        Wybierz **C#**. Zatrzymał się? tak / nie. Czy przy kolejnym F5 lista wraca? tak / nie
      - (c) komunikat / błąd. Zapisz dosłownie: ...........................................
      - (d) program wykonał się bez zatrzymania (w konsoli `15`, brak podświetlenia)
- [ ] 1.4 Panel **Variables** (lewa strona): zapisz, co pokazuje przy pierwszym zatrzymaniu:
      `i` = ....... `suma` = ....... (lekcja oczekuje `1` i `0` — zatrzymanie **przed** linią)
- [ ] 1.5 **F10** raz. `suma` = ....... Która linia jest teraz podświetlona (numer)? .......
      (lekcja mówi: „do `}` albo do `i++` — zależnie od wersji debuggera")
- [ ] 1.6 **F5** raz. Zatrzymał się znowu w linii 4? tak / nie. `i` = ....... `suma` = .......
- [ ] 1.7 F5 jeszcze trzy razy — licz zatrzymania. Ile razem (z pierwszym)? .......
      (lekcja: 5). Po ostatnim F5 program dokończył się i wypisał `15`? tak / nie
      Gdzie pojawił się wynik: konsola VS Code (Debug Console) / Terminal / osobne okno? ...........
- [ ] 1.8 **Shift+F5** — debugger zatrzymany, podświetlenie zniknęło? tak / nie

## 2. Plik jednoplikowy — lekcja 7.4, krok 3.B (wejście do metody)

- [ ] 2.1 `cp wiedza/przyklady/zepsute/18-metody-zepsute.cs kurs/zadania/` i zwykłe
      `dotnet run 18-metody-zepsute.cs` → `OK      Suma(3, 4) = 7` i `BŁĄD    Srednia(3, 4): jest 3, miało być 3,5`.
- [ ] 2.2 Otwórz plik w VS Code, **F9** w linii 14: `return (a + b) / 2;`. **F5**.
      Zatrzymał się w linii 14? tak / nie (jeśli lista wyboru — jak w 1.3b)
- [ ] 2.3 **Variables**: `a` = ....... `b` = ....... (lekcja: 3 i 4)
- [ ] 2.4 Panel **Call Stack**: przepisz **dosłownie** wszystkie wpisy, od góry:
      1. ...........................................
      2. ...........................................
      (lekcja: na górze `Srednia`, pod nim `Main` — „nazwa może być dłuższa i dziwna")
- [ ] 2.5 Najedź kursorem na `a + b` w linii 14. Pokazało wartość? tak / nie, jaką? .......
      Najedź na `(a + b) / 2`. Pokazało? tak / nie, jaką? ....... (lekcja: 7 i 3)
      Jeśli nie pokazuje: czy zaznaczenie fragmentu myszą + najechanie pomaga? tak / nie
- [ ] 2.6 **F10** raz. Gdzie jest podświetlenie: nadal linia 14 / klamra `}` w linii 15 / linia 5
      (`SprawdzDouble`) / inna: .......
- [ ] 2.7 Naciskaj **F10**, aż wyjdziesz z metody. Ile naciśnięć od 2.6 do powrotu do linii 5? .......
      Po powrocie Call Stack ma jeden wpis? tak / nie
- [ ] 2.8 **Shift+F5**. Napraw `/ 2` → `/ 2.0`, zwykłe `dotnet run` → dwa `OK`? tak / nie

## 3. Plik jednoplikowy — lekcja 7.4, krok 3.C (F10 kontra F11)

- [ ] 3.1 F9 w linii 14 (usuwa breakpoint), F9 w linii 4: `Sprawdz("Suma(3, 4)", Suma(3, 4), 7);`. **F5**.
      Zatrzymał się w linii 4? tak / nie
- [ ] 3.2 **F10**. Podświetlona linia: ....... (lekcja: 5). W konsoli pojawiło się
      `OK      Suma(3, 4) = 7`? tak / nie. Gdzie (Debug Console / Terminal)? ...........
- [ ] 3.3 W linii 5 **F11**. Gdzie jesteś: `Srednia` (linia 14) / `SprawdzDouble` (linia 31) / inne: .......
      (lekcja: `Srednia`, bo wynik liczy się przed wywołaniem `SprawdzDouble`)
- [ ] 3.4 Naciskaj **F11**, aż wyjdziesz z `Srednia`, i jeszcze raz. Gdzie jesteś? .......
      (lekcja: w `SprawdzDouble`). Ile naciśnięć F11 od 3.3? .......
- [ ] 3.5 Czy w którymś momencie F11 wszedł do kodu biblioteki (np. `Console.WriteLine`,
      plik spoza kursu, komunikat o braku źródeł)? tak / nie. Jeśli tak — co pokazał VS Code? ...........
- [ ] 3.6 **Shift+F5**.

## 4. Projekt z `.csproj` — lekcja 14.1, krok H

Poza repozytorium:
```
cd ~
dotnet new console -o proba-debugger
code proba-debugger
```
- [ ] 4.1 W `Program.cs` dopisz pod `Console.WriteLine("Hello, World!");` drugą linię
      `Console.WriteLine("druga");`, **F9** na pierwszej linii, **F5**.
      Zatrzymał się? tak / nie. Lista wyboru? tak / nie (zapisz pozycje: ...........)
      Pytanie o konfigurację / `launch.json`? tak / nie (zapisz: ...........)
- [ ] 4.2 **F10** raz → podświetlona druga linia? tak / nie. Call Stack, wpis na górze, dosłownie: ...........
- [ ] 4.3 **Shift+F5**. Usuń folder: `rm -rf ~/proba-debugger`.

## 5. Porządek

- [ ] `kurs/zadania/21-debugger.cs` i `18-metody-zepsute.cs` zostawić albo usunąć — są w `.gitignore`,
      do repo nie trafią. `git status` w repo ma być czysty.

---

## Co zmienić w lekcji w zależności od wyniku

| Obserwacja | Zmiana |
| --- | --- |
| 1.3a — F5 zatrzymuje od razu | Nic. Zdanie o „liście do wyboru" w 3.A zostaje jako zabezpieczenie. |
| 1.3b — lista wyboru, po wyborze C# działa | Dopisać do 3.A dosłowne brzmienie listy i którą pozycję wybrać; jeśli lista wraca przy każdym F5 — jedno zdanie „za każdym razem wybierz to samo". |
| 1.3c/d — nie startuje | Opcja B: gotowy projekt `wiedza/przyklady/debugger/` z `.csproj` (F5 na projekcie jest udokumentowane), 7.4 przepisana na ten folder; krok H w 14.1 do skrócenia. |
| 1.5 / 2.6 — gdzie ląduje F10 | Wpisać do lekcji konkretną linię zamiast „zależnie od wersji". |
| 2.4 — nazwa `Main` na stosie | Wpisać dosłowną nazwę (np. `Program.<Main>$`) do 3.B, żeby uczeń ją rozpoznał. |
| 2.5 — brak wartości po najechaniu | Zamienić „najedź kursorem" na Watch albo na `Console.WriteLine` w tym miejscu; jeśli działa po zaznaczeniu — dopisać „zaznacz wyrażenie". |
| 3.3 / 3.4 — inna kolejność wejść F11 | Poprawić opis kolejności w 3.C na obserwowaną. |
| 3.5 — F11 wchodzi w bibliotekę | Rozszerzyć pułapkę „F11 na `Console.WriteLine`" o to, co dokładnie pokazuje VS Code i jak wyjść (Shift+F11). |
| 4.1 — projekt: F5 działa bez pytań | Nic; krok H zostaje. |
| 4.1 — projekt: pyta o konfigurację | Dopisać do H, co wybrać; rozważyć `launch.json` w gotowym projekcie (opcja B). |

## Raport do wklejenia tutorowi autorskiemu

```
Wersje: VS Code ......, C# ......, C# Dev Kit ......, .NET ......, system ......
1.3: (a/b/c/d) ......  lista: ......
1.4: i=.. suma=..   1.5: suma=.. linia ..   1.7: zatrzymań .. wynik w: ......
2.3: a=.. b=..   2.4: stos: 1) ...... 2) ......   2.5: a+b=.. (a+b)/2=..   2.6: linia ..   2.7: .. naciśnięć
3.2: linia .. OK w: ......   3.3: ......   3.4: ...... po .. F11   3.5: ......
4.1: ......   4.2: linia .. / stos: ......
Uwagi: ......
```

---

## Przebieg 2026-09-08 (sterowany z sesji Claude Code: klawisze przez System Events, zrzuty ekranu)

**Wersje:** VS Code 1.134.0, C# 2.140.9, C# Dev Kit 3.20.199, .NET SDK 10.0.400, macOS 15.2 (Apple Silicon).
**Stan:** przerwany przy 9% baterii i wygaszonym ekranie; części 2-4 do dokończenia.

| Krok | Wynik | Obserwacja |
| --- | --- | --- |
| 0 | ✅ | Rozszerzenia były zainstalowane; pasek stanu Dev Kita pokazuje „No Solution" i „Sign In". |
| 1.1 | ✅ | `dotnet run 21-debugger.cs` → `15`. |
| 1.2 | ✅ | F9 → czerwona kropka przy linii 4. |
| 1.3 | **(b)** | Lista „Select debugger": `C#` (Suggested), `Uno Platform .NET Debugger`, `More Node.js options...`, `More Mojo options...`, `More C# options...`, `More Python Debugger options...`, `Install an extension for C#...` (pozycje spoza C# zależą od innych rozszerzeń na maszynie). **Żadna pozycja nie jest zaznaczona**: sam Enter nic nie robi; strzałka w dół zaznacza `C#`, strzałka w górę przeskakuje na `Install an extension for C#...` i Enter otwiera Marketplace. Lista wraca przy każdym F5. |
| 1.3 po wyborze C# | ❌ | Powiadomienie „Debug: Waiting for projects to load" i nic więcej (po 60 s). To samo dla pliku z dyrektywą `#:property` na górze. Log `C# Dev Kit.log`: `Failed: Opening a solution ... Cannot find an instance of the Microsoft.VisualStudio.Server.Services.MonoDevelop.SolutionUtilities+SolutionData service.` — Dev Kit nie potrafi tu otworzyć „rozwiązania"; wpis `C#` (typ `dotnet`, z rozszerzenia C#) czeka na system projektów. Nierozstrzygnięte, czy to usterka tej instalacji Dev Kita, czy zachowanie ogólne. |
| serwer języka | — | `C#.log`: zwykły plik kursu ładowany jako luźny plik (`roslyn-canonical-misc/Canonical.csproj`); plik z dyrektywą `#:` — jako aplikacja jednoplikowa. Ustawienia `dotnet.projects.enableFileBasedProgramsWhenAmbiguous` i `dotnet.fileBasedApps.enableAutomaticDiscovery` (włączone w lokalnym `.vscode/settings.json`, po przeładowaniu okna) nie zmieniły klasyfikacji zwykłego pliku. |
| bez Dev Kita | częściowo | Po uruchomieniu VS Code z `--disable-extension ms-dotnettools.csdevkit` lista ma `.NET 5+ and .NET Core` (Suggested) zamiast `C#` — klasyczny debugger `coreclr`, który wymaga `launch.json`. Wynik po wyborze niezaobserwowany (ekran zajęty przez inną aplikację). |
| Dev Kit, przycisk ▷ | nie sprawdzono | Dev Kit dodaje do menu ▷ w nagłówku edytora „Run/Debug project associated with this file" (komendy `csdevkit.debug.fileLaunch`, `csdevkit.debug.noDebugFileLaunch`, ukryte w palecie). To może być właściwe wejście dla plików bez projektu; klik w menu z automatu był zawodny. |
| 4 (projekt) | nie sprawdzono | `~/proba-debugger` utworzony (`dotnet run` → dwie linie); otwarcie w VS Code przerwane przez wygaszony ekran. |

**Wnioski wstępne dla lekcji 7.4:**
1. Zdanie „VS Code pokaże listę do wyboru → wybierz C#" jest prawdziwe, ale trzeba dopisać: **kliknij** `C#` (Enter bez zaznaczenia nic nie robi) i lista wraca przy każdym F5.
2. Na tej maszynie ścieżka F5 → `C#` nie startuje. Do rozstrzygnięcia: (a) ▷ „Debug project associated with this file", (b) projekt z `.csproj` (krok H), (c) czy Dev Kit po zalogowaniu / aktualizacji przestaje zgłaszać błąd `SolutionData`.
3. Jeśli (a)-(c) nie dadzą działającego F5 na pliku, obowiązuje opcja B z odpowiedzi tutora: gotowy projekt `wiedza/przyklady/debugger/` z `.csproj` i `launch.json`, a 7.4 przepisana na ten folder.

### Uzupełnienie 2026-09-08, 15:10–15:13 (bateria 8→6%, przebieg przerwany)

| Krok | Wynik | Obserwacja |
| --- | --- | --- |
| 4.1 projekt `~/proba-debugger`, Dev Kit aktywny | ⚠️ | Dev Kit **działa z projektem**: pasek stanu „Projects: proba-debugger · Debug Any CPU · net10.0 | Debug" (błąd `SolutionData` dotyczy tylko folderu bez projektu). F5 zbudował projekt i uruchomił `vsdbg`, ale program padł na starcie: apphost nie znalazł środowiska .NET — `DOTNET_ROOT` nieustawione, zarejestrowana lokalizacja `/etc/dotnet/install_location_arm64 = /usr/local/share/dotnet` (katalog istnieje, ale bez runtime 10.0), a SDK 10.0.400 siedzi w `~/.dotnet` (instalacja skryptem). |
| diagnoza z terminala | ✅ | `bin/Debug/net10.0/proba-debugger` → „You must install .NET to run this application"; z `DOTNET_ROOT=$HOME/.dotnet` → `Hello, World!` / `druga`. `dotnet run` działa zawsze, bo używa muxera z PATH; pod debuggerem VS Code uruchamia **apphost**, który szuka runtime'u przez `DOTNET_ROOT` / zarejestrowaną lokalizację. |
| 4.1 po relaunchu VS Code z `DOTNET_ROOT` | nie rozstrzygnięto | F9 na linii 1 ✅; F5 nie wystartował w 25 s (prawdopodobnie Dev Kit jeszcze ładował projekt); bateria 6% — koniec przebiegu. |

**Wniosek dla `setup-dotnet`:** gdy `which dotnet` wskazuje `~/.dotnet` (instalacja skryptem dotnet-install), a `/etc/dotnet/install_location*` wskazuje inny katalog, debugowanie w VS Code (i każde uruchomienie zbudowanego pliku wykonywalnego) zawiedzie z „You must install .NET to run this application". Sprawdzenie: `DOTNET_ROOT` ustawione albo runtime w zarejestrowanej lokalizacji. Poprawka dla ucznia: instalacja oficjalnym pakietem `.pkg` (rejestruje lokalizację) **albo** `export DOTNET_ROOT=$HOME/.dotnet` w profilu powłoki i uruchamianie VS Code tak, żeby to widział. Do dopisania w skillu `setup-dotnet` (krok weryfikacji) i w 7.4 (ścieżka awaryjna: komunikat „You must install .NET" = to, nie błąd lekcji).

**Pozostało do sprawdzenia:** (1) F5 na projekcie z poprawnym `DOTNET_ROOT` — oczekiwane zatrzymanie na linii 1; (2) menu ▷ „Debug project associated with this file" na pliku jednoplikowym; (3) wybór `.NET 5+ and .NET Core` bez Dev Kita; (4) czy po naprawie `DOTNET_ROOT` ścieżka F5 → `C#` na pliku nadal wisi na „Waiting for projects to load".

### Dokończenie 2026-09-08, 15:16–15:23 (zasilanie podłączone)

| Krok | Wynik | Obserwacja |
| --- | --- | --- |
| 4.1 projekt, VS Code uruchomione z `DOTNET_ROOT=$HOME/.dotnet` | ✅ | F5 → „Select debugger" (`C#`) → **druga lista** „Select Launch Configuration": `C#: Launch Startup Project`, `C#: proba-debugger`, `C#: Debug Active File` → **trzecia lista** „Select C# Startup Project" (jedna pozycja). Trzy wybory przy pierwszym F5; potem konfiguracja zapamiętana w pasku stanu i F5 startuje od razu. Program pod `vsdbg`: baner licencji, `Hello, World!`, `druga`, `exited with code 0` — wyjście trafia do **Debug Console**, nie do Terminala. |
| 4.1 breakpoint | ✅ | Uwaga: F9 na linii z istniejącym breakpointem **wyłącza** go (pierwszy przebieg przeszedł bez zatrzymania). Po ponownym F9: „Paused on breakpoint" na linii 1, żółte podświetlenie, panel Run and Debug z Variables/Locals, Watch, Call Stack, Breakpoints. |
| 4.2 F10 | ✅ | Podświetlenie na linii 2, „Paused on step", Locals: `args [string[]] = {string[0]}`, w Debug Console `Hello, World!`. **Call Stack, wpis na górze, dosłownie:** `proba-debugger.dll!Program.<Main>$(string[] args)`, pod nim `[External Code]`. |
| plik z kursu po naprawie `DOTNET_ROOT` | ❌ | Folder repozytorium bez projektu: F5 → `C#` → „Debug: Waiting for projects to load" — bez zmian. Ścieżka lekcji 7.4 (F5 na pliku jednoplikowym) **nie działa** z C# Dev Kit 3.20.199 + C# 2.140.9; „No Solution" jest stanem naturalnym repozytorium kursu, więc nie jest to usterka jednej maszyny. |
| nie sprawdzono | — | Menu ▷ „Debug project associated with this file" (Dev Kit) na pliku; wybór `.NET 5+ and .NET Core` bez Dev Kita (wymaga `launch.json`, dla początkującego bez znaczenia). |

**Werdykt:** krok H w 14.1 (projekt z `.csproj`) działa i jest jedyną potwierdzoną drogą. Lekcję 7.4 trzeba przepisać na **gotowy projekt** (opcja B z rozmowy 2026-09-08): folder `wiedza/przyklady/debugger/` z `.csproj` i kodem z `18-metody-zepsute.cs`, kopiowany do `kurs/zadania/debugger/`, otwierany w VS Code jako folder. Do lekcji wchodzą też: trzy listy przy pierwszym F5 (co wybrać, dosłownie), F9 na istniejącym breakpoincie go wyłącza, wyjście programu w Debug Console, nazwa `Program.<Main>$(string[] args)` na stosie. Do `setup-dotnet`: sprawdzenie `DOTNET_ROOT` przy instalacji w `~/.dotnet` (bez tego debugger kończy „You must install .NET to run this application").

Porządki: usunięte `kurs/zadania/{21-debugger,22-test-fbp,18-metody-zepsute}.cs`, lokalny `.vscode/settings.json`, `~/proba-debugger`; `caffeinate` zatrzymany. Folder `.trunk/` utworzyło rozszerzenie Trunk użytkownika (ignorowany przez git) — zostawiony.

### Weryfikacja gotowego projektu `wiedza/przyklady/debugger/` (2026-09-08, 16:25–16:32, C# Dev Kit, `DOTNET_ROOT` ustawione)

Projekt skopiowany do `kurs/zadania/debugger/`, otwarty w VS Code **jako folder** (`code kurs/zadania/debugger`). Dev Kit: „Projects: Debugger · Debug Any CPU · net10.0 | Debug".

| Krok lekcji | Wynik | Obserwacja (dosłownie) |
| --- | --- | --- |
| pierwszy F5 | ✅ | trzy listy: „Select debugger" → `C#` (nic nie zaznaczone, strzałka w dół + Enter albo klik) → „Select Launch Configuration" → `C#: Launch Startup Project` (zaznaczone) → „Select C# Startup Project" → `Debugger /…/Debugger.csproj` (zaznaczone). Potem pasek stanu „C#: Launch Startup Project (debugger)" i kolejne F5 startują bez pytań. |
| 3.B breakpoint w `return (a + b) / 2;` (linia 22) | ✅ | „Paused on breakpoint", linia 22 żółta. Variables → Locals: `a [int] = 3`, `b [int] = 4` (Locals bywa zwinięte — rozwinąć strzałką). Call Stack: `Debugger.dll!Program.<Main>$.__Srednia\|0_1(int a, int b)` (nazwa ucięta w panelu; zaczyna się od `Program.<Main>$`, zawiera `Srednia`), pod nim `Debugger.dll!Program.<Main>$(string[] args)`, pod nim `[External Code]`. Wyjście programu (`15`, `OK      Suma(3, 4) = 7`) w **Debug Console**, nie w Terminalu. |
| wartość wyrażenia | ✅ | Debug Console (pole na dole panelu): wpisane `a` → `3`, `b` → `4`, `a + b` → `7`, `(a + b) / 2` → `3`. Najechanie kursorem niesprawdzone (brak myszy w automacie) — lekcja używa Debug Console. |
| F10 z `return` | ✅ | pierwszy F10 → linia 23 (`}`), drugi → linia 13 (`SprawdzDouble(...)`), stos z jednym wpisem, w Locals dodatkowy wiersz `Program.<Main>$.__Srednia\|0_1 returned` z wartością zwróconą oraz `suma [int] = 15`, `args`. |
| 3.C breakpoint w linii 12, F10 | ✅ | F10 → linia 13; w Debug Console pojawia się `OK      Suma(3, 4) = 7`; Locals: `…__Suma\|0_0 returned`. |
| 3.C Step Into z linii 13 | ✅ | 1: `Srednia` linia 21 (`{`), Locals `a`, `b`; 2: linia 22; 3: linia 23; 4: powrót do linii 13 z `…Srednia\|0_1 returned`; 5: `SprawdzDouble` linia 38 (`{`), Locals `opis = "Srednia(3, 4)"`, `wynik [double] = 3`, `oczekiwane [double] = 3.5`. Kolejność wejść = kolejność obliczania. |
| **F11 na macOS** | ❌→obejście | klawisz F11 jest domyślnym skrótem systemu „Pokaż biurko": okno VS Code zjeżdża z ekranu, krok nie wykonuje się. Obejście zweryfikowane: paleta poleceń → „Debug: Step Into" (albo przycisk ↓ na pasku debugowania / menu Run → Step Into). F10 i F9 działają. |
| F9 na istniejącym breakpoincie | ⚠️ | wyłącza go (przebieg bez zatrzymania). |
| Shift+F5 | ✅ | zatrzymuje sesję. |

**Środowisko:** przy SDK w `~/.dotnet` bez `DOTNET_ROOT` program pod debuggerem kończy się „You must install .NET to run this application" (Terminal), choć `dotnet run` działa. Z `DOTNET_ROOT=$HOME/.dotnet` w środowisku VS Code — działa.

### Wdrożone (2026-09-08, po weryfikacji)

- `wiedza/przyklady/debugger/` — `Debugger.csproj` + `Program.cs` (pętla z 5.2 + zepsute metody z 7.1); CI (`sprawdz-przyklady.sh`) buduje projekt i porównuje wyjście.
- Lekcja 7.4 przepisana na projekt: kopiowanie folderu, File → Open Folder, trzy listy przy pierwszym F5, Locals/Call Stack/Debug Console z dosłownymi tekstami z tego przebiegu, Step Into przez przycisk ↓ (F11 na macOS = „Pokaż biurko"), ścieżka awaryjna z dwoma znanymi przypadkami („Waiting for projects to load" = zły folder; „You must install .NET" = `DOTNET_ROOT`).
- `setup-dotnet`: krok 4B „`DOTNET_ROOT`" dla instalacji skryptem.
- 14.1 krok H: ta sama ścieżka co 7.4 (projekt), odesłanie do `DOTNET_ROOT`.
- INDEX 7.4: kolumna przykładów wskazuje `debugger/`; README: wpis w drzewie.

Niezweryfikowane po przepisaniu: cała lekcja rozmową z tutorem (test behawioralny 7.4 na VS Code), F10 z linii 8 w pętli (lekcja zostawia „do `}` albo do `i++`"), najechanie kursorem na wyrażenie (lekcja używa Debug Console).
