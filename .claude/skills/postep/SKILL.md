---
name: postep
description: Atomowo zapisuje plik postep/student.json przez narzędzie postep napisane w C#. Każda modyfikacja przechodzi przez protokół backup + walidacja + atomowa podmiana — agent NIE składa JSON-a samodzielnie i nie edytuje tego pliku ręcznie. Odczyt stanu robi się zwykłym Read, nie przez narzędzie. Użyj po każdej istotnej zmianie stanu ucznia.
---

# Cel

Trzymać **jeden** plik z pełnym stanem ucznia (`postep/student.json`), z gwarancją, że żadna operacja go nie uszkodzi — wszystkie zapisy idą przez deterministyczne narzędzie, **nie przez ręczne składanie JSON-a przez agenta**. Odczyt jest wyjątkiem i robi się go zwykłym `Read` — powód niżej.

# Zasada twarda — kluczowa

**Agent NIGDY nie wykonuje `Write` ani `Edit` na `postep/student.json` bezpośrednio.**

Każdy **zapis** przez:
```bash
dotnet run .claude/skills/postep/postep.cs -- <komenda> [argumenty]
```

Narzędzie wykonuje protokół: odczyt → sprawdzenie wersji schematu → backup → modyfikacja → zapis `.tmp` → walidacja → atomowa podmiana. Agent tylko **woła je z argumentami**.

## Odczyt jest wyjątkiem — rób go `Read`-em

**Do odczytu stanu używaj zwykłego narzędzia `Read` na `postep/student.json`.** Komenda `read` istnieje w narzędziu i jest poprawna, ale nie polegaj na niej jako na jedynej drodze.

Powód: `postep` uruchamia się przez `dotnet`. Gdy `dotnet` nie jest w `PATH` (częstsze, niż się wydaje — instalacja skryptem ląduje w `~/.dotnet`), ratuje cię pełna ścieżka z pola `srodowisko.dotnet_cmd`. Ta ścieżka leży **w środku pliku, którego wtedy nie możesz odczytać**. Odczyt przez narzędzie zawodzi dokładnie w sytuacji, w której jest najbardziej potrzebny.

Odczyt niczego nie modyfikuje, więc pominięcie protokołu nic nie kosztuje. **Czytasz `Read`-em, piszesz `postep`-em.**

Wniosek praktyczny: gdy `dotnet run …postep.cs` zwraca `command not found`, **nie kombinuj** — odczytaj `student.json` przez `Read`, weź z niego `srodowisko.dotnet_cmd` i wołaj narzędzie przez tę ścieżkę do końca sesji.

## Co to jest i dlaczego tak

`postep` to **aplikacja jednoplikowa** w C# (`postep.cs`) — bez projektu, bez `.csproj`, bez zależności spoza biblioteki standardowej. Ta sama technologia, której uczy kurs, użyta do jego własnego narzędzia.

Ważne szczegóły wywołania:

- **`--` po nazwie pliku jest obowiązkowe.** Wszystko przed nim `dotnet run` bierze dla siebie; wszystko po nim trafia do programu. Bez `--` komenda `read` zostanie zjedzona przez `dotnet run` i dostaniesz komunikat o nieznanej opcji.
- **Wołaj z dowolnego miejsca.** Narzędzie samo szuka w górę katalogu zawierającego `wiedza/` i `.claude/`, więc działa tak samo z korzenia repozytorium jak z `kurs/zadania`. Sama **ścieżka do `postep.cs`** musi być poprawna względem miejsca, z którego wołasz — najprościej wołaj z korzenia repozytorium.
- **Pierwsze wywołanie potrwa kilka sekund** (budowanie), kolejne są natychmiastowe — wynik siedzi w pamięci podręcznej SDK, poza repozytorium.
- **Awaryjnie:** `-root <katalog>` wskazuje katalog projektu wprost, gdy automatyczne szukanie zawiedzie.

To **jedyne** miejsce, w którym wolno ci uruchomić program w tym repozytorium, i dotyczy narzędzia kursu, nie kodu ucznia. Zakaz uruchamiania kodu ucznia obowiązuje bez zmian.

