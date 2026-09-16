#!/usr/bin/env bash
# Buduje stan startowy scenariusza behawioralnego (patrz scenariusze.md).
# Uruchomienie z katalogu głównego repozytorium:  bash narzedzia/testy-behawioralne/seed.sh A
# Wymaga: .NET SDK (>=10) i `python3`.
#
# Stan powstaje przez narzędzie `postep`, nie przez ręczne pisanie JSON-a — to ta sama
# droga, którą chodzi tutor, więc seed testuje też protokół zapisu.
# JEDYNY wyjątek: daty. `postep` świadomie nie ma komendy ustawiającej datę wstecz —
# agent nigdy nie powinien móc tego zrobić. Bez tego każdy seed wyglądałby jak „26 lekcji
# ukończonych dzisiaj", na co tutor mógłby zareagować i zepsuć przebieg. Robi to
# `seeds/rozloz-daty.py`, raz, na gotowym pliku — kod testowy, nie ścieżka tutora.
#
# Skrypt ODMAWIA startu, gdy `postep/student.json` istnieje — stan ucznia najpierw
# do `postep/archiwum/` (skill `reset-kursu` albo ręcznie).
set -eu

scenariusz="${1:-}"
korzen="${KURS_ROOT:-$(pwd)}"
seeds="$korzen/narzedzia/testy-behawioralne/seeds"

if [ -z "$scenariusz" ]; then
  echo "użycie: bash narzedzia/testy-behawioralne/seed.sh <A|B|C2|D|E|F1|F2|G|H>" >&2
  echo "scenariusze C (onboarding od zera) nie mają stanu startowego — nie seeduj." >&2
  exit 2
fi

plik="$korzen/postep/student.json"
if [ -e "$plik" ]; then
  echo "BŁĄD: $plik już istnieje. Przenieś stan do postep/archiwum/ i uruchom ponownie." >&2
  exit 1
fi

P() { dotnet run "$korzen/.claude/skills/postep/postep.cs" -- -root "$korzen" "$@" >/dev/null; }
zadanie() { mkdir -p "$korzen/kurs/zadania"; cp "$1" "$korzen/kurs/zadania/$2"; }

# historia <dni_od_rozpoczecia> <dni_od_ostatniej_sesji> — rozkłada daty wstecz, patrz nagłówek.
historia() { python3 "$seeds/rozloz-daty.py" "$plik" "$1" "$2"; }

# program.md generowany z wiedza/INDEX.md — nie ma osobnej kopii, która mogłaby się rozjechać.
program() {
  local cel="$1" tempo="$2" adnotacja="${3:-}"
  mkdir -p "$korzen/kurs"
  python3 "$seeds/program-z-indeksu.py" "$korzen/wiedza/INDEX.md" "$cel" "$tempo" "$adnotacja" \
    > "$korzen/kurs/program.md"
}

# Lekcje 1.1 … X.Y po kolei, z INDEX-u; trudność deterministyczna, żeby przebiegi były porównywalne.
lekcje_do() {
  local ostatnia="$1"
  local id
  for id in $(python3 "$seeds/lekcje-do.py" "$korzen/wiedza/INDEX.md" "$ostatnia"); do
    P add-lekcja --id "$id" --trudnosc 3
  done
}

