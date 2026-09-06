#!/usr/bin/env bash
# Testy narzędzia postep: błędna operacja NIE zmienia student.json.
# Działa na tymczasowym katalogu z pustymi plikami lekcji — nie dotyka postep/ w repozytorium.
# Uruchomienie z katalogu głównego:  bash narzedzia/testy-postep.sh
set -u
export DOTNET_NOLOGO=1 DOTNET_CLI_TELEMETRY_OPTOUT=1 DOTNET_CLI_UI_LANGUAGE=en

root=$(mktemp -d)
trap 'rm -rf "$root"' EXIT
mkdir -p "$root/wiedza/lekcje" "$root/.claude" "$root/postep"
for f in wiedza/lekcje/[0-9]*.md; do : > "$root/wiedza/lekcje/$(basename "$f")"; done

P() { dotnet run .claude/skills/postep/postep.cs -- -root "$root" "$@"; }
plik="$root/postep/student.json"
bledy=0

# Oczekuje kodu wyjścia != 0 i niezmienionego pliku.
odrzuca() {
  local opis=$1; shift
  local przed; przed=$(cat "$plik")
  if P "$@" >/dev/null 2>&1; then
    echo "FAIL: $opis — komenda przeszła"; bledy=$((bledy + 1)); return
  fi
  if [ "$przed" != "$(cat "$plik")" ]; then
    echo "FAIL: $opis — plik zmieniony mimo błędu"; bledy=$((bledy + 1)); return
  fi
  echo "ok: odrzuca — $opis"
}

# Oczekuje sukcesu i wzorca w pliku.
przyjmuje() {
  local opis=$1 wzorzec=$2; shift 2
  if ! P "$@" >/dev/null 2>&1; then
    echo "FAIL: $opis — komenda odrzucona"; bledy=$((bledy + 1)); return
  fi
  if ! grep -q -- "$wzorzec" "$plik"; then
    echo "FAIL: $opis — brak \"$wzorzec\" w pliku"; bledy=$((bledy + 1)); return
  fi
  echo "ok: przyjmuje — $opis"
}

P init --imie Test --cel hobby --tempo 2 >/dev/null || { echo "FAIL: init"; exit 1; }

odrzuca  "set na liście"                 set --field ukonczone_lekcje --value oops
odrzuca  "set na obiekcie"               set --field srodowisko --value oops
odrzuca  "set na liczbie"                set --field liczba_sesji --value 7
odrzuca  "set na nieznanym polu"         set --field nie_ma --value x
odrzuca  "set sciezka spoza słownika"    set --field sciezka --value szybka
odrzuca  "set aktualna_lekcja 99.99"     set --field aktualna_lekcja --value 99.99
odrzuca  "add-lekcja 99.99"              add-lekcja --id 99.99 --trudnosc 2
odrzuca  "add-lekcja bez kropki"         add-lekcja --id 41 --trudnosc 2
odrzuca  "add-lekcja trudność 9"         add-lekcja --id 4.1 --trudnosc 9
odrzuca  "add-cwiczenie 99.99"           add-cwiczenie --lekcja 99.99 --poziom main
odrzuca  "review nieznanego tematu"      review-do-powtorki --temat nic --wynik ok

przyjmuje "set sciezka skrocona"    '"sciezka": "skrocona"'   set --field sciezka --value skrocona
przyjmuje "set pola w obiekcie"     '"edytor": "vim"'         set --field srodowisko.edytor --value vim
przyjmuje "set aktualna_lekcja 4.1" '"aktualna_lekcja": "4.1"' set --field aktualna_lekcja --value 4.1
przyjmuje "update-srodowisko edytor" '"edytor": "code"'  update-srodowisko --edytor code
przyjmuje "add-lekcja 4.1"          '"id": "4.1"'             add-lekcja --id 4.1 --trudnosc 2
przyjmuje "add-cwiczenie 4.1/main"  '"poziom": "main"'        add-cwiczenie --lekcja 4.1 --poziom main
przyjmuje "add-do-powtorki"         '"poziom": 0'             add-do-powtorki --temat konwersje --lekcja 2.3
przyjmuje "review pomoc: poziom stoi" '"poziom": 0'           review-do-powtorki --temat konwersje --wynik pomoc
przyjmuje "review ok: poziom rośnie"  '"poziom": 1'           review-do-powtorki --temat konwersje --wynik ok
odrzuca   "review wynik spoza słownika"                        review-do-powtorki --temat konwersje --wynik moze