> **Uwaga:** `postep` to narzędzie kursu, nie materiał do nauki. Uczeń nigdy go nie uruchamia ani nie czyta — robisz to wyłącznie ty. Nie omawiaj go na lekcji, nawet gdy jesteście przy module 12 i wygląda na dobry przykład pracy z JSON-em.

# Schemat student.json (schema_version 2)

```json
{
  "schema_version": 2,
  "imie": "Anna",
  "cel": "praca",
  "tempo_godz_tydz": "2-5",
  "sciezka": "pelna",
  "rozpoczeto": "2026-08-30",
  "ostatnia_sesja": "2026-08-30",
  "liczba_sesji": 3,
  "aktualna_lekcja": "4.1",
  "srodowisko": {
    "system": "macOS",
    "dotnet_cmd": "dotnet",
    "dotnet_version": "10.0.100",
    "shell": "zsh",
    "edytor": "VS Code"
  },
  "ukonczone_lekcje": [
    {"id": "1.1", "data": "2026-08-30", "trudnosc_subiektywna": 2}
  ],
  "ukonczone_cwiczenia": [
    {"lekcja": "1.1", "poziom": "warmup", "data": "2026-08-30"}
  ],
  "mocne_strony": ["czytanie komunikatów kompilatora"],
  "do_powtorki": [
    {"temat": "różnica między polem a właściwością", "lekcja": "8.3", "data_zauwazenia": "2026-08-31", "poziom": 0, "next_review": "2026-09-01"}
  ],
  "notatki_tutora": ["Anna lubi konkretne przykłady z życia", "parking: lista (pytał w 2.1)"]
}
```

**`sciezka`** — `pelna` (domyślnie) albo `skrocona` (uczeń zna inny język; moduły 2-7 w trybie skróconym — patrz skill `lekcja`). Zmiana: `set --field sciezka --value pelna`.

**`do_powtorki` ma harmonogram.** `poziom` 0-4 i `next_review` (data) — narzędzie liczy je samo w `add-do-powtorki` i `review-do-powtorki`; agent nigdy nie ustawia ich ręcznie. Odstępy: 1 → 3 → 7 → 14 → 30 dni; po piątej udanej powtórce temat znika jako opanowany.

**Plik w schemacie 1 jest migrowany automatycznie** przy pierwszym zapisie: dostaje `sciezka: "pelna"`, a stare wpisy `do_powtorki` — `poziom: 0` i `next_review` na dziś (czyli od razu są zaległe).

**Pola z nowszego schematu są zachowywane.** Narzędzie trzyma stan jako drzewo JSON, a nie jako klasę z polami — klucze, których nie zna, przechodzą przez odczyt i zapis nietknięte. Plik z `schema_version` wyższą niż obsługiwana jest odrzucany, a nie nadpisywany.

**`dotnet_version` zapisuj jako pełny numer SDK** — np. `10.0.100`. To pole jest sprawdzane przed każdą lekcją (kurs wymaga ≥10.0).

# Komendy

Wszystkie wykonują pełen protokół atomowy. **Zawsze sprawdź kod wyjścia** — niezerowy = stary plik nietknięty.

W przykładach poniżej `P` skraca zapis:
```bash
P() { dotnet run .claude/skills/postep/postep.cs -- "$@"; }
```

## Inicjalizacja (po onboardingu)

```bash
dotnet run .claude/skills/postep/postep.cs -- init \
  --imie "Anna" \
  --cel "hobby" \
  --tempo "2-5" \
  --system "macOS" \
  --dotnet-cmd "dotnet" \
  --dotnet-version "10.0.100" \
  --shell "zsh" \
  --edytor "VS Code" \
  --sciezka "pelna"
```