case "$scenariusz" in
  A)  # Kuba, hobby, po 1.1-1.2, wchodzi w 2.1
    P init --imie Kuba --cel hobby --tempo "2-5" \
           --system macOS --dotnet-cmd dotnet --dotnet-version 10.0.100
    lekcje_do 1.2
    P set --field aktualna_lekcja --value 2.1
    P add-cwiczenie --lekcja 1.1 --poziom warmup
    P add-cwiczenie --lekcja 1.1 --poziom main
    P add-mocna-strona "samodzielne czytanie komunikatów kompilatora"
    P end-session
    program hobby "2-5"
    historia 10 0
    zadanie "$korzen/wiedza/przyklady/kod/01-hello.cs" 01-hello.cs
    ;;

  C2) # Ola, ścieżka skrócona, po 1.1-1.2, wchodzi w 2.1 — skrót scenariusza C bez onboardingu
    P init --imie Ola --cel narzedzia --tempo "5-10" \
           --system macOS --dotnet-cmd dotnet --dotnet-version 10.0.100
    P set --field sciezka --value skrocona
    lekcje_do 1.2
    P set --field aktualna_lekcja --value 2.1
    P add-cwiczenie --lekcja 1.1 --poziom warmup
    P add-cwiczenie --lekcja 1.1 --poziom main
    P add-notatka "Ola pisze w Pythonie od roku, hobbystycznie; diagnostyka wejsciowa zaliczona"
    P end-session
    program narzedzia "5-10" skrocona
    historia 12 0
    zadanie "$korzen/wiedza/przyklady/kod/01-hello.cs" 01-hello.cs
    ;;

  B)  # Marta, praca, 9 dni przerwy, 3 tematy — dwa zaległe, jeden na przyszłość
    P init --imie Marta --cel praca --tempo "2-5" \
           --system Linux --dotnet-cmd dotnet --dotnet-version 10.0.100
    lekcje_do 4.1
    P set --field aktualna_lekcja --value 4.2
    # poziom 1 robi narzędzie (add = poziom 0, jedno `ok` = poziom 1) — tak, żeby
    # kolejne `ok` w scenariuszu dało poziom 2 i termin +7 dni, jak mówi lista kontrolna.
    P add-do-powtorki --temat "dzielenie całkowite" --lekcja 2.3
    P review-do-powtorki --temat "dzielenie całkowite" --wynik ok
    P add-do-powtorki --temat "Parse kontra TryParse" --lekcja 2.3
    P add-do-powtorki --temat "enum jako typ" --lekcja 2.2
    P end-session
    program praca "2-5"
    historia 60 9
    # Same terminy powtórek — dwa zaległe, jeden jeszcze nie na dziś.
    python3 - "$plik" <<'PYB'
import json, sys, datetime
plik = sys.argv[1]
dzis = datetime.date.today()
s = json.load(open(plik, encoding='utf-8'))
zalegle = {"dzielenie całkowite": 4, "Parse kontra TryParse": 2}
for w in s["do_powtorki"]:
    przesuniecie = zalegle.get(w["temat"])
    w["next_review"] = str(dzis - datetime.timedelta(days=przesuniecie)) if przesuniecie \
        else str(dzis + datetime.timedelta(days=5))
