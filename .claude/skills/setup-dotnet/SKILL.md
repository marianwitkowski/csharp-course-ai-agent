---
name: setup-dotnet
description: Sprawdza, czy w systemie zainstalowany jest .NET SDK w wersji 10.0 lub nowszej (macOS / Linux / Windows), prowadzi ucznia przez instalację lub aktualizację, weryfikuje że aplikacje jednoplikowe (dotnet run plik.cs) działają, ustawia edytor i zapisuje środowisko do student.json. Użyj na początku pierwszej sesji lub gdy uczeń zgłasza, że komenda `dotnet` nie działa.
---

# Cel

Doprowadzić ucznia do stanu, w którym **w jego terminalu** działają dwie rzeczy:

1. `dotnet --version` pokazuje **10.0 lub nowszy**
2. `dotnet run przyklad.cs` uruchamia pojedynczy plik `.cs`

**Drugi punkt jest ważniejszy od pierwszego** i musisz go faktycznie sprawdzić, a nie założyć. Cały kurs stoi na aplikacjach jednoplikowych: każde ćwiczenie to jeden plik `.cs` bez projektu i bez `.csproj`. Jeśli to nie działa, kurs nie ruszy.

**Dlaczego 10.0 to twarda granica:** możliwość uruchomienia samego pliku `.cs` pojawiła się dopiero w .NET 10. Na .NET 9 i starszym każde ćwiczenie wymagałoby `dotnet new console` i osobnego katalogu z projektem — czyli dokładnie tej ceremonii, której początkującego chcemy oszczędzić.

Zaleta tej granicy: na starszym SDK zawodzi **głośno** (komunikat, że to nie projekt), nie po cichu. Nie ma ryzyka, że uczeń przez tydzień pracuje na czymś, co daje błędne wyniki.

# Krok 0: rozpoznaj system operacyjny

**Zawsze najpierw** ustal, na jakim systemie pracuje uczeń.

```bash
uname -s 2>/dev/null || echo "Windows (lub PowerShell)"
```

- `Darwin` → macOS
- `Linux` → Linux
- `MINGW*` / `MSYS*` / brak `uname` → Windows

Jeśli niejasne — zapytaj wprost: „Na jakim systemie pracujesz: macOS, Linux czy Windows?"

Na macOS warto znać architekturę (wpływa na wybór instalatora):
```bash
uname -m    # arm64 = Apple Silicon (M1-M4), x86_64 = Intel
```

# Krok 1: sprawdź obecność .NET

Na **wszystkich** systemach komenda jest ta sama:

```bash
dotnet --version
```

**Interpretacja:**
- `10.0.xxx` lub nowszy → **dobrze**, przejdź do kroku 4
- `8.0.xxx` / `9.0.xxx` → **konieczna aktualizacja**, przejdź do kroku 2
- `command not found` / `nie jest rozpoznawany` → krok 2

Pełniejszy obraz, gdy coś jest nie tak:
```bash
dotnet --list-sdks       # które SDK są zainstalowane
dotnet --list-runtimes   # które środowiska uruchomieniowe
```

**Ważne rozróżnienie:** uczeń może mieć zainstalowane samo **środowisko uruchomieniowe** (runtime) bez **SDK**. Wtedy `dotnet --version` działa, ale `dotnet run` nie. Rozstrzyga `dotnet --list-sdks` — jeśli wypisze pustą listę, brakuje SDK, nie .NET-a jako takiego. Powiedz to uczniowi wprost, bo komunikat błędu tego nie tłumaczy.

**Częsty przypadek:** .NET jest zainstalowany, ale nie ma go w `PATH`. Zanim uznasz, że brakuje — sprawdź typowe lokalizacje:
```bash
ls /usr/local/share/dotnet/dotnet 2>/dev/null   # macOS, instalator oficjalny
ls /opt/homebrew/bin/dotnet 2>/dev/null          # macOS, Homebrew na Apple Silicon
ls /usr/share/dotnet/dotnet 2>/dev/null          # Linux, pakiet dystrybucji
ls "$HOME/.dotnet/dotnet" 2>/dev/null            # instalacja skryptem, per użytkownik
```
Jeśli plik istnieje, a `dotnet --version` nie działa → problem z `PATH`, patrz krok 3B.

