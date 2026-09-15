"""Rozkłada daty seeda w czasie — JEDYNE miejsce, w którym stan testowy jest pisany wprost.

`postep` świadomie nie ma komendy ustawiającej datę wstecz: gdyby miał, tutor mógłby
manipulować harmonogramem powtórek i historią ucznia. Seed potrzebuje przeszłości
(„26 lekcji ukończonych dzisiaj" to stan, na który tutor mógłby zareagować i zepsuć
przebieg), więc robi to sam, raz, na gotowym pliku.

użycie: rozloz-daty.py <student.json> <dni_od_rozpoczecia> <dni_od_ostatniej_sesji>
"""
import json, sys, datetime

plik = sys.argv[1]
od_startu, od_sesji = int(sys.argv[2]), int(sys.argv[3])
dzis = datetime.date.today()
start, koniec = dzis - datetime.timedelta(days=od_startu), dzis - datetime.timedelta(days=od_sesji)
if start > koniec:
    sys.exit("dni_od_rozpoczecia musi być większe niż dni_od_ostatniej_sesji")

s = json.load(open(plik, encoding="utf-8"))
lekcje = s["ukonczone_lekcje"]
rozpietosc = (koniec - start).days

def data_lekcji(i):
    if len(lekcje) < 2:
        return str(koniec)
    return str(start + datetime.timedelta(days=round(rozpietosc * i / (len(lekcje) - 1))))

kiedy = {}
for i, w in enumerate(lekcje):
    w["data"] = data_lekcji(i)
    kiedy[w["id"]] = w["data"]

for w in s["ukonczone_cwiczenia"]:
    w["data"] = kiedy.get(w["lekcja"], str(koniec))

# Temat do powtórki zauważa się na lekcji, z której pochodzi.
for w in s["do_powtorki"]:
    w["data_zauwazenia"] = kiedy.get(w.get("lekcja"), str(koniec))

s["rozpoczeto"] = str(start)
s["ostatnia_sesja"] = str(koniec)
s["liczba_sesji"] = max(1, round(len(lekcje) / 2))

json.dump(s, open(plik, "w", encoding="utf-8"), ensure_ascii=False, indent=2)