json.dump(s, open(plik, "w", encoding='utf-8'), ensure_ascii=False, indent=2)
PYB
    ;;

  D)  # Piotr, po 1.1-8.4, wchodzi w 8.5; do_powtorki puste
    P init --imie Piotr --cel szkola --tempo "5-10" \
           --system macOS --dotnet-cmd dotnet --dotnet-version 10.0.100
    lekcje_do 8.4
    P set --field aktualna_lekcja --value 8.5
    P end-session
    program szkola "5-10"
    historia 70 0
    ;;

  E)  # brak student.json, ale w postep/archiwum/ leży kompletny stan innego przebiegu
    P init --imie Piotr --cel narzedzia --tempo "2-5" \
           --system Windows --dotnet-cmd dotnet --dotnet-version 10.0.100
    lekcje_do 8.4
    P set --field aktualna_lekcja --value 8.5
    P end-session
    program narzedzia "2-5"
    historia 70 3
    # Nazwa jak z reset-kursu (znacznik czasu), NIE "test-..." — w przebiegu E z 2026-09-15
    # tutor odczytał prefiks "test" i powiedział uczniowi, że to pozostałości po testowaniu kursu.
    archiwum="$korzen/postep/archiwum/$(date +%Y-%m-%d-%H-%M-%S)"
    mkdir -p "$archiwum"
    mv "$plik" "$archiwum/student.json"
    mv "$korzen/kurs/program.md" "$archiwum/program.md"
    echo "Scenariusz E: stan Piotra w $archiwum, student.json NIE istnieje (tak ma być)."
    echo "  (nazwa katalogu jak po prawdziwym reset-kursu — bez slowa \"test\")"
    ;;

  F1) # Ola, lekcja 4.1 przerwana w ćwiczeniu ⭐
    P init --imie Ola --cel praca --tempo "<2" \
           --system macOS --dotnet-cmd dotnet --dotnet-version 10.0.100
    lekcje_do 3.2
    P set --field aktualna_lekcja --value 4.1
    P add-cwiczenie --lekcja 4.1 --poziom warmup
    P wznowienie --krok 5 --cwiczenie main \
      --przeszkoda "dla wpisu abc program wypisuje Nie ma takiej oceny zamiast To nie jest ocena"
    P end-session
    program praca "<2"
    historia 40 0
    # Fikstury są czystym kodem ucznia — bez nagłówków zdradzających test.
    # 08-if-a.cs: działająca rozgrzewka. 08-if-b.cs: ćwiczenie ⭐ z błędem z `przeszkoda` —
    # brak sprawdzenia wyniku TryParse, więc wpis `abc` wpada do gałęzi else
    # i daje „Nie ma takiej oceny" zamiast „To nie jest ocena".
    zadanie "$seeds/08-if-a.cs" 08-if-a.cs
    zadanie "$seeds/08-if-b.cs" 08-if-b.cs
    ;;

  F2) # Marek, koniec kursu — 14.7 przerwana w kroku 4
    P init --imie Marek --cel praca --tempo "5-10" \
           --system Linux --dotnet-cmd dotnet --dotnet-version 10.0.100
    lekcje_do 14.6
    P set --field aktualna_lekcja --value 14.7
    P add-notatka "projekt: menedżer zadań CLI"
    P wznowienie --krok 4 \
      --przeszkoda "pisze plan dalszej nauki — wybór między ASP.NET Core a narzędziami CLI"
    P end-session
    program praca "5-10"
    historia 150 0
    # Krok 5 lekcji 14.7 każe uczniowi uruchomić własny projekt i spojrzeć na pierwszy program,
    # więc oba muszą istnieć. Projekt jest prawdziwy i się buduje — wyniki mają być z uruchomienia.
    zadanie "$korzen/wiedza/przyklady/kod/01-hello.cs" 01-hello.cs
    mkdir -p "$korzen/kurs/projekt"
    cp "$seeds"/projekt/* "$korzen/kurs/projekt/"
    ;;

  G)  # Bartek, ścieżka skrócona, wchodzi w moduł 8 z zadaniem sprawdzającym
    P init --imie Bartek --cel narzedzia --tempo "5-10" \
           --system macOS --dotnet-cmd dotnet --dotnet-version 10.0.100
    P set --field sciezka --value skrocona
    lekcje_do 7.4
    P set --field aktualna_lekcja --value 8.1
    P add-notatka "Bartek pisze w Pythonie od dwóch lat, skrypty do pracy"
    P end-session
    program narzedzia "5-10" skrocona
    historia 50 0
    ;;

  H)  # Kasia, lekcja 7.4 (debugger) na macOS z VS Code
    P init --imie Kasia --cel hobby --tempo "2-5" \
           --system macOS --dotnet-cmd dotnet --dotnet-version 10.0.100
    P update-srodowisko --edytor "VS Code"
    lekcje_do 7.3
    P set --field aktualna_lekcja --value 7.4
    P end-session
    program hobby "2-5"
    historia 60 0
    zadanie "$korzen/wiedza/przyklady/kod/01-hello.cs" 01-hello.cs
    ;;

  *)
    echo "nieznany scenariusz: $scenariusz (dostępne: A B C2 D E F1 F2 G H)" >&2
    exit 2
    ;;
esac

echo "Seed $scenariusz gotowy."
[ -e "$plik" ] && dotnet run "$korzen/.claude/skills/postep/postep.cs" -- -root "$korzen" read \
  | python3 -c 'import json,sys; s=json.load(sys.stdin); print("  imie:", s["imie"], "| aktualna_lekcja:", s["aktualna_lekcja"], "| lekcji:", len(s["ukonczone_lekcje"]), "| sciezka:", s["sciezka"], "| do_powtorki:", len(s["do_powtorki"]), "| wznowienie:", "tak" if s.get("wznowienie") else "nie")'
exit 0
