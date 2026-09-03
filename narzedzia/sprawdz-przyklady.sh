#!/usr/bin/env bash
# Buduje każdy przykład z wiedza/przyklady/kod/ i narzędzie postep.
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

# Zepsute programy (ćwiczenia 🔧): plik z „nie kompiluje się" w nagłówku MA nie kompilować się,
# każdy inny ma się kompilować (błąd jest w logice albo wychodzi dopiero w trakcie działania).
for plik in wiedza/przyklady/zepsute/*.cs; do
  if head -4 "$plik" | grep -q "nie kompiluje się"; then
    if dotnet build "$plik" >/dev/null 2>&1; then
      echo "ZEPSUTY POWINIEN NIE KOMPILOWAĆ SIĘ, A KOMPILUJE: $plik"; bledy=$((bledy + 1))
    fi
  elif ! dotnet build "$plik" >/dev/null 2>&1; then
    echo "ZEPSUTY NIE KOMPILUJE SIĘ, A POWINIEN: $plik"; bledy=$((bledy + 1))
  fi
done

if [ "$bledy" -ne 0 ]; then
  echo "BŁĘDY (zepsute): $bledy plików"
  exit 1
fi
echo "OK: zepsute programy zachowują się jak opisano w nagłówkach"
