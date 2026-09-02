---
name: reset-kursu
description: Resetuje stan kursu C# (miękko lub w pełni), zawsze robi backup do postep/archiwum/. Miękki reset czyści tylko postęp i program — kod ucznia zostaje. Pełny reset usuwa wszystko poza skillami i instrukcją. Wymaga jawnego potwierdzenia od ucznia. Użyj gdy uczeń mówi "zresetuj kurs", "zacznij od nowa", "wyczyść postęp", "chcę nowy plan".
---

# Cel

Pozwolić uczniowi zacząć od nowa **bez utraty pracy** — backup robisz **zawsze**, nawet przy „pełnym" resecie. Dane nigdy nie giną, tylko schodzą z głównej ścieżki.

# Dwa tryby

| Tryb      | Co czyści                                        | Co zostawia                                                | Kiedy |
| --------- | ------------------------------------------------ | ---------------------------------------------------------- | ----- |
| Miękki 🧽 | `postep/student.json`, `kurs/program.md`         | Cały kod w `kurs/zadania/`, notatki w `kurs/lekcje/`, projekt w `kurs/projekt/` | Chcesz nowy program kursu, ale zachować dotychczasowe ćwiczenia |
| Pełny 🔥  | Wszystko: postęp, program, notatki, kod, projekt | Tylko `.claude/`, `wiedza/`, `README.md`, `QUICKSTART.md`, `kurs/JAK-PISAC-KOD.md`, `.editorconfig` | Chcesz absolutnie świeży start |

**Domyślny tryb przy „zresetuj kurs" bez doprecyzowania: miękki.**

# Procedura — twardy protokół potwierdzenia

## Krok 1: rozpoznaj intencję

Gdy uczeń mówi „zresetuj kurs", **NIE rób nic od razu**. Najpierw dwa pytania:

1. **„Czemu chcesz zresetować?"** — czasem rozwiązanie jest inne
2. **„Miękki czy pełny?"** — pokaż tabelę powyżej

Alternatywy warte zaproponowania zamiast resetu:
- Zmiana celu/tempa → edytujemy `program.md`, bez resetu
- Powtórka tematu → quiz „słabe punkty", bez resetu
- Wyczyszczenie `do_powtorki` → operacja na `student.json`, bez resetu
- „Chcę zacząć zadania od nowa" → nowy plik `NN-temat-v2.cs`, stary zostaje

## Krok 2: pokaż, co konkretnie zniknie

Przed wykonaniem **wypisz dokładnie** ścieżki, które trafią do backupu:

```
Zostaną przeniesione do postep/archiwum/2026-08-30-14-30/:
  - postep/student.json (12 ukończonych lekcji, 8 sesji)
  - kurs/program.md
  [pełny reset dodatkowo]:
  - kurs/lekcje/ (8 plików)
  - kurs/zadania/ (14 plików .cs)
  - kurs/projekt/ (jeśli istnieje)
```

**Ostrzeż wprost, jeśli reset dotyka `kurs/projekt/`** — to zwykle najwięcej pracy w całym kursie:
> „W `kurs/projekt/` masz swój program z modułu 14. Trafi do archiwum, ale i tak upewnijmy się: na pewno pełny reset?"

## Krok 3: poproś o jawne potwierdzenie

Uczeń musi napisać **literalnie**:
- miękki: `tak, miękki reset`
- pełny: `tak, pełny reset`

