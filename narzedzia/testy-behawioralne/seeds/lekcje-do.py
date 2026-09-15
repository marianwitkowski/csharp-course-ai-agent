"""Wypisuje id lekcji od 1.1 do podanej włącznie, w kolejności z wiedza/INDEX.md."""
import re, sys

indeks, ostatnia = sys.argv[1], sys.argv[2]
wynik = []
for linia in open(indeks, encoding="utf-8"):
    m = re.match(r"\|\s*(\d+\.\d+)\s*\|", linia)
    if m:
        wynik.append(m.group(1))
if ostatnia not in wynik:
    sys.exit(f"lekcja {ostatnia} nie występuje w {indeks}")
print("\n".join(wynik[: wynik.index(ostatnia) + 1]))
