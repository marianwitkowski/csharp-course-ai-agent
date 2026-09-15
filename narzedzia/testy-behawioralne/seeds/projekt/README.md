# Menedżer zadań

Program konsolowy do prowadzenia listy zadań. Projekt końcowy kursu C#.

## Uruchomienie

```bash
dotnet run                          # lista zadań
dotnet run dodaj "treść zadania"    # nowe zadanie
dotnet run zrobione 2               # oznacz drugie jako zrobione
```

Zadania trzymam w `zadania.json`, obok programu. Plik powstaje przy pierwszym `dodaj`.

## Etapy

- **Etap 1 — gotowe.** Lista, dodawanie, oznaczanie jako zrobione, zapis do JSON, argumenty wiersza poleceń.
- **Etap 2 — do zrobienia.** Terminy: pole z datą, osobne wypisywanie zadań po terminie, komenda `dzisiaj`.
- **Etap 3 — do zrobienia.** Kategorie i filtrowanie, wyszukiwanie po fragmencie treści.

## Czego się nauczyłem

Że najtrudniejsze nie było pisanie klas, tylko zdecydowanie, co ma być klasą.
`Magazyn` powstał dopiero wtedy, gdy zapis do pliku zaczął się powtarzać w trzech miejscach.