# Krok 2: instalacja — gałąź wg systemu

**Nie instaluj nic sam.** Daj instrukcję, uczeń wykonuje, potem wracacie do kroku 1 w **nowym terminalu** (PATH musi się odświeżyć).

## 2A — macOS

**Opcja 1 — instalator oficjalny (najprościej dla początkującego):**
1. https://dotnet.microsoft.com/download
2. Pobierz **SDK** (nie „Runtime") dla .NET 10 — `macOS Arm64` dla Apple Silicon, `macOS x64` dla Intel (sprawdziłeś przez `uname -m`)
3. Otwórz `.pkg`, zainstaluj — trafi do `/usr/local/share/dotnet`, a `PATH` ustawi instalator
4. **Zamknij i otwórz nowy terminal**

**Opcja 2 — Homebrew (jeśli uczeń już go ma):**
```bash
brew install --cask dotnet-sdk
```

## 2B — Linux

**Skrypt instalacyjny Microsoftu — zalecany**, bo wersje w repozytoriach dystrybucji bywają stare (a nam potrzeba ≥10.0):

```bash
# 1. Pobierz skrypt
curl -sSL https://dot.net/v1/dotnet-install.sh -o dotnet-install.sh
chmod +x dotnet-install.sh

# 2. Zainstaluj SDK do ~/.dotnet (bez sudo, tylko dla twojego użytkownika)
./dotnet-install.sh --channel 10.0

# 3. Dodaj do PATH
echo 'export PATH=$PATH:$HOME/.dotnet' >> ~/.profile
source ~/.profile
```

To **jedyny wyjątek** od zasady „nie modyfikuj plików konfiguracyjnych ucznia" — i tak wykonuje to uczeń, nie ty. Wyjaśnij mu, co robi każda linia, zanim ją wklei.

Przez menedżer pakietów (**sprawdź wersję po instalacji** — często za stara):
```bash
sudo apt install dotnet-sdk-10.0     # Ubuntu/Debian
sudo dnf install dotnet-sdk-10.0     # Fedora
sudo pacman -S dotnet-sdk            # Arch
```

## 2C — Windows

**Rekomendacja: instalator z dotnet.microsoft.com**

1. https://dotnet.microsoft.com/download → **SDK** dla .NET 10, `Windows x64` (albo `Arm64` na maszynach ARM)
2. Uruchom, klikaj dalej — instalator **sam dodaje .NET do PATH**
3. **Zamknij i otwórz NOWY** PowerShell
4. Sprawdź: `dotnet --version`

Alternatywy: `winget install Microsoft.DotNet.SDK.10` albo `choco install dotnet-sdk` (jeśli uczeń już używa tych menedżerów).

**Nie proponuj instalacji Visual Studio.** To kilkanaście gigabajtów i pełne IDE, którego kurs nie używa — a przy okazji instaluje własne, czasem starsze SDK. Uczeń potrzebuje SDK plus edytora tekstu.

# Krok 3: weryfikacja

## 3A — normalna ścieżka

1. Uczeń **otwiera NOWY terminal** (stary nie zna nowego PATH)
2. Powtarza krok 1: `dotnet --version`

## 3B — .NET zainstalowany, ale komenda nie działa

Problem z `PATH`. Diagnoza:

```bash
echo $PATH                    # macOS/Linux
$env:Path                     # Windows PowerShell
```

Naprawa (macOS/Linux, uczeń wykonuje sam):
```bash
echo 'export PATH=$PATH:/usr/local/share/dotnet' >> ~/.zshrc   # zsh (domyślny na macOS)
echo 'export PATH=$PATH:$HOME/.dotnet' >> ~/.bashrc            # bash, instalacja skryptem
```
Potem **nowy terminal**.

**Obejście awaryjne:** jeśli `PATH` nie chce współpracować, zapisz w `student.json` pełną ścieżkę jako `dotnet_cmd` (np. `/usr/local/share/dotnet/dotnet`) i używaj jej we wszystkich komendach pokazywanych uczniowi. Działa, choć jest niewygodne — wróćcie do naprawy `PATH` przy okazji.

# Krok 4: test „aplikacje jednoplikowe działają" — OBOWIĄZKOWY

To najważniejszy krok w tym skillu. **Nie pomijaj go, nawet gdy `dotnet --version` pokazuje 10.0.**

W repozytorium leży gotowy przykład: `wiedza/przyklady/kod/00-sprawdzenie.cs`. Możesz go **zbudować sam** — to kod autora kursu, nie kod ucznia, a `dotnet build` niczego nie uruchamia:

```bash
dotnet build wiedza/przyklady/kod/00-sprawdzenie.cs
```

Oczekiwany wynik: informacja o udanym budowaniu (`Build succeeded` / `Kompilacja powiodła się`).

**Interpretacja:**

| Co widzisz | Co to znaczy | Co zrobić |
| --- | --- | --- |
| Budowanie powiodło się | Wszystko gotowe | Przejdź do kroku 5 |
| `MSB1003` / „nie znaleziono projektu" | SDK nie zna aplikacji jednoplikowych — czyli jest starszy niż 10.0, mimo tego, co pokazał `--version` | Wróć do kroku 2, sprawdź `dotnet --list-sdks` |
| `command not found` | PATH | Krok 3B |

Następnie **uczeń** uruchamia ten sam plik u siebie (ty nie uruchamiasz niczego):

```sh
dotnet run wiedza/przyklady/kod/00-sprawdzenie.cs
```

Powinien zobaczyć:
```
Środowisko gotowe. Czas zacząć.
```

> **Na świeżo zainstalowanym SDK poprzedzi to jednorazowy komunikat powitalny** — kilka linii o dokumentacji, telemetrii i `dotnet --help`, zakończonych paskiem myślników. To nie jest błąd i nie pojawi się ponownie. Uczeń, który wklei ci całą ścianę tekstu, ma szukać **ostatniej linii**.

Poproś o wklejenie wyniku. To jednocześnie **pierwsze uruchomienie programu w życiu ucznia** — warto to nazwać.

> **Uwaga o pierwszym uruchomieniu:** potrwa kilka sekund dłużej niż kolejne. .NET buduje program przy pierwszym `dotnet run` i zapamiętuje wynik w pamięci podręcznej. Uprzedź o tym, bo inaczej uczeń pomyśli, że coś się zawiesiło.

# Krok 5: edytor

Rekomendacja dla początkujących (działa na wszystkich systemach):
- **VS Code** (https://code.visualstudio.com/) + rozszerzenie **C# Dev Kit** od Microsoftu

Przy pierwszym otwarciu pliku `.cs` rozszerzenie dociągnie narzędzia pomocnicze → **zgódź się**. Da to podpowiedzi, podświetlanie błędów w locie i formatowanie przy zapisie.

Alternatywy:
- **Rider** (JetBrains, bezpłatny do użytku niekomercyjnego)
- **Visual Studio** — tylko Windows, ciężkie; kurs go nie potrzebuje, ale jeśli uczeń już go ma, niech używa
- Jakikolwiek edytor tekstu — **NIE Word, NIE TextEdit w trybie sformatowanym na macOS** (wstawiają znaki, których kompilator nie zrozumie)

**Ustawienie warte pięciu sekund:** „Format on Save" w VS Code (Settings → wyszukaj „format on save"). W repozytorium leży `.editorconfig` z konwencjami kursu — edytor go czyta i kod formatuje się sam przy każdym zapisie.

> **Czemu to ma znaczenie:** w C# nie ma jednego narzędzia, które wymuszałoby format globalnie. `.editorconfig` plus „Format on Save" to standard świata .NET i jedyny sposób, żeby wcięcia nigdy nie stały się tematem rozmowy na lekcji.

# Krok 6: instrukcja workflow

**Zawsze** na koniec setupu wskaż uczniowi plik `kurs/JAK-PISAC-KOD.md`:

> "Zanim zaczniemy pierwszą lekcję — otwórz w edytorze plik `kurs/JAK-PISAC-KOD.md` i przeczytaj go. To 5 minut, a wyjaśnia: gdzie zapisywać kod, jak uruchamiać programy, jak czytać komunikaty kompilatora, plus różnice komend między macOS/Linux/Windows. Daj znać, gdy przeczytasz."

Nie idź dalej, dopóki uczeń nie potwierdzi.

# Mapa komend wg systemu — ściąga dla agenta

| Co | macOS / Linux | Windows PowerShell |
| --- | --- | --- |
| Wersja SDK | `dotnet --version` | `dotnet --version` |
| Lista SDK | `dotnet --list-sdks` | `dotnet --list-sdks` |
| Uruchom program | `dotnet run 01-hello.cs` | `dotnet run 01-hello.cs` |
| Sprawdź ścieżkę | `which dotnet` | `Get-Command dotnet` |
| Podgląd pliku | `cat plik.cs` | `type plik.cs` |
| Lista plików | `ls -l` | `dir` |
| Bieżący katalog | `pwd` | `pwd` |
| Zmienna środowiskowa | `export X=y` | `$env:X="y"` |

**Dobra wiadomość:** wszystkie komendy `dotnet ...` są identyczne na każdym systemie. Różnice dotyczą wyłącznie powłoki. To znacznie mniej różnic niż w większości środowisk — i warto to uczniowi powiedzieć, jeśli pracuje na Windows i spodziewa się kłopotów.

# Twarde zasady

- **Nie uruchamiaj instalatorów** za ucznia. To jego maszyna.
- **Nie modyfikuj** `~/.zshrc`, `~/.bashrc`, profilu PowerShella — pokaż komendę, uczeń ją wykonuje.
- **Po instalacji ZAWSZE nowy terminal** — oszczędzi długich poszukiwań „czemu nie działa".
- **.NET < 10.0 → nie zaczynaj kursu.** Na starszym SDK nie zadziała ani jedno ćwiczenie.
- **Krok 4 jest obowiązkowy.** `dotnet --version` mówi o wersji, nie o tym, że aplikacje jednoplikowe działają. Sprawdź to naprawdę.
- **Nie uruchamiaj kodu ucznia** — także tutaj. Jedyne dozwolone wywołania w tym skillu to `dotnet --version`, `dotnet --list-sdks`, `dotnet --info`, `uname` i `dotnet build` na przykładzie autora z kroku 4.
- **Nie proponuj Visual Studio jako wymagania.** SDK plus VS Code wystarczą, działają wszędzie i nie zajmują kilkunastu gigabajtów.

# Zapis środowiska do `student.json`

Po zakończonym setupie ZAWSZE zapisz środowisko (podczas onboardingu może być od razu w `init`, później przez `update-srodowisko`):

```bash
dotnet run .claude/skills/postep/postep.cs -- update-srodowisko \
  --system "macOS" \
  --dotnet-cmd "dotnet" \
  --dotnet-version "10.0.100" \
  --shell "zsh" \
  --edytor "VS Code"
```

**Mapowanie systemu → wartości:**

| System | `dotnet_cmd` | `shell` | Uwagi |
| --- | --- | --- | --- |
| macOS | `dotnet` | `zsh` | pełna ścieżka w `dotnet_cmd` tylko przy kłopocie z PATH |
| Linux | `dotnet` | `bash` / `zsh` | jw. |
| Windows | `dotnet` | `PowerShell` | ścieżki z `\`, komendy powłoki inne |
| WSL | `dotnet` | `bash` | traktuj jak Linux |

`dotnet_version` zapisuj jako pełny numer SDK, np. `10.0.100`. Wyciągnięcie:
```bash
dotnet --version
```

Zapis przez narzędzie `postep` jest atomowy — nie ma ryzyka uszkodzenia `student.json`.

# Zwrotka do agenta-rodzica

Po zakończeniu zwróć krótko:
- `OK: .NET SDK 10.0.x na <system>, aplikacje jednoplikowe działają, edytor: <nazwa> — środowisko zapisane`
- `STARY SDK: 9.0.x — wymaga aktualizacji PRZED startem kursu` (nie zaczynajcie lekcji 1.1)
- `BLOCKED: <co nie działa>`

**Od tego momentu** WSZYSTKIE skille edukacyjne (`lekcja`, `cwiczenie`, `review-kodu`, `quiz`) muszą używać `srodowisko.dotnet_cmd` z `student.json` i konwencji powłoki z `srodowisko.system`.
