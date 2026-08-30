---
name: quiz
description: Przeprowadza krótki quiz powtórkowy (3-7 pytań) z ukończonych przez ucznia lekcji C#. Trzy tryby — szybki (3 pyt), pełny (5-7 pyt), słabe punkty (z do_powtorki). Pyta jedno pytanie naraz, czeka na odpowiedź, daje sokratejski feedback. Użyj gdy uczeń mówi "quiz", "powtórka", "sprawdź mnie", między lekcjami lub gdy przerwa była >7 dni.
---

# Cel

Utrwalić wiedzę z **ukończonych** lekcji przez krótkie, interaktywne pytania. Quiz to **diagnoza**, nie egzamin — chodzi o wyłapanie luk, nie o ocenianie.

# Trzy tryby

| Tryb            | Liczba pytań | Z jakich lekcji                         | Kiedy                                 |
| --------------- | ------------ | --------------------------------------- | ------------------------------------- |
| Szybki ⚡       | 3            | 1-2 ostatnio ukończone lekcje           | Rozgrzewka na początku sesji          |
| Pełny 📋        | 5-7          | Wszystkie ukończone lekcje (losowy mix) | Co kilka lekcji, na życzenie ucznia   |
| Słabe punkty 🎯 | 3-5          | Tematy z `do_powtorki` w `student.json` | Gdy `do_powtorki` ma ≥3 pozycje       |

Domyślny tryb przy „quiz" bez doprecyzowania: **szybki**.

# Procedura

## Krok 1: wybór zakresu

1. Odczytaj `postep/student.json` (skill: **postep**)
2. Sprawdź `ukonczone_lekcje` i `do_powtorki`
3. Jeśli `ukonczone_lekcje` ma <2 pozycje → powiedz, że za wcześnie na quiz, zaproponuj lekcję
4. Wybierz tryb (z prośby ucznia lub z kontekstu)

## Krok 2: dobór pytań

Mieszaj **3 rodzaje** pytań:

### A. Przewidywanie wyniku („co wypisze ten kod?")
```csharp
int a = 7;
int b = 2;
Console.WriteLine(a / b);
```
Sprawdza rozumienie, nie pamięć.

### B. Konceptualne („dlaczego / kiedy")
> Kiedy użyjesz tablicy, a kiedy `List<T>`?

### C. „Znajdź błąd" / „popraw"
```csharp
// Kompiluje się bez zastrzeżeń. Co się stanie po uruchomieniu,
// gdy użytkownik wpisze "trzydzieści"?
Console.Write("Podaj wiek: ");
int wiek = int.Parse(Console.ReadLine());
Console.WriteLine(wiek + 1);
```
**Poprawna odpowiedź:** program przerwie działanie z `FormatException` — `int.Parse` rzuca wyjątkiem, gdy tekst nie jest liczbą. Bezpieczna wersja to `int.TryParse`, która zwraca `false` zamiast wywalać program.

> **Uwaga dla agenta:** nie uruchamiasz kodu, więc odpowiedzi znasz wyłącznie z tego pliku i z własnej wiedzy. Jeśli uczeń twierdzi co innego — poproś o wklejenie wyniku, zanim go poprawisz. Gdy wynik ucznia przeczy twojej odpowiedzi, **rację ma wynik**; zgłoś rozbieżność użytkownikowi zamiast upierać się przy swoim.

**Proporcja w pełnym quizie:** ~40% A, ~30% B, ~30% C.

**Pytania typu A pisz tak, żeby dało się je rozstrzygnąć w głowie.** Jeśli uczeń musi uruchomić kod, żeby odpowiedzieć — to nie jest pytanie quizowe, tylko ćwiczenie.

## Krok 3: prowadzenie quizu

**Jedno pytanie naraz.** Nie wrzucaj pięciu w jednej wiadomości.

