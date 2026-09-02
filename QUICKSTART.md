# QUICKSTART — jak korzystać z agenta `csharp-tutor`

Krótki przewodnik dla osoby zaczynającej naukę C# z tym kursem.

> **⚠️ Użytkownicy Windows:** komendy `dotnet ...` są takie same na każdym systemie — to główna zaleta tego kursu i nie ma tu żadnego haczyka. Różnią się tylko komendy powłoki. W dokumentach są w wersji **macOS/Linux**; na Windows zamień:
> - `cat plik.cs` → `type plik.cs`
> - `ls -l` → `dir`
> - `export X=y` → `$env:X="y"`
>
> Agent tłumaczy je automatycznie w trakcie sesji. Ta uwaga dotyczy **ręcznego** czytania dokumentów.

---

## 🚀 Pierwsze uruchomienie

### 1. Sprawdź, gdzie jesteś
```sh
cd ~/Projects/claude-agent-csharp-course
pwd
```
Musisz być **w tym katalogu** — agent i skille są lokalne (`.claude/`).

### 2. Uruchom Claude Code
```sh
claude
```

### 3. Napisz w czacie
```
ucz mnie C#
```

Agent automatycznie:
1. Wykryje brak `postep/student.json` → uruchomi **onboarding**
2. Sprawdzi, czy masz .NET SDK (`dotnet --version`) — wymagane **10.0 lub nowsze**
3. Sprawdzi, czy działają aplikacje jednoplikowe (`dotnet run plik.cs`)
4. Każe Ci przeczytać `kurs/JAK-PISAC-KOD.md` (5 min)
5. Zapyta o imię, cel (praca / narzędzia / hobby / szkoła), tempo (h/tydzień)
6. Wygeneruje **Twój** `kurs/program.md` (48 lekcji dopasowanych do celu)
7. Utworzy `postep/student.json` ze stanem początkowym
8. Zaproponuje rozpoczęcie lekcji 1.1

---

## 📚 Typowa sesja nauki

### Układ dwóch okien obok siebie

```
┌──────────────────────────┬──────────────────────────┐
│                          │                          │
│      Claude Code         │     VS Code + terminal   │
│      (lewa połowa)       │     (prawa połowa)       │
│                          │                          │
│  Tutaj rozmawiasz        │  Tutaj piszesz kod       │
│  z agentem               │  i go uruchamiasz        │
│                          │                          │
└──────────────────────────┴──────────────────────────┘
```

