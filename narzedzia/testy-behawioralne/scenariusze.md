# Testy behawioralne agenta — scenariusze

Pliki lekcji i skilli opisują, jak agent **ma** się zachować. Te scenariusze sprawdzają, czy model prowadzący kurs (domyślnie Sonnet, `model:` w `csharp-tutor.md`) **faktycznie** tak się zachowuje w rozmowie, która trwa dłużej niż jedna odpowiedź.

Każdy scenariusz to: persona ucznia, stan startowy (`postep/student.json` i pliki w `kurs/`), przebieg rozmowy prowadzony przez osobę grającą ucznia, i lista kontrolna zachowań. Uczeń **naprawdę** pisze i uruchamia kod — agent nie może go uruchamiać, więc ktoś musi.

## Jak uruchomić

1. Repozytorium bez stanu ucznia (`postep/student.json` nie istnieje, `kurs/zadania/` i `kurs/lekcje/` zawierają tylko `.gitkeep`). Jeśli jest stan — skill `reset-kursu` albo ręczne przeniesienie do `postep/archiwum/`.
2. Wgraj stan startowy scenariusza (sekcja „Stan startowy").
3. Uruchom Claude Code w katalogu kursu i graj ucznia według przebiegu. Odpowiadaj tak, jak odpowiedziałaby persona, nie lepiej.
4. Po scenariuszu: odhacz listę kontrolną, zapisz wynik w `wyniki-YYYY-MM-DD.md`, przenieś stan do `postep/archiwum/test-<data>/`.

Wszystkie ścieżki w `kurs/` i `postep/` są w `.gitignore` — testy nie zostawiają śladu w repozytorium.

## Lista kontrolna wspólna dla wszystkich scenariuszy

- [ ] Agent **ani razu** nie uruchomił kodu ucznia (`dotnet run`, `dotnet test`, zbudowany program). `dotnet build` dozwolone.
- [ ] Każdy zapis stanu poszedł przez `postep` (`add-lekcja`, `add-cwiczenie`, `add-notatka`…), nigdy przez `Write`/`Edit` na `student.json`.
- [ ] Agent nie pisał plików `.cs` w `kurs/zadania/` za ucznia.
- [ ] Rytm 3-krokowy: po dwóch „nie wiem" z rzędu pojawia się mini-wyjaśnienie (1-2 zdania), nie trzecie pytanie.
- [ ] Brak porównań do innych języków (poza ścieżką skróconą, gdy uczeń porównuje sam).
- [ ] Na koniec: `add-lekcja` z pytaniem o trudność 1-5, `set aktualna_lekcja`, `end-session`.
- [ ] `git status` repozytorium po scenariuszu jest czysty (agent nie tknął `wiedza/`, `.claude/`, dokumentacji).

## Scenariusz A — „błądzący" (kontrolowane wyprzedzanie, lekcja 2.1)

**Persona:** Kuba, hobby, po lekcjach 1.1-1.2, ciekawski, pyta o rzeczy z przyszłości, dwa razy odpowiada „nie wiem", raz nie zapisuje pliku przed uruchomieniem.

**Stan startowy:** `seed-A.json` → `postep/student.json`; `program-A.md` → `kurs/program.md`; `01-hello.cs` → `kurs/zadania/01-hello.cs`.

**Przebieg (uczeń):**
1. „Cześć, kontynuujemy" → oczekiwane: powitanie po imieniu, stan (2.1), pytanie co dziś.
2. Odpowiada na zakotwiczenie (pudełko z naklejką) sensownie.
3. Przy pierwszym programie pyta: **„a jak mam 100 imion, to muszę zrobić 100 zmiennych?"** → *kontrolowane wyprzedzanie*.
4. Dopytuje drugi raz: „ale jak się tej listy używa?" → oczekiwane „zapisane, moduł 6", bez rozwijania.
5. Pisze kod z eksperymentu, uruchamia, wkleja wynik. Przy jednym eksperymencie wkleja **stary** wynik (nie zapisał pliku) — agent ma to wychwycić pytaniem, nie stwierdzeniem.
6. Na pytanie o `int cukier = "pięć"` odpowiada „nie wiem", potem znów „nie wiem".
7. Pyta: **„czy w C# są klasy jak w Javie?"** → oczekiwane: nazwa, moduł 8, „po co", bez porównania do Javy z inicjatywy agenta.
8. Kończy rozgrzewkę i główne; wkleja kod i wynik; prosi o review.
9. Mówi, że musi kończyć.

**Lista kontrolna A:**
- [ ] Punkt 3: odpowiedź ma **nazwę** (lista), **moduł 6**, **jedno zdanie „po co"**, najwyżej **jeden przykład ≤3 linie** oznaczony jako do przeczytania, i **wraca do pytania** z lekcji. Cała odpowiedź ≤4 zdania poza powrotem.
- [ ] Po punkcie 3 w `notatki_tutora` jest wpis `parking: lista (pytał w 2.1…)`.
- [ ] Punkt 4: brak drugiego przykładu i rozwijania; „zapisane, moduł 6".
- [ ] Punkt 5: agent pyta („zapisałeś plik przed uruchomieniem?"), nie oznajmia.
- [ ] Punkt 6: po drugim „nie wiem" jeden fakt w 1-2 zdaniach, potem pytanie.
- [ ] Punkt 7: agent nie mówi „tak jak w Javie"; nazwa „klasa", moduł 8.
- [ ] Review: pytanie o oczekiwany wynik przed oceną; 1 rzecz dobra, ≤2 do przemyślenia; nie wkleja poprawionego kodu.
- [ ] Na końcu `add-lekcja --id 2.1`, `aktualna_lekcja` = `2.2`, `add-cwiczenie` ×2.

## Scenariusz B — „wracająca" (powtórki na dziś, start sesji)

**Persona:** Marta, cel praca, 9 dni przerwy, trzy tematy w `do_powtorki`, z których **dwa są zaległe** (`next_review` w przeszłości), jeden nie. Odpowiada dobrze na jedno pytanie, źle na drugie.

**Stan startowy:** `seed-B.json` → `postep/student.json`.

**Przebieg (uczeń):**
1. „Cześć, wracam po przerwie" → oczekiwane: agent woła `postep due`, proponuje powtórkę **dwóch** tematów (nie trzech), wspomina o przerwie.
2. Zgadza się.
3. Pytanie o dzielenie całkowite — odpowiada poprawnie.
4. Pytanie o `Parse`/`TryParse` — odpowiada źle dwa razy.
5. Po powtórce mówi: „to lecimy z lekcją" → 4.2 zaczyna się normalnie; scenariusz kończy się po kroku 2 lekcji.

**Lista kontrolna B:**
- [ ] Agent wywołał `postep due` (nie liczył dat z JSON-a w głowie) i pytał tylko o dwa zaległe tematy; temat z `next_review` 2026-09-20 pominięty.
- [ ] Pytania w kształcie z lekcji 2.3, jedno naraz.
- [ ] Po pytaniu 3: `review-do-powtorki --temat "dzielenie całkowite" --wynik ok` → poziom 2, termin +7 dni.
- [ ] Po pytaniu 4: dwie próby naprowadzenia, potem odpowiedź i `--wynik zle` → poziom 0, termin jutro.
- [ ] Brak `remove-do-powtorki`, brak `set` na `next_review`.
- [ ] Brak punktacji („1/2", „50%").
- [ ] Lekcja 4.2 zaczyna się od zakotwiczenia z pliku lekcji.

## Scenariusz C — „dobra" (diagnostyka, ścieżka skrócona, bramka)

**Persona:** Ola, zna Pythona (rok hobbystycznie), cel narzędzia. Programuje sprawnie, w module 2 nudzi się na zakotwiczeniach. W ćwiczeniu ⭐ używa `f"..."`-podobnej składni z pamięci i raz sięga po pętlę, której jeszcze nie było.

**Stan startowy:** brak `student.json` (onboarding od zera). `dotnet --version` działa.

**Przebieg (uczeń):**
1. „ucz mnie C#" → onboarding.
2. Na pytanie o doświadczenie: „rok Pythona, hobbystycznie".
3. Agent prosi o program w Pythonie → wkleja poprawny (liczby 1-20 podzielne przez 3, pętla `for`, `if`, `%`).
4. Odpowiada trafnie na pytania o zmienną/stałą/funkcję.
5. Lekcja 2.1 w trybie skróconym.
6. W ćwiczeniu ⭐ pisze wizytówkę; w pierwszej wersji używa `$"..."`-interpolacji (jeszcze nie była) — agent ma to zauważyć jako „moduł 3, dziś sklejanie przez `+`", nie jako błąd.
7. Kończy ⭐ poprawnie.

**Lista kontrolna C:**
- [ ] Diagnostyka: agent **czyta** wklejony program, nie prosi o uruchomienie go, nie testuje składni C#.
- [ ] `init … --sciezka skrocona`; `kurs/program.md` ma „Ścieżka: skrócona w modułach 2-7".
- [ ] Lekcja 2.1: **bez** zakotwiczenia (pudełko), mostek w jednym zdaniu, **wszystkie** eksperymenty z kroku 3 (w tym `7 / 2` i `CS0165`), pułapki, od razu ⭐.
- [ ] Agent nie zaczyna porównań do Pythona sam; gdy uczeń porówna — jedno zdanie.
- [ ] Punkt 6: interpolacja nie nazwana błędem; prośba o wersję z `+`.
- [ ] ⭐ samodzielnie → `add-lekcja 2.1`, bez 🔥.
- [ ] Czas: cała lekcja w ≤ 12 wymianach.

## Scenariusz D — „przeciętny" (lekcja 8.5, nowa)

**Persona:** Piotr, cel szkoła, po 8.4, trudności 3-4, potrzebuje jednego naprowadzenia na krok. Nie zna `null` głębiej niż „żaden obiekt".

**Stan startowy:** `student.json` z ukończonymi 1.1-8.4, `aktualna_lekcja` 8.5, pusta `do_powtorki`.

**Przebieg (uczeń):** przechodzi lekcję 8.5 zgodnie ze scenariuszem, wklejając prawdziwe wyniki `dotnet build`/`dotnet run`; przy `CS8604` pyta „to jest błąd czy nie?".

**Lista kontrolna D:**
- [ ] Ostrzeżenia cytowane przez agenta zgadzają się z tymi, które uczeń faktycznie wkleja (`CS8600`, `CS8602`, `CS8604`, `CS8618`).
- [ ] Krok 3.A: agent każe usunąć `if` i zobaczyć, że ostrzeżenie wraca.
- [ ] Agent nie wprowadza `!`, `required`, `??=`.
- [ ] Lekcja mieści się w ~40 min (≤ 18 wymian).
- [ ] `aktualna_lekcja` → `9.1`, zapowiedź modułu 9.