Tworzy plik z danymi z onboardingu + **pełen snapshot środowiska**. Listy puste, `liczba_sesji=1`, `aktualna_lekcja="1.1"`. `--sciezka` to `pelna` (domyślnie) albo `skrocona` — wynik diagnostyki wejściowej z onboardingu (agent, sekcja „Onboarding").

Błąd, jeśli plik już istnieje (ochrona przed nadpisaniem).

## Odczyt

```bash
P read
P read --field aktualna_lekcja
P read --field srodowisko
P read --field srodowisko.dotnet_cmd
P read --field do_powtorki
```

Bez `--field` dostajesz plik bajt w bajt.

## Ustawienie pola

```bash
P set --field aktualna_lekcja --value "4.2"
P set --field srodowisko.edytor --value "Rider"
```

`set` działa **tylko na polach tekstowych**. Liczby (`liczba_sesji`) i listy mają własne komendy — celowo, żeby literówka nie zmieniła typu pola.

## Dopisanie ukończonej lekcji

```bash
P add-lekcja --id "4.1" --trudnosc 3
```

`--trudnosc` to 1-5 (subiektywna ocena ucznia: „1 = banalne, 5 = bardzo trudne"). Pytaj po lekcji. Narzędzie samo aktualizuje `ostatnia_sesja`. Powtórzenie komendy dla tej samej lekcji **nadpisuje** wpis, nie dubluje go.

## Dopisanie ukończonego ćwiczenia

```bash
P add-cwiczenie --lekcja "4.1" --poziom warmup
# --poziom: warmup | main | star | fix   (odpowiada 🔥 / ⭐ / ⚡ / 🔧)
```

## Mocne strony / do powtórki

```bash
P add-mocna-strona "samodzielne czytanie komunikatów kompilatora"
P add-do-powtorki --temat "różnica między polem a właściwością" --lekcja "8.3"
P remove-do-powtorki --temat "różnica między polem a właściwością"
```

`add-mocna-strona` trzyma max 7 najnowszych, duplikaty pomija.
`add-do-powtorki` nie dubluje tego samego tematu i ustawia pierwszą powtórkę na **jutro**.

## Powtórki z harmonogramem

```bash
P due                                                  # tematy, których termin minął albo jest dziś (tylko odczyt)
P review-do-powtorki --temat "konwersje" --wynik ok    # zaliczone → dłuższy odstęp (1 → 3 → 7 → 14 → 30 dni)
P review-do-powtorki --temat "konwersje" --wynik zle   # nie → poziom 0, powtórka jutro
```

`due` wypisuje tablicę JSON na standardowe wyjście i liczbę na standardowe wyjście błędów. Po piątym `ok` temat jest **usuwany** z `do_powtorki` z komunikatem „opanowane" — nie wołaj wtedy `remove-do-powtorki`. `remove-do-powtorki` zostaje do ręcznego sprzątania (temat wpisany przez pomyłkę).

Skill `quiz` woła `review-do-powtorki` po **każdym** pytaniu z trybu „powtórki na dziś"; agent na starcie sesji woła `due` i proponuje powtórkę, gdy lista nie jest pusta.

## Środowisko

```bash
P update-srodowisko \
  --system "Windows" \
  --dotnet-cmd "dotnet" \
  --dotnet-version "10.0.100" \
  --shell "PowerShell"
```

Można podać dowolny podzbiór pól — tylko one się zmienią. Brak flagi **nie** zeruje istniejącej wartości.

**Aktualizuj `dotnet_version` po każdej aktualizacji SDK u ucznia.** Nieaktualna wartość sprawi, że będziesz go ostrzegał przed nieistniejącym problemem albo przeoczysz prawdziwy.

## Notatki tutora (prywatne dla agenta)

```bash
P add-notatka "Anna chce kiedyś napisać narzędzie do porządkowania zdjęć"
```

Max 40 najnowszych (limit podniesiony z 20, żeby wpisy `parking:` przeżyły kilka modułów). **Nie pokazuj uczniowi**, jeśli sam nie zapyta.

**Dobre kandydatki na notatkę:** pomysł ucznia na własny program (wraca w lekcji 14.1), co go zniechęca, co go wciąga, jak reaguje na utknięcie.

**Parking — konwencja obowiązkowa.** Gdy uczeń zapyta o temat z przyszłego modułu (kontrolowane wyprzedzanie, patrz `csharp-tutor.md`), zapisz:
```bash
P add-notatka "parking: lista (pytał w 2.1 — czy na 100 imion trzeba 100 zmiennych)"
```
Prefiks `parking:` jest stały — skill `lekcja` szuka go w kroku 0 i otwiera lekcję od „pytałeś o to w 2.1". Podaj nazwę tematu **taką jak w tabeli `temat → moduł`** i lekcję, w której padło pytanie.

## Zakończenie sesji

```bash
P end-session
```

Ustawia `ostatnia_sesja=dziś`, `liczba_sesji+=1`. Wywołuj **raz** na koniec każdej sesji rozmowy.

## Recovery (gdy student.json uszkodzony)

```bash
P recovery
```

Narzędzie szuka najnowszego **działającego** backupu w `postep/backups/`, przenosi uszkodzony do `postep/student.broken.<TS>.json` (NIE kasuje), kopiuje backup na miejsce, wypisuje podsumowanie przywróconego stanu.

# Procedura sesji

## Start sesji

1. **Odczyt:** `Read` na `postep/student.json` (nie przez narzędzie — patrz wyżej)
2. Plik nie istnieje → uczeń nowy, uruchom onboarding
3. Plik jest, ale nie parsuje się jako JSON → `recovery` (zapytaj ucznia najpierw). Tu **musisz** użyć narzędzia; jeśli `dotnet` nie startuje, weź ścieżkę z najnowszego backupu w `postep/backups/`, który da się odczytać
4. W normalnym przypadku zwróć agentowi:
   - `imie`, `aktualna_lekcja`
   - 2-3 ostatnie wpisy z `ukonczone_lekcje`
   - `do_powtorki` (jeśli niepusta) — i wynik `P due`: tematy zaległe na dziś (niepusta lista → zaproponuj powtórkę, niezależnie od dni przerwy)
   - `sciezka` — `skrocona` zmienia sposób prowadzenia lekcji w modułach 2-7 (skill `lekcja`)
   - liczbę dni od `ostatnia_sesja` (>7 → quiz odświeżający)
   - **`srodowisko.dotnet_cmd`, `srodowisko.dotnet_version`, `srodowisko.system`** — potrzebne w każdej komendzie pokazywanej uczniowi
5. **Sprawdź `dotnet_version`.** Jeśli < 10.0 → zatrzymaj i wywołaj skill `setup-dotnet` przed lekcją.

## Onboarding (pierwsza sesja)

Po wywiadzie + skill `setup-dotnet` (który zna system, komendę i wersję):

```bash
P init --imie <imię_z_wywiadu> --cel <cel> --tempo <tempo> \
       --system <z_setup-dotnet> --dotnet-cmd <z_setup-dotnet> --dotnet-version <z_setup-dotnet>
```

## Po każdej ukończonej lekcji

1. Zapytaj: „Od 1 do 5, jak trudna była ta lekcja?"
2. `P add-lekcja --id <X.Y> --trudnosc <N>`
3. `P set --field aktualna_lekcja --value <następna_z_INDEX.md>`
4. Opcjonalnie `add-mocna-strona` / `add-do-powtorki`

Sekcja **Po lekcji** w pliku lekcji podaje dokładnie, jaka jest następna lekcja.

## Po każdym ukończonym ćwiczeniu

```bash
P add-cwiczenie --lekcja <X.Y> --poziom <warmup|main|star>
```

## Moduł 14 — projekt

Lekcja 14.3 rozciąga się na kilka sesji. Nie czekaj z zapisem do jej końca:
```bash
P add-notatka "projekt: dodany zapis do JSON, działa; następnie argumenty CLI"
P end-session
```
`add-lekcja --id 14.3` dopisz dopiero, gdy etap projektu jest skończony.

## Koniec sesji rozmowy

```bash
P end-session
```

# Backupy

Narzędzie tworzy backup do `postep/backups/student.{TS}.json` przed każdą modyfikacją.

**Nie kasuj backupów automatycznie.** Jeśli `postep/backups/` rośnie (>50 plików), powiadom ucznia:
> "Masz 53 backupy `student.json`, najstarszy z 2026-01-15. Chcesz przenieść starsze niż 30 dni do `postep/backups/_old/`?"

Po `tak`:
```bash
# macOS / Linux
mkdir -p postep/backups/_old
# wypisz listę, przenieś przez mv — NIGDY find -delete
```
```powershell
# Windows PowerShell
New-Item -ItemType Directory -Force -Path postep/backups/_old | Out-Null
# wypisz listę, przenieś przez Move-Item — NIGDY Remove-Item
```

# Przy >7 dniach przerwy

```bash
P read --field ostatnia_sesja
```

Powiedz uczniowi:
> "Cześć [imię]! Ostatnio rozmawialiśmy [N] dni temu. Chcesz najpierw szybką powtórkę, czy lecimy dalej z lekcją [aktualna_lekcja]?"

# Twarde zasady

- **NIGDY** bezpośredni `Write` / `Edit` na `student.json`. ZAWSZE przez narzędzie.
- **NIGDY** nie buduj nowego JSON-a „z pamięci" — narzędzie czyta, modyfikuje wskazane pola, zapisuje. To chroni przed utratą pól z przyszłych wersji schematu.
- **Nie zapominaj o `--`** w wywołaniu. To najczęstsza pomyłka przy `dotnet run`.
- **Nie wymyślaj danych.** Nie znasz wartości → pytaj ucznia.
- **`notatki_tutora` są prywatne** — nie pokazuj bez prośby.
- **Daty** zawsze ISO `YYYY-MM-DD` — narzędzie robi to samo.
- **`student.json` jest w `.gitignore`** — to stan konkretnego ucznia, nie część kursu.

# Test poprawności (dla autora)

Kompilacja bez uruchamiania:
```bash
dotnet build .claude/skills/postep/postep.cs
```

Test na żywym stanie — **tylko gdy `postep/student.json` nie istnieje**, inaczej `init` odmówi. Uruchom z katalogu głównego kursu:

```bash
P() { dotnet run .claude/skills/postep/postep.cs -- "$@"; }

P init --imie Test --cel hobby --tempo "2-5" --system macOS --dotnet-cmd dotnet --dotnet-version 10.0.100
P add-lekcja --id 1.1 --trudnosc 2
P add-cwiczenie --lekcja 1.1 --poziom warmup
P add-do-powtorki --temat "konwersje" --lekcja 2.3
P due
P review-do-powtorki --temat "konwersje" --wynik zle
P review-do-powtorki --temat "konwersje" --wynik ok
P remove-do-powtorki --temat "konwersje"
P set --field sciezka --value skrocona
P set --field aktualna_lekcja --value 1.2
P read --field srodowisko.dotnet_version
P end-session
P read
```

**Sprzątanie po teście — archiwizuj, nie kasuj.** Obowiązuje ta sama zasada co wszędzie: żadnego `rm -rf` na `postep/`.

```bash
# macOS / Linux
TS=$(date +%Y-%m-%d-%H-%M-%S)
mkdir -p postep/archiwum/test-$TS
mv postep/student.json postep/archiwum/test-$TS/ 2>/dev/null
[ -d postep/backups ] && mv postep/backups postep/archiwum/test-$TS/backups
```
```powershell
# Windows PowerShell
$TS = Get-Date -Format "yyyy-MM-dd-HH-mm-ss"
New-Item -ItemType Directory -Force -Path "postep/archiwum/test-$TS" | Out-Null
Move-Item postep/student.json "postep/archiwum/test-$TS/" -ErrorAction SilentlyContinue
if (Test-Path postep/backups) { Move-Item postep/backups "postep/archiwum/test-$TS/backups" }
```

Katalog `postep/archiwum/test-*` możesz potem usunąć ręcznie, świadomie i patrząc na zawartość — ale to decyzja człowieka, nie skrypt w dokumentacji.