VS Code ma wbudowany terminal (`Terminal → New Terminal`, Ctrl+`) — nie potrzebujesz trzeciego okna.

### Przebieg lekcji

1. **W Claude Code:** napisz `kontynuujemy`
2. Agent wczytuje gotową lekcję z `wiedza/lekcje/02.01-zmienne-i-typy.md` i prowadzi Cię **sokratejsko** (zadaje pytania, Ty odpowiadasz)
3. **Eksperyment** — agent każe Ci coś napisać; otwierasz VS Code, piszesz w `kurs/zadania/02-zmienne.cs`
4. **Zapisujesz plik** (Cmd+S / Ctrl+S) — najczęstszy pominięty krok na świecie
5. **W terminalu**, z katalogu `kurs/zadania`, uruchamiasz: `dotnet run 02-zmienne.cs`
6. Wracasz do Claude Code, wklejasz wynik — agent dopytuje
7. **Ćwiczenie** — agent generuje 3 zadania (🔥/⭐/⚡)
8. Piszesz rozwiązania w osobnych plikach: `02-zmienne-a.cs`, `02-zmienne-b.cs`
9. Mówisz `sprawdź moje zadanie` → agent robi **sokratejski review** (pyta, nie ocenia z góry)
10. Agent aktualizuje `postep/student.json`, ustawia następną lekcję

---

## 🎯 Najczęstsze komendy

| Co chcę zrobić | Komenda |
| --- | --- |
| Wrócić do nauki po przerwie | `kontynuujemy` |
| Coś przetestować | `daj mi zadanie` |
| Sprawdzić mój kod | `sprawdź moje zadanie` |
| Kod się nie kompiluje | `nie chce się skompilować` + wklej komunikat |
| Program działa źle | `nie działa mi` + wklej kod i wynik |
| Powtórka | `quiz` (3 pyt) / `quiz słabe` |
| Stan postępów | `pokaż postępy` |
| Zapomniałem, co mogę robić | `lista komend` |
| Zacząć od nowa | `zresetuj kurs` |

Pełna lista — napisz `lista komend` w czacie lub zobacz [README.md](README.md).

---

## ⏸ Co zrobić, gdy wracasz po tygodniach

Po prostu napisz `ucz mnie C#` albo `kontynuujemy`:
- Agent czyta `postep/student.json`
- Wita Cię po imieniu, pokazuje ostatnią lekcję
- Jeśli przerwa > 7 dni → **automatycznie** zaproponuje krótki quiz powtórkowy
- Potem ruszacie z bieżącą lekcją

---

## 📂 Co jest gdzie

```
claude-agent-csharp-course/
│
├── .claude/                      ← agent + 9 skilli (NIE dotykaj)
├── wiedza/                       ← baza wiedzy (lekcje sokratejskie + przykłady)
├── .editorconfig                 ← konwencje formatowania, czyta je edytor
│
├── kurs/
│   ├── JAK-PISAC-KOD.md          ← PRZECZYTAJ NA POCZĄTKU (5 min)
│   ├── program.md                ← Twój plan kursu (powstanie po onboardingu)
│   ├── lekcje/                   ← Twoje notatki z lekcji (agent zapisuje)
│   ├── zadania/                  ← TU PISZESZ SWÓJ KOD
│   │   ├── 01-hello.cs           ← tworzysz Ty
│   │   ├── 02-zmienne-a.cs
│   │   └── 02-zmienne-ZADANIA.md ← zapisuje agent
│   └── projekt/                  ← Twój program z modułu 14
│
└── postep/
    ├── student.json              ← Twój stan (możesz podglądać)
    ├── backups/                  ← automatyczne kopie stanu
    └── archiwum/                 ← backupy po resetach
```

Zwróć uwagę na `kurs/zadania/` — **płaska lista plików**, żadnych podkatalogów, żadnych projektów. Jedno ćwiczenie to jeden plik `.cs`.

---

## 💡 Mini-przykład pełnej sesji

```
Ty:    ucz mnie C#
Agent: Cześć! Widzę, że jesteś tu pierwszy raz. Uczę przez pytania — i od razu
       uprzedzam: kod uruchamiasz Ty, nie ja. Zacznijmy od sprawdzenia
       środowiska. Otwórz terminal i wpisz: dotnet --version

Ty:    10.0.400
Agent: Świetnie. Teraz sprawdźmy, czy działają programy jednoplikowe — to na
       nich stoi cały kurs. Wpisz:
       dotnet run wiedza/przyklady/kod/00-sprawdzenie.cs
       Pierwsze uruchomienie potrwa kilka sekund, to normalne.

Ty:    Środowisko gotowe. Czas zacząć.
Agent: Dokładnie tak miało być. Teraz przeczytaj kurs/JAK-PISAC-KOD.md —
       5 minut, wyjaśnia gdzie piszesz kod i jak go uruchamiasz.
       Daj znać, gdy skończysz.

Ty:    przeczytałam
Agent: Super. Teraz 4 pytania, żeby dopasować kurs:
       1. Jak masz na imię?
       2. Czemu chcesz się uczyć C#? (praca / narzędzia / hobby / szkoła)
       3. Ile godzin tygodniowo możesz poświęcić?
       4. Programowałaś już kiedyś w innym języku?

Ty:    Anna, praca, 3-5h tygodniowo, nigdy nie programowałam
Agent: [generuje program.md, student.json] Mam dla Ciebie 48 lekcji w 14 modułach,
       tempo ~3 lekcje/tydzień. Na końcu napiszesz własne narzędzie
       wiersza poleceń z testami.
       Zaczynamy od lekcji 1.1 — "Czym jest C# i pierwszy program"?

Ty:    tak, zaczynamy
Agent: Zanim powiem cokolwiek o C# — wyobraź sobie, że tłumaczysz komuś
       przez telefon, jak zrobić jajecznicę...
```

---

## ⚠️ Kilka pułapek na start

- **Plik niezapisany przed `dotnet run`** — najczęstszy błąd. Zawsze Cmd+S / Ctrl+S.
- **Uruchamianie z niewłaściwego katalogu** — zadania odpalasz **z `kurs/zadania`**. Sprawdź `pwd`. Komunikat, który wtedy dostaniesz, mówi coś o „projekcie" i jest mylący — nie potrzebujesz żadnego projektu, po prostu jesteś nie tam.
- **Pierwsze uruchomienie trwa kilka sekund** — .NET buduje program. To nie zawieszenie.
- **Otwarcie pojedynczego pliku zamiast całego folderu w VS Code** — wtedy formatowanie i podpowiedzi nie działają. `File → Open Folder`.
- **.NET starszy niż 10.0** — nie ruszy ani jedno ćwiczenie. Zaktualizuj przed startem.
- **Nie kopiuj rozwiązań z internetu** — uczysz się przez próbowanie, nie przez wklejanie. Poza tym większość przykładów w sieci jest w starszym stylu C# i będziesz się dziwić, czemu wygląda inaczej niż Twój kod.

---

## 🆘 Co zrobić, gdy coś nie działa

| Problem | Co zrobić |
| --- | --- |
| Agent się nie aktywuje | Sprawdź `pwd` — musisz być w katalogu kursu |
| Nie pamiętam komend | `lista komend` |
| Utknąłem na zadaniu | `nie działa mi` + wklej kod + wklej komunikat |
| Kod się nie kompiluje | `nie chce się skompilować` + cały komunikat, razem z `CSxxxx` |
| `dotnet: command not found` | `sprawdź .NET` — agent poprowadzi przez instalację |
| „Couldn't find a project to run" | Jesteś w złym katalogu albo pomyliłeś nazwę pliku. `pwd`, potem `ls` |
| Chcę zacząć od nowa | `zresetuj kurs` (z backupem) |
| Zgubiłem postęp | `cofnij reset` (z `postep/archiwum/`) |
| Pytanie o C#, nie o kurs | Po prostu zapytaj agenta normalnie |

---

## 📤 Dla autora (publikacja na GitHubie)

Jeśli chcesz udostępnić kurs:

```sh
git add .
git status                # sprawdź, czy nic prywatnego (student.json, .kb/ są ignorowane)
git commit -m "Agent kursu C#: szkielet, skille, moduły 1-2"
gh repo create claude-agent-csharp-course --public --source=. --push
```

`.gitignore` zadba, żeby Twój postęp, Twój kod i katalog `.kb/` z materiałami źródłowymi nie trafiły do publicznego repozytorium — można bezpiecznie udostępnić **strukturę kursu**, a każdy uczeń sklonuje i ma własny `student.json`.

Repozytorium ma plik [`LICENSE`](LICENSE) (MIT) i [`NOTICE.md`](NOTICE.md) z wyjaśnieniem po polsku. Jeśli publikujesz własną, zmienioną wersję — zostaw notę o autorstwie oryginału.

---

## 🎓 Filozofia kursu — w 3 zdaniach

1. **Sokratejsko, nie wykładowo** — agent zadaje pytania, Ty dochodzisz do odpowiedzi sam(a). Wolniej, ale głębiej.
2. **Nie uruchamiamy kodu za Ciebie** — sam piszesz, sam uruchamiasz, sam czytasz wynik. To część nauki, nie ograniczenie narzędzia.
3. **Twoje tempo** — czas trwania lekcji to wskazówka, nie termin. Możesz wrócić za tydzień, agent wie, gdzie skończyliście.

---

## 🔗 Powiązane dokumenty

- **[README.md](README.md)** — pełna dokumentacja, struktura, lista komend
- **[kurs/JAK-PISAC-KOD.md](kurs/JAK-PISAC-KOD.md)** — workflow pisania i uruchamiania kodu (przeczytaj raz na początku)
- **[wiedza/INDEX.md](wiedza/INDEX.md)** — mapa 48 lekcji i lista tego, czego w kursie nie ma
- **[wiedza/AKTUALIZACJE.md](wiedza/AKTUALIZACJE.md)** — co się zmieniło między .NET Framework a .NET 10

---

**Powodzenia!** 🟣
