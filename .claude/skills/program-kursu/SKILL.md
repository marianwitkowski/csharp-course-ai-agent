---
name: program-kursu
description: Generuje plik kurs/program.md — spersonalizowany program 14 modułów / 49 lekcji podstaw języka C# i platformy .NET, na podstawie wiedza/INDEX.md. Dostosowuje akcenty do celu ucznia (praca/narzędzia/hobby/szkoła) i deklarowanego tempa. Użyj raz, podczas onboardingu, po krótkim wywiadzie z uczniem.
---

# Cel

Stworzyć `kurs/program.md` — plan kursu, do którego uczeń i tutor będą wracać. To **kompas**, nie sztywne tory.

# Źródło prawdy

**Zawsze** opieraj plan na pliku `wiedza/INDEX.md`. Nie wymyślaj modułów ani lekcji — tabele w INDEX.md to kanon: **14 modułów, 49 lekcji**.

Jeśli `wiedza/INDEX.md` nie istnieje → coś jest nie tak z repozytorium. Powiedz uczniowi i nie generuj programu z pamięci.

# Wejście

Wymagane od ucznia (przed wywołaniem skill):
- **Cel:** praca / narzędzia i automatyzacja / hobby / szkoła / inne
- **Czas tygodniowo:** <2h / 2-5h / 5-10h / 10+h
- **Doświadczenie z programowania:** brak (domyślnie) / coś dotykał / inny język

# Procedura

1. **Wczytaj** `wiedza/INDEX.md` — źródło struktury kursu
2. **Skopiuj kanon** (14 modułów, 49 lekcji)
3. **Personalizuj** akcenty wg celu (patrz niżej)
4. **Dostosuj tempo** wg dostępnego czasu
5. **Zapisz** do `kurs/program.md`

# Personalizacja wg celu

Personalizacja dotyczy **akcentów i projektu końcowego**, nie struktury. Wszystkie 49 lekcji zostaje w tej samej kolejności.

- **Cel: praca (programista .NET)** → mocniej moduły 8-10 (klasy, OOP, interfejsy — to jest to, o co pytają na rozmowach) i 14.4 (testy); projekt: narzędzie przetwarzające dane z pliku, z testami
- **Cel: narzędzia i automatyzacja** → mocniej moduł 12 (pliki, JSON, argumenty CLI) i 13 (LINQ do przemielenia danych); projekt: narzędzie zastępujące ręczną czynność, którą uczeń faktycznie wykonuje
- **Cel: hobby / gry tekstowe** → mocniej moduły 4-5 (decyzje, pętle) i 6 (kolekcje); projekt: gra tekstowa z tabelą wyników zapisywaną do pliku
- **Cel: szkoła / algorytmy** → mocniej moduł 7 (metody) i 6 (tablice, listy); projekt: kalkulator albo solver z testami
- **Cel: inny** → dopytaj o konkret, dobierz akcent

**Moduły 8-10 (programowanie obiektowe) zostają zawsze**, niezależnie od celu. C# jest językiem obiektowym do szpiku — uczeń, który ominie klasy i interfejsy, nie przeczyta żadnego prawdziwego kodu C#, jaki spotka.

# Tempo

Lekcja trwa 40-60 minut plus ćwiczenie.

| Czas/tydz | Lekcji/tydz | Czas trwania kursu |
| --------- | ----------- | ------------------ |
| <2h       | 1           | ~49 tygodni        |
| 2-5h      | 2-3         | ~16-24 tygodni     |
| 5-10h     | 3-5         | ~10-16 tygodni     |
| 10+h      | 5-7         | ~7-10 tygodni      |

**Uwaga o module 14:** projekt rozciąga się na kilka sesji (lekcja 14.3 jest prowadzona wielokrotnie). Do szacunku doliczaj 2-4 dodatkowe sesje.

**Nie sprzedawaj tych liczb jako obietnicy.** Uczeń, który usłyszy „10 tygodni" i po 12 jest w połowie, uzna, że mu nie idzie — a idzie mu normalnie.

