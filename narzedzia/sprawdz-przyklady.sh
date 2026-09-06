#!/usr/bin/env bash
# Buduje każdy przykład z wiedza/przyklady/kod/ i narzędzie postep; zepsute programy uruchamia
# i porównuje z nagłówkami CI- (sekcja niżej).
# Błąd kompilacji = kod wyjścia 1. Ostrzeżenia są dozwolone — dwa przykłady
# (07-wejscie.cs: CS8600, 21-klasy.cs: CS8618) pokazują je celowo.
# Uruchomienie z katalogu głównego repozytorium:  bash narzedzia/sprawdz-przyklady.sh
set -u
export DOTNET_NOLOGO=1 DOTNET_CLI_TELEMETRY_OPTOUT=1 DOTNET_CLI_UI_LANGUAGE=en

bledy=0
for plik in wiedza/przyklady/kod/*.cs .claude/skills/postep/postep.cs; do
  if ! wynik=$(dotnet build "$plik" 2>&1); then
    echo "NIE KOMPILUJE: $plik"
    echo "$wynik" | grep -E "error CS" | head -3
    bledy=$((bledy + 1))
  fi
done

if [ "$bledy" -eq 0 ]; then
  echo "OK: wszystkie przykłady i postep.cs kompilują się"
fi

# Zepsute programy (ćwiczenia 🔧): nagłówek każdego pliku deklaruje oczekiwane zachowanie
# liniami `// CI-…`, a skrypt je odtwarza w katalogu tymczasowym:
#   CI-blad: CSxxxx          plik MA nie kompilować się, z tym kodem błędu
#   CI-ostrzezenie: CSxxxx   kompilacja wypisuje to ostrzeżenie (może być kilka linii)
#   CI-wejscie: tekst        linia podana na stdin (może być kilka)
#   CI-wyjscie: tekst        kolejne linie stdout — porównywane dokładnie, w tej kolejności
#   CI-wyjatek: Nazwa        stderr zawiera tę nazwę wyjątku (bez ścieżek ze stack trace)
#   CI-plik: nazwa=tresc     po uruchomieniu plik `nazwa` w katalogu roboczym ma dokładnie tę treść
# Kody diagnostyk są porównywane zamiast treści komunikatów (te zależą od języka systemu),
# a liczby wypisują się w kulturze niezmiennej (`3.5`, nie `3,5`).
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
if command -v timeout >/dev/null 2>&1; then LIMIT="timeout 120"; else LIMIT=""; fi

klucz() { grep "^// CI-$1: " "$2" | sed "s#^// CI-$1: ##"; }

for plik in wiedza/przyklady/zepsute/*.cs; do
  if ! grep -q "^// CI-" "$plik"; then
    echo "ZEPSUTY BEZ NAGŁÓWKA CI-: $plik"; bledy=$((bledy + 1)); continue
  fi
  katalog=$(mktemp -d)
  cp "$plik" "$katalog/p.cs"
  budowa=$($LIMIT dotnet build "$katalog/p.cs" 2>&1); kod=$?

  blad=$(klucz blad "$plik")
  if [ -n "$blad" ]; then
    if [ "$kod" -eq 0 ]; then
      echo "ZEPSUTY POWINIEN NIE KOMPILOWAĆ SIĘ, A KOMPILUJE: $plik"; bledy=$((bledy + 1))
    elif ! echo "$budowa" | grep -q "error $blad"; then
      echo "ZEPSUTY NIE KOMPILUJE SIĘ, ALE INNYM BŁĘDEM NIŻ $blad: $plik"; bledy=$((bledy + 1))
    fi
    rm -rf "$katalog"; continue
  fi
  if [ "$kod" -ne 0 ]; then
    echo "ZEPSUTY NIE KOMPILUJE SIĘ, A POWINIEN: $plik"; echo "$budowa" | grep "error CS" | head -3
    bledy=$((bledy + 1)); rm -rf "$katalog"; continue
  fi
  while IFS= read -r ostrz; do
    [ -z "$ostrz" ] && continue
    if ! echo "$budowa" | grep -q "warning $ostrz"; then
      echo "ZEPSUTY BEZ OCZEKIWANEGO OSTRZEŻENIA $ostrz: $plik"; bledy=$((bledy + 1))
    fi
  done <<< "$(klucz ostrzezenie "$plik")"

  (cd "$katalog" && klucz wejscie p.cs | $LIMIT dotnet run p.cs >out.txt 2>err.txt)
  # dotnet run powtarza ostrzeżenia kompilatora na stdout — odfiltrowane.
  grep -v ": warning CS" "$katalog/out.txt" > "$katalog/stdout.txt"

  oczekiwane=$(klucz wyjscie "$plik")
  if [ -n "$oczekiwane" ]; then
    ile=$(echo "$oczekiwane" | wc -l | tr -d ' ')
    if [ "$(head -n "$ile" "$katalog/stdout.txt")" != "$oczekiwane" ]; then
      echo "ZEPSUTY WYPISUJE CO INNEGO NIŻ NAGŁÓWEK: $plik"
      echo "--- oczekiwane"; echo "$oczekiwane"; echo "--- jest"; head -n "$ile" "$katalog/stdout.txt"
      bledy=$((bledy + 1))
    fi
  fi
  wyjatek=$(klucz wyjatek "$plik")
  if [ -n "$wyjatek" ] && ! grep -q "$wyjatek" "$katalog/err.txt"; then
    echo "ZEPSUTY BEZ OCZEKIWANEGO WYJĄTKU $wyjatek: $plik"; bledy=$((bledy + 1))
  fi
  while IFS= read -r wpis; do
    [ -z "$wpis" ] && continue
    nazwa=${wpis%%=*}; tresc=${wpis#*=}
    if [ "$(cat "$katalog/$nazwa" 2>/dev/null)" != "$tresc" ]; then
      echo "ZEPSUTY ZOSTAWIA INNY PLIK $nazwa NIŻ NAGŁÓWEK: $plik"; bledy=$((bledy + 1))
    fi
  done <<< "$(klucz plik "$plik")"
  rm -rf "$katalog"
done

if [ "$bledy" -ne 0 ]; then
  echo "BŁĘDY (zepsute): $bledy"
  exit 1
fi
echo "OK: zepsute programy zachowują się jak deklarują nagłówki CI-"
