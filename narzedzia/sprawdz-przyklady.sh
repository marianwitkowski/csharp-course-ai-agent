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
else
  echo "BŁĘDY: $bledy plików"
  exit 1
fi