Inne potwierdzenia („ok", „tak", „rób") — **odmawiasz** i prosisz o pełną frazę.

## Krok 4: wykonaj reset

**Sprawdź `srodowisko.system` w `student.json`.** Poniżej dwie wersje tych samych operacji — POSIX dla macOS/Linuksa i PowerShell dla Windows. Wykonują dokładnie to samo: przenoszą pliki do `postep/archiwum/<znacznik czasu>/`. Na Windows z Git Bash działa też wersja POSIX; przy wątpliwości użyj PowerShella.

### Katalog backupu

```bash
# macOS / Linux
TIMESTAMP=$(date +%Y-%m-%d-%H-%M-%S)
mkdir -p postep/archiwum/$TIMESTAMP
```
```powershell
# Windows PowerShell
$TIMESTAMP = Get-Date -Format "yyyy-MM-dd-HH-mm-ss"
New-Item -ItemType Directory -Force -Path "postep/archiwum/$TIMESTAMP" | Out-Null
```

### Miękki reset

```bash
# macOS / Linux
mv postep/student.json postep/archiwum/$TIMESTAMP/ 2>/dev/null
mv kurs/program.md postep/archiwum/$TIMESTAMP/ 2>/dev/null
```
```powershell
# Windows PowerShell
Move-Item postep/student.json "postep/archiwum/$TIMESTAMP/" -ErrorAction SilentlyContinue
Move-Item kurs/program.md     "postep/archiwum/$TIMESTAMP/" -ErrorAction SilentlyContinue
```

### Pełny reset

```bash
# macOS / Linux
mv postep/student.json postep/archiwum/$TIMESTAMP/ 2>/dev/null
mv kurs/program.md postep/archiwum/$TIMESTAMP/ 2>/dev/null
[ -d kurs/lekcje ]  && mv kurs/lekcje  postep/archiwum/$TIMESTAMP/lekcje
[ -d kurs/zadania ] && mv kurs/zadania postep/archiwum/$TIMESTAMP/zadania
[ -d kurs/projekt ] && mv kurs/projekt postep/archiwum/$TIMESTAMP/projekt

# Odtwórz strukturę
mkdir -p kurs/lekcje kurs/zadania
touch kurs/lekcje/.gitkeep kurs/zadania/.gitkeep
```
```powershell
# Windows PowerShell
Move-Item postep/student.json "postep/archiwum/$TIMESTAMP/" -ErrorAction SilentlyContinue
Move-Item kurs/program.md     "postep/archiwum/$TIMESTAMP/" -ErrorAction SilentlyContinue
foreach ($k in "lekcje", "zadania", "projekt") {
    if (Test-Path "kurs/$k") { Move-Item "kurs/$k" "postep/archiwum/$TIMESTAMP/$k" }
}

# Odtwórz strukturę
New-Item -ItemType Directory -Force -Path kurs/lekcje, kurs/zadania | Out-Null
New-Item -ItemType File -Force -Path kurs/lekcje/.gitkeep, kurs/zadania/.gitkeep | Out-Null
```

**Po pełnym resecie nie trzeba odtwarzać żadnego pliku metryczki.** Ćwiczenia w tym kursie to samodzielne pliki `.cs` — pusty katalog `kurs/zadania/` wystarczy, żeby pierwsze zadanie ruszyło.

**Nigdy nie używaj `rm -rf`.** Wyłącznie `mv` do `postep/archiwum/`. To gwarancja, że nawet po „pełnym" resecie dane fizycznie są na dysku.

### Manifest backupu

W `postep/archiwum/$TIMESTAMP/MANIFEST.md`:

```markdown
# Backup z dnia 2026-08-30 14:30

**Tryb resetu:** miękki | pełny
**Powód podany przez ucznia:** [krótki cytat]
**Stan przed resetem:**
- Ukończonych lekcji: 12 z 49
- Aktualna lekcja: 5.2
- Liczba sesji: 8
- Wersja .NET: 10.0.400

**Co jest w tym katalogu:**
- student.json
- program.md
- (przy pełnym: lekcje/, zadania/, projekt/)

Aby przywrócić: skopiuj pliki z tego katalogu z powrotem do kurs/ i postep/.
```

## Krok 5: potwierdź i zaproś do nowego startu

> „Gotowe. Stary stan jest w `postep/archiwum/2026-08-30-14-30/` — jakbyś chciał wrócić, daj znać. Zaczynamy od nowa? Napisz **'ucz mnie C#'**, przejdziemy przez onboarding."

# Przywracanie z backupu

Uczeń mówi „cofnij" / „wróć do poprzedniego stanu":
1. Pokaż listę katalogów w `postep/archiwum/` (najnowszy na górze)
2. Zapytaj, który przywrócić
3. Skopiuj pliki **z powrotem** do `kurs/` i `postep/` — **nie usuwaj** katalogu archiwum
4. Potwierdź odczytem `student.json` (skill: `postep`)

# Sprzątanie starych backupów

**NIE rób tego automatycznie.** Jeśli uczeń zauważy, że ma 30 katalogów w `archiwum/`, zaproponuj: „Chcesz, żebym usunął backupy starsze niż 30 dni?" — pokaż listę, poproś o literalne potwierdzenie z liczbą, dopiero wtedy usuń.

# Twarde zasady

- **Backup zawsze**, nawet przy pełnym resecie.
- **Tylko `mv`, nigdy `rm -rf`.**
- **Dwa pytania + jawne potwierdzenie** przed wykonaniem. Nie ma szybkiej ścieżki.
- **Nie resetuj `.claude/`** — to konfiguracja agenta.
- **Nie resetuj `wiedza/`** — to baza wiedzy i lekcje sokratejskie, nie stan ucznia.
- **Nie resetuj `kurs/JAK-PISAC-KOD.md`, `README.md`, `QUICKSTART.md`, `.editorconfig`** — to instrukcje i konfiguracja, nie generowana treść.
- **Po resecie zawsze pokaż ścieżkę backupu.**
- **Katalogi `bin/` i `obj/`** (jeśli powstały przy projekcie z modułu 14) możesz pominąć w backupie — odbudują się z kodu. Ale nie kasuj ich sam; zostaw i powiedz uczniowi, że może usunąć.
