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

# Gotowy projekt do lekcji 7.4: buduje się i wypisuje dokładnie to, co obiecuje lekcja
# (kultura niezmienna: 3.5, nie 3,5 — jak przy zepsutych niżej).
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
oczekiwane_debugger=$'15\nOK      Suma(3, 4) = 7\nBŁĄD    Srednia(3, 4): jest 3, miało być 3.5'
wyjscie_debugger=$(cd wiedza/przyklady/debugger && dotnet run 2>&1 | grep -v ": warning CS")
if [ "$wyjscie_debugger" != "$oczekiwane_debugger" ]; then
  echo "PROJEKT debugger/ WYPISUJE CO INNEGO NIŻ LEKCJA 7.4:"; echo "$wyjscie_debugger" | head -5
  bledy=$((bledy + 1))
else
  echo "OK: projekt wiedza/przyklady/debugger/ buduje się i wypisuje 15 / OK / BŁĄD"
fi

# Zepsute programy (ćwiczenia 🔧): nagłówek każdego pliku deklaruje oczekiwane zachowanie
# liniami `// CI-…`, a skrypt je odtwarza w katalogu tymczasowym:
#   CI-blad: CSxxxx          plik MA nie kompilować się, z tym kodem błędu
#   CI-ostrzezenie: CSxxxx   kompilacja wypisuje to ostrzeżenie (może być kilka linii)
#   CI-wejscie: tekst        linia podana na stdin (może być kilka)
#   CI-wyjscie: tekst        CAŁY stdout, linia po linii — nic mniej, nic więcej (brak deklaracji = stdout pusty)
#   CI-wyjatek: Nazwa        stderr zawiera tę nazwę wyjątku (bez ścieżek ze stack trace); program MA
#                            zakończyć się kodem != 0. Bez tej deklaracji kod wyjścia MUSI być 0.
#                            Przekroczenie limitu czasu jest zawsze błędem.
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

  (cd "$katalog" && klucz wejscie p.cs | $LIMIT dotnet run p.cs >out.txt 2>err.txt; echo $? >kod.txt)
  kod=$(cat "$katalog/kod.txt")
  # dotnet run powtarza ostrzeżenia kompilatora na stdout — odfiltrowane.
  grep -v ": warning CS" "$katalog/out.txt" > "$katalog/stdout.txt"

  # Całe wyjście, nie tylko początek: dodatkowa linia po poprawnym wyniku też jest rozjazdem.
  oczekiwane=$(klucz wyjscie "$plik")
  if [ "$(cat "$katalog/stdout.txt")" != "$oczekiwane" ]; then
    echo "ZEPSUTY WYPISUJE CO INNEGO NIŻ NAGŁÓWEK: $plik"
    echo "--- oczekiwane"; echo "$oczekiwane"; echo "--- jest"; cat "$katalog/stdout.txt"
    bledy=$((bledy + 1))
  fi
  wyjatek=$(klucz wyjatek "$plik")
  if [ "$kod" -eq 124 ]; then
    echo "ZEPSUTY PRZEKROCZYŁ LIMIT CZASU: $plik"; bledy=$((bledy + 1))
  elif [ -n "$wyjatek" ]; then
    if ! grep -q "$wyjatek" "$katalog/err.txt"; then
      echo "ZEPSUTY BEZ OCZEKIWANEGO WYJĄTKU $wyjatek: $plik"; bledy=$((bledy + 1))
    fi
    if [ "$kod" -eq 0 ]; then
      echo "ZEPSUTY Z WYJĄTKIEM ZAKOŃCZYŁ SIĘ KODEM 0: $plik"; bledy=$((bledy + 1))
    fi
  elif [ "$kod" -ne 0 ]; then
    echo "ZEPSUTY ZAKOŃCZYŁ SIĘ KODEM $kod, OCZEKIWANO 0: $plik"; head -3 "$katalog/err.txt"
    bledy=$((bledy + 1))
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