odrzuca   "set na wznowienie (null)"                        set --field wznowienie --value x
odrzuca   "wznowienie krok 9"                                wznowienie --krok 9
odrzuca   "wznowienie cwiczenie spoza słownika"              wznowienie --krok 3 --cwiczenie latwe
przyjmuje "wznowienie krok 3 + main" '"krok": 3'             wznowienie --krok 3 --cwiczenie main --przeszkoda "CS0165"
przyjmuje "wznowienie zapisuje lekcję" '"lekcja": "4.1"'     read
przyjmuje "add-cwiczenie projekt"    '"poziom": "projekt"'   add-cwiczenie --lekcja 5.3 --poziom projekt
przyjmuje "add-lekcja czyści wznowienie" '"wznowienie": null' add-lekcja --id 4.1 --trudnosc 3
przyjmuje "wznowienie --wyczysc"     '"wznowienie": null'    wznowienie --wyczysc

# Normalizacja id: 7.04 i 7.4 to ta sama lekcja, zapisana jako 7.4.
przyjmuje "add-lekcja 7.04"         '"id": "7.4"'             add-lekcja --id 7.04 --trudnosc 2
P add-lekcja --id 7.4 --trudnosc 3 >/dev/null 2>&1
if [ "$(grep -c '"id": "7.4"' "$plik")" != "1" ] || grep -q '"id": "7.04"' "$plik"; then
  echo "FAIL: 7.04 i 7.4 dały dwa wpisy albo zapis bez normalizacji"; bledy=$((bledy + 1))
else
  echo "ok: normalizacja — 7.04 + 7.4 = jeden wpis 7.4"
fi
przyjmuje "add-cwiczenie 07.4 → 7.4" '"lekcja": "7.4"'        add-cwiczenie --lekcja 07.4 --poziom star
grep -q '"lekcja": "07.4"' "$plik" && { echo "FAIL: add-cwiczenie zapisał 07.4 bez normalizacji"; bledy=$((bledy + 1)); }
odrzuca   "add-do-powtorki 99.99"                              add-do-powtorki --temat nic --lekcja 99.99
przyjmuje "add-do-powtorki 02.03 → 2.3" '"lekcja": "2.3"'     add-do-powtorki --temat norm --lekcja 02.03

# Koniec kursu (14.7 / 15.2): jedyna wartość spoza M.L, tylko w set aktualna_lekcja.
przyjmuje "set aktualna_lekcja 07.4 → 7.4" '"aktualna_lekcja": "7.4"' set --field aktualna_lekcja --value 07.4
odrzuca   "set aktualna_lekcja ukonczony (bez ogonków)"       set --field aktualna_lekcja --value ukonczony
przyjmuje "set aktualna_lekcja ukończony" '"aktualna_lekcja": "ukończony"' set --field aktualna_lekcja --value ukończony
odrzuca   "wznowienie po ukończeniu kursu"                     wznowienie --krok 2
odrzuca   "add-lekcja ukończony"                               add-lekcja --id ukończony --trudnosc 1
przyjmuje "set aktualna_lekcja 15.1 po ukończeniu" '"aktualna_lekcja": "15.1"' set --field aktualna_lekcja --value 15.1

# Struktura: poprawny JSON ze złym typem pola albo bez imienia = uszkodzony stan, nie inny stan.
kopia=$(cat "$plik")
python3 - "$plik" <<'PY'
import json,sys
p=sys.argv[1]; d=json.load(open(p)); d["ukonczone_lekcje"]="oops"; json.dump(d,open(p,"w"),ensure_ascii=False)
PY
odrzuca   "lista zamieniona na tekst"                          add-notatka "x"
printf '%s\n' "$kopia" > "$plik"
python3 - "$plik" <<'PY'
import json,sys
p=sys.argv[1]; d=json.load(open(p)); del d["imie"]; json.dump(d,open(p,"w"),ensure_ascii=False)
PY
odrzuca   "brak pola imie"                                     add-notatka "x"
printf '%s\n' "$kopia" > "$plik"
przyjmuje "po przywróceniu kopii zapis działa" '"x"'           add-notatka "x"

# Plik w schemacie 2 (bez klucza wznowienie): migracja przy pierwszym zapisie.
python3 - "$plik" <<'PY'
import json,sys
p=sys.argv[1]; d=json.load(open(p)); d["schema_version"]=2; del d["wznowienie"]; json.dump(d,open(p,"w"))
PY
przyjmuje "migracja 2 → 3"          '"schema_version": 3'    add-notatka "test"
przyjmuje "migracja dodaje wznowienie" '"wznowienie": null'  read

# Uszkodzony JSON: narzędzie ma odmówić, nie nadpisać.
echo '{"schema_version": 2, "imie": ' > "$plik"
odrzuca "uszkodzony JSON" add-lekcja --id 4.1 --trudnosc 2

if [ "$bledy" -ne 0 ]; then
  echo "BŁĘDY (postep): $bledy"; exit 1
fi
echo "OK: postep odrzuca błędne operacje bez zmiany stanu"