# Format pliku `kurs/program.md`

```markdown
# Program kursu C# / .NET — [imię]

**Cel:** [praca / narzędzia / hobby / szkoła]
**Tempo:** [X lekcji / tydzień]
**Rozpoczęto:** YYYY-MM-DD
**Wersja .NET:** [z srodowisko.dotnet_version] (minimum kursu: 10.0)
**Bazujemy na:** wiedza/INDEX.md

## Jak działa kurs

Uczysz się przez pytania, nie przez wykłady. Kod piszesz i uruchamiasz sam —
tutor czyta, pyta i podpowiada, ale nigdy nie uruchamia twojego programu za ciebie.
Każde ćwiczenie to jeden plik `.cs`, uruchamiany przez `dotnet run nazwa.cs`.
Postęp zapisuje się w `postep/student.json`, więc każdą sesję zaczynasz tam,
gdzie skończyłeś.

## Moduły i lekcje

### Moduł 1: Wprowadzenie i środowisko
- Lekcja 1.1: Czym jest C# i .NET — i pierwszy program
- Lekcja 1.2: Edytor, terminal, formatowanie kodu

### Moduł 2: Zmienne i typy
- Lekcja 2.1: Zmienne i typy proste
- Lekcja 2.2: Stałe i typy wyliczeniowe (`const`, `enum`)
- Lekcja 2.3: Konwersje typów — `Parse`, `TryParse`
- Lekcja 2.4: Operatory i wyrażenia

[...kontynuuj wg INDEX.md, wszystkie 14 modułów, 49 lekcji...]

## Projekt końcowy (Moduł 14)

[Spersonalizowany pod cel ucznia — 2-3 propozycje do wyboru,
 wszystkie w formie programu konsolowego]

## Czego w tym kursie nie ma

Aplikacji okienkowych (Windows Forms, WPF), aplikacji webowych (ASP.NET Core,
Blazor), baz danych i Entity Framework, wzorców architektonicznych. To nie
przeoczenie — każda z tych rzeczy wymaga fundamentu, który ten kurs buduje.
Mapa dalszych kroków czeka w lekcji 14.7.
```

# Twarde zasady

- **Źródłem prawdy jest `wiedza/INDEX.md`.** Nie wymyślaj lekcji, nie pomijaj modułów bez zgody ucznia.
- **Trzymaj się 14 modułów i kolejności.** Sekcja „Zależności między modułami" w INDEX.md wymienia trzy miejsca, których przestawić nie wolno: 7→8→9→10 (metody → klasy → dziedziczenie → interfejsy) oraz 11 przed 12 (wyjątki przed plikami).
- **Nie wymyślaj modułów typu „ASP.NET", „Entity Framework", „WPF"** — te tematy są wzmiankowane w lekcji 14.7 (mapa ekosystemu), nigdy jako osobne lekcje. Uczeń, który prosi o taki moduł, prosi o inny kurs; powiedz to życzliwie i wprost.
- **Nie skracaj kursu przez wycięcie modułów 8-9 (klasy i OOP)** — nawet jeśli uczeń chce szybciej. W C# klasa jest jednostką organizacji kodu; bez modułów 8-9 lekcje 10-14 nie mają się o co oprzeć.
- Jeśli uczeń chce krótszej wersji → zaproponuj zatrzymanie się po module 10 i wrócenie do 11-14 później. Nie wycinaj środka.
- Plik nadpisujesz **tylko jeśli** uczeń świadomie chce zmienić program (np. zmienił się cel).

# Po wygenerowaniu

Pokaż uczniowi **spis modułów** (nie cały plik) i zapytaj, czy chce coś zmienić, zanim ruszycie z lekcją 1.1. Dodaj jedno zdanie kotwiczące:

> „Czterdzieści dziewięć lekcji brzmi dużo, ale pierwsze siedem to podstawy, które przerobisz szybciej, niż myślisz. Prawdziwy próg jest w module 8 — tam zaczyna się to, co w C# jest naprawdę własne."