Schemat dla każdego pytania:
1. Podaj pytanie z numerem: „Pytanie 2/5"
2. **Poczekaj** na odpowiedź
3. Po odpowiedzi:
   - **Poprawna** → krótkie potwierdzenie + pytanie pogłębiające („A gdyby `b` było `2.0`?")
   - **Częściowo** → naprowadzenie („Blisko. Jakiego typu są obie liczby?")
   - **Błędna** → NIE podawaj odpowiedzi, naprowadź pytaniem. Po 2 nieudanych próbach pokaż odpowiedź i dopisz temat do `do_powtorki`
4. Następne pytanie

**Bez punktacji po każdym pytaniu** — to nie test.

## Krok 4: podsumowanie

- Ile było **na pewno OK**, ile **z pomocą**, ile **do powtórki**
- Wymień konkretnie 1-2 tematy do utrwalenia
- Uczeń zaliczył temat z `do_powtorki` → skill **postep**, `remove-do-powtorki`
- Nowe luki → `add-do-powtorki`
- Zaproponuj następny krok: „Wracamy do lekcji X" albo „Krótkie ćwiczenie na [temat]?"

# Bank pytań — przykłady wg modułów

Pytania **generuj na żywo** pod to, co uczeń przerobił. Poniżej szablony jako inspiracja.

## Moduł 1-2 (środowisko, zmienne, typy, konwersje)
- A: Co wypisze `Console.WriteLine(7 / 2);`? A `Console.WriteLine(7.0 / 2);`?
- A: Co wypisze `Console.WriteLine((int)3.9);`?
- B: Czym różni się `const` od zwykłej zmiennej? Po co komuś coś, czego nie da się zmienić?
- B: Po co istnieje `enum`, skoro dni tygodnia można trzymać jako tekst?
- C: Czemu to nie da oczekiwanego wyniku? `string wiek = "30"; Console.WriteLine(wiek + 5);`
- C: Kiedy `int.Parse` wywali program, a `int.TryParse` nie?

## Moduł 3 (rozmowa z użytkownikiem)
- A: Co wypisze `Console.WriteLine($"{2 + 2} koty");`?
- B: Czemu `Console.ReadLine` zawsze zwraca tekst, nawet gdy użytkownik wpisał liczbę?
- C: Co jest nie tak z `int n = int.Parse(Console.ReadLine());`, gdy program dostaje dane od człowieka?

## Moduł 4 (decyzje)
- A: Co wypisze ten kod dla `x = 5`? `if (x > 3) Console.WriteLine("A"); else if (x > 1) Console.WriteLine("B");`
- B: Kiedy `switch` czyta się lepiej niż łańcuch `if / else if`?
- C: Czemu `if (x = 5)` nie kompiluje się w C#?

## Moduł 5 (pętle)
- A: Ile razy wykona się `for (int i = 0; i < 10; i += 3)`?
- B: Kiedy `while`, a kiedy `for`? Co decyduje?
- C: Co jest nie tak? `int i = 0; while (i < 5) { Console.WriteLine(i); }`

## Moduł 6 (kolekcje)
- A: Co wypisze `int[] t = new int[3]; Console.WriteLine(t[0]);`?
- A: Jaki jest ostatni poprawny indeks tablicy o długości 5?
- B: Czym różni się `Length` tablicy od `Count` listy? Czemu to dwie różne nazwy?
- C: Co się stanie przy `slownik["brak"]`, gdy takiego klucza nie ma?

## Moduł 7 (metody)
- A: Co znaczy `void` w nagłówku metody?
- B: Po co metodzie parametry, skoro mogłaby czytać zmienne z zewnątrz?
- C: Czemu ta metoda nie zmienia wartości u wywołującego? `void Zwieksz(int x) { x++; }`

## Moduł 8-9 (klasy, obiekty, OOP)
- A: Ile obiektów powstanie? `var a = new Kot(); var b = a;`
- B: Czym różni się klasa od obiektu? Podaj przykład z życia.
- B: Po co właściwość, skoro publiczne pole robi to samo krócej?
- C: Co wypisze `Console.WriteLine(kot)`, gdy klasa nie ma `ToString()`?
- C: Kiedy `override` jest konieczne, a kiedy kompilator się nie zgodzi?

## Moduł 10-11 (interfejsy, wyjątki)
- A: Czy klasa może implementować dwa interfejsy? A dziedziczyć po dwóch klasach?
- B: Interfejs nie ma żadnego kodu. Do czego więc służy?
- C: Co wypisze blok `finally`, gdy w `try` był `return`?

## Moduł 12-13 (pliki, JSON, LINQ)
- A: Co zwróci `lista.Where(x => x > 10)`, gdy żaden element nie jest większy od 10?
- B: Czemu `File.ReadAllText` może rzucić wyjątkiem, choć kod wygląda poprawnie?
- C: Co jest nie tak z odczytem pliku bez `try/catch` w programie dla użytkownika?

# Twarde zasady

- **Tylko ukończone lekcje.** Nie pytaj o materiał, którego uczeń nie miał.
- **Jedno pytanie naraz.** Czekanie na odpowiedź to część quizu.
- **Nie uruchamiaj kodu z pytań**, żeby sprawdzić własną odpowiedź. Jeśli nie jesteś pewien wyniku — nie dawaj tego pytania.
- **Sokratejskie naprowadzanie**, nie podpowiedzi typu „to chyba dzielenie całkowite".
- **Bez ocen liczbowych** („5/7", „70%"). Mów jakościowo.
- **Aktualizuj `student.json`** przez skill **postep** po każdym quizie.
- **Quiz to nie lekcja.** Duża luka → zaproponuj powrót do lekcji, ale nie tłumacz materiału w trakcie quizu.

# Gdy uczeń wszystko wie

- Pochwal konkretnie
- Zaproponuj **gwiazdkowe** ćwiczenie z bieżącej lekcji (skill: **cwiczenie**)
- Albo przyspiesz przejście do następnej lekcji

# Gdy uczeń się „rozsypuje"

Jeśli >50% pytań idzie źle:
- Przerwij po 3-4 pytaniach (nie męcz)
- Powiedz wprost: „Widzę, że temat X wymaga powtórki — wróćmy do lekcji N"
- Dopisz tematy do `do_powtorki`
- NIE rób z tego porażki — to diagnoza, którą zrobiliście razem
