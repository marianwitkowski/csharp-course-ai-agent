"""Generuje kurs/program.md z wiedza/INDEX.md — tak jak zrobiłby to skill program-kursu.

Seed nie trzyma kopii programu, żeby nie mogła się rozjechać z kanonem 50 lekcji.
"""
import re, sys

indeks, cel, tempo = sys.argv[1], sys.argv[2], sys.argv[3]
adnotacja = sys.argv[4] if len(sys.argv) > 4 else ""

PROJEKT = {
    "praca": "narzędzie przetwarzające dane z pliku, z testami",
    "narzedzia": "narzędzie zastępujące ręczną czynność, którą naprawdę wykonujesz",
    "hobby": "gra tekstowa z tabelą wyników zapisywaną do pliku",
    "szkola": "kalkulator albo solver z testami",
}
CZAS = {"<2": "~50 tygodni", "2-5": "~16-24 tygodni", "5-10": "~10-16 tygodni", "10+": "~7-10 tygodni"}

moduly, biezacy = [], None
for linia in open(indeks, encoding="utf-8"):
    m = re.match(r"### Moduł (\d+) — (.+?) \(", linia)
    if m:
        numer = int(m.group(1))
        biezacy = None if numer > 14 else (numer, m.group(2), [])   # moduł 15 to dodatek po kursie
        if biezacy:
            moduly.append(biezacy)
        continue
    m = re.match(r"\|\s*(\d+\.\d+)\s*\|\s*(.+?)\s*\|", linia)
    if m and biezacy:
        biezacy[2].append((m.group(1), m.group(2)))

wiersze = [
    "# Program kursu C#",
    "",
    f"Cel: **{cel}** · tempo: **{tempo} h/tydz** · czas trwania: **{CZAS.get(tempo, '?')}**",
    "",
    "## Jak działa kurs",
    "",
    "Uczysz się przez pytania, nie przez wykłady. Kod piszesz i uruchamiasz sam —",
    "tutor czyta, pyta i podpowiada, ale nigdy nie uruchamia twojego programu za ciebie.",
    "Każde ćwiczenie to jeden plik `.cs`, uruchamiany przez `dotnet run nazwa.cs`.",
    "Postęp zapisuje się w `postep/student.json`, więc każdą sesję zaczynasz tam,",
    "gdzie skończyłeś.",
    "",
    "## Moduły i lekcje",
    "",
]
for numer, nazwa, lekcje in moduly:
    naglowek = f"### Moduł {numer}: {nazwa}"
    if adnotacja == "skrocona" and 2 <= numer <= 7:
        naglowek += " *(ścieżka skrócona: eksperymenty i ćwiczenie ⭐; bramka — ⭐ samodzielnie)*"
    elif adnotacja == "skrocona" and 8 <= numer <= 13:
        naglowek += " *(ścieżka skrócona: najpierw ⭐ jako zadanie sprawdzające; udane → tylko eksperymenty i pułapki)*"
    wiersze.append(naglowek)
    wiersze += [f"- Lekcja {id}: {temat}" for id, temat in lekcje]
    wiersze.append("")

wiersze += [
    "## Projekt końcowy (Moduł 14)",
    "",
    PROJEKT.get(cel, "program konsolowy uzgodniony z tutorem w lekcji 14.1"),
    "",
    "## Czego w tym kursie nie ma",
    "",
    "Aplikacji okienkowych (Windows Forms, WPF), aplikacji webowych (ASP.NET Core,",
    "Blazor), baz danych i Entity Framework, wzorców architektonicznych. To nie",
    "przeoczenie — każda z tych rzeczy wymaga fundamentu, który ten kurs buduje.",
    "Mapa dalszych kroków czeka w lekcji 14.7.",
    "",
]
lekcji = sum(len(m[2]) for m in moduly)
assert lekcji == 50, f"INDEX.md dał {lekcji} lekcji kursu zamiast 50"
print("\n".join(wiersze))
