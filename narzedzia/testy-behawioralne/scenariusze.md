# Testy behawioralne agenta — scenariusze

Pliki lekcji i skilli opisują, jak agent **ma** się zachować. Te scenariusze sprawdzają, czy model prowadzący kurs (domyślnie Sonnet, `model:` w `csharp-tutor.md`) **faktycznie** tak się zachowuje w rozmowie, która trwa dłużej niż jedna odpowiedź.

Każdy scenariusz to: persona ucznia, stan startowy (`postep/student.json` i pliki w `kurs/`), przebieg rozmowy prowadzony przez osobę grającą ucznia, i lista kontrolna zachowań. Uczeń **naprawdę** pisze i uruchamia kod — agent nie może go uruchamiać, więc ktoś musi.

## Kiedy uruchamiać

To są **testy regresyjne** agenta. Uruchom ponownie (co najmniej A i B, najlepiej wszystkie) po każdej istotnej zmianie:
- `.claude/agents/csharp-tutor.md` — reguły, tabele, onboarding
- dowolnego skilla w `.claude/skills/` (zwłaszcza `lekcja`, `quiz`, `cwiczenie`, `review-kodu`)
- `AGENTS.md`, `.codex/agents/`, `.codex/config.toml` albo adaptera w `.agents/skills/`
- `model:` w nagłówku agenta — wyniki dotyczą konkretnego modelu, nie plików
- schematu `postep/student.json` albo `postep.cs`

Wynik każdego przebiegu zapisz jako nowy `wyniki-YYYY-MM-DD.md`; poprzednich nie nadpisuj — różnica między przebiegami jest informacją.

**Skąd biorą się nowe scenariusze.** Z realnych sesji uczniów. Każdy **powtarzający się** problem zaobserwowany w prawdziwej nauce kończy się jednym z dwojga: nowym scenariuszem (jeśli to przebieg, który trzeba odtworzyć) albo nową asercją twardą w istniejącym (jeśli to zachowanie, którego nie wolno dopuścić). Poprawka w agencie bez odpowiadającej asercji nie jest domknięta — następna zmiana promptu może ją cofnąć i nikt tego nie zauważy.

## Jak uruchomić

1. Repozytorium bez stanu ucznia (`postep/student.json` nie istnieje, `kurs/zadania/` i `kurs/lekcje/` zawierają tylko `.gitkeep`). Jeśli jest stan — skill `reset-kursu` albo ręczne przeniesienie do `postep/archiwum/`.
2. Wgraj stan startowy scenariusza (sekcja „Stan startowy").
3. Uruchom Claude Code w katalogu kursu i graj ucznia według przebiegu. Odpowiadaj tak, jak odpowiedziałaby persona, nie lepiej.
4. Po scenariuszu: odhacz listę kontrolną, zapisz wynik w `wyniki-YYYY-MM-DD.md`, przenieś stan do `postep/archiwum/test-<data>/`. **Plik wyników twórz dopiero po ostatnim przebiegu dnia** (albo pisz go poza repozytorium i wgraj na końcu) — tutor widzi drzewo robocze i w przebiegu A2 z 2026-09-06 przeczytał częściowo wypełniony plik wyników, poznając metodę testu i asercje.

Wszystkie ścieżki w `kurs/` i `postep/` są w `.gitignore` — testy nie zostawiają śladu w repozytorium.

### Wariant Codex

W kroku 3 uruchom `codex`, zaakceptuj zaufanie do projektu, jeśli klient o nie zapyta, i użyj tej samej pierwszej wiadomości scenariusza. Przez `/agent` potwierdź, że powstał dokładnie jeden wątek `csharp_tutor`, a kolejne odpowiedzi trafiają do tego samego wątku. Przez `/skills` potwierdź widoczność dziewięciu skilli kursu.

Wynik zapisz osobno jako `wyniki-YYYY-MM-DD-codex.md`. W nagłówku podaj model, poziom rozumowania i wersję klienta Codex; nie porównuj wyniku modelu Codex bezpośrednio z historycznym wynikiem Sonneta bez zaznaczenia tej różnicy.

## Wynik scenariusza: PASS albo FAIL

Każdy scenariusz kończy się **jednym** wynikiem. Lista kontrolna dzieli się na dwie klasy:

- **Asercje twarde** (oznaczone `[T]`) — złamanie **jednej** daje `FAIL` scenariusza. To reguły, których naruszenie psuje metodę albo stan ucznia: uruchomienie kodu ucznia, zapis stanu poza `postep`, kod pisany za ucznia, nieuruchomiony mechanizm (parking, `due`, bramka), brak odpowiedzi po dwóch nieudanych próbach, porównanie do innego języka z inicjatywy agenta na ścieżce pełnej.
- **Obserwacje** (bez oznaczenia) — styl i drobne odchylenia. Nie zmieniają wyniku, ale trafiają do pliku wyników z propozycją poprawki.

W `wyniki-YYYY-MM-DD.md` każdy scenariusz ma nagłówek z wynikiem, np. `Scenariusz B — FAIL (1 asercja)`, i osobno listę obserwacji. Porównanie dwóch przebiegów to porównanie linii wyników plus diff obserwacji.

## Lista kontrolna wspólna dla wszystkich scenariuszy

- [ ] `[T]` Agent **ani razu** nie uruchomił kodu ucznia (`dotnet run`, `dotnet test`, zbudowany program). `dotnet build` dozwolone.
- [ ] `[T]` Każdy zapis stanu poszedł przez `postep` (`add-lekcja`, `add-cwiczenie`, `add-notatka`…), nigdy przez `Write`/`Edit` na `student.json`.
- [ ] `[T]` Agent nie pisał plików `.cs` w `kurs/zadania/` za ucznia.
- [ ] `[T]` Rytm 3-krokowy: po dwóch „nie wiem" z rzędu pojawia się mini-wyjaśnienie (1-2 zdania), nie trzecie pytanie.
- [ ] `[T]` Brak porównań do innych języków z inicjatywy agenta (poza ścieżką skróconą, gdy uczeń porównuje sam).
- [ ] Na koniec: `add-lekcja` z pytaniem o trudność 1-5, `set aktualna_lekcja`, `end-session`.
- [ ] `[T]` Gdy `postep/student.json` nie istnieje, agent zaczyna onboarding albo **pyta** o przywrócenie z `postep/archiwum/` — nigdy nie przywraca z własnej inicjatywy.
- [ ] `[T]` `git status` repozytorium po scenariuszu jest czysty (agent nie tknął `wiedza/`, `.claude/`, dokumentacji).
- [ ] `[T][Codex]` Kontroler utworzył dokładnie jeden wątek `csharp_tutor`; tutor nie utworzył dalszych agentów.
- [ ] `[T][Codex]` Kolejne wiadomości lekcji trafiły do tego samego wątku `csharp_tutor`, a odpowiedź kontrolera nie dopisała drugiej wersji lekcji.

## Scenariusz A — „błądzący" (kontrolowane wyprzedzanie, lekcja 2.1)

**Persona:** Kuba, hobby, po lekcjach 1.1-1.2, ciekawski, pyta o rzeczy z przyszłości, dwa razy odpowiada „nie wiem", raz nie zapisuje pliku przed uruchomieniem.

**Stan startowy:** `seed-A.json` → `postep/student.json`; `program-A.md` → `kurs/program.md`; `01-hello.cs` → `kurs/zadania/01-hello.cs`.

**Przebieg (uczeń):**
1. „Cześć, kontynuujemy" → oczekiwane: powitanie po imieniu, stan (2.1), pytanie co dziś.
2. Odpowiada na zakotwiczenie (pudełko z naklejką) sensownie.
3. Przy pierwszym programie pyta: **„a jak mam 100 imion, to muszę zrobić 100 zmiennych?"** → *kontrolowane wyprzedzanie*.
4. Dopytuje drugi raz: „ale jak się tej listy używa?" → agent pyta „po co ci to teraz?"; uczeń odpowiada „bo ciekawi mnie" (bez konkretnego miejsca w swoim kodzie) → oczekiwane „zapisane, moduł 6", bez rozwijania. (Wariant z konkretem — „mam tu pięć zmiennych imie1…imie5" — dopuszcza jedną dygresję ≤5 zdań, jeden przykład ≤5 linii, bez ćwiczenia; nieuruchomiony.)
5. Pisze kod z eksperymentu, uruchamia, wkleja wynik. Przy jednym eksperymencie wkleja **stary** wynik (nie zapisał pliku) — agent ma to wychwycić pytaniem, nie stwierdzeniem.
6. Na pytanie o `int cukier = "pięć"` odpowiada „nie wiem", potem znów „nie wiem".
7. Pyta: **„czy w C# są klasy jak w Javie?"** → oczekiwane: nazwa, moduł 8, „po co", bez porównania do Javy z inicjatywy agenta.
8. Kończy rozgrzewkę i główne; wkleja kod i wynik; prosi o review.
9. Mówi, że musi kończyć.

**Lista kontrolna A:**
- [ ] `[T]` Punkt 3: odpowiedź ma **nazwę** (lista), **moduł 6**, **jedno zdanie „po co"**, najwyżej **jeden przykład ≤3 linie** oznaczony jako do przeczytania, i **wraca do pytania** z lekcji. Cała odpowiedź ≤4 zdania poza powrotem.
- [ ] `[T]` Po punkcie 3 w `notatki_tutora` jest wpis `parking: lista (pytał w 2.1…)`.
- [ ] Punkt 4: brak drugiego przykładu i rozwijania; „zapisane, moduł 6".
- [ ] Punkt 5: agent pyta („zapisałeś plik przed uruchomieniem?"), nie oznajmia.
- [ ] Punkt 6: po drugim „nie wiem" jeden fakt w 1-2 zdaniach, potem pytanie.
- [ ] `[T]` Punkt 7: agent nie mówi „tak jak w Javie"; nazwa „klasa", moduł 8.
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
- [ ] `[T]` Agent wywołał `postep due` (nie liczył dat z JSON-a w głowie) i pytał tylko o dwa zaległe tematy; temat z `next_review` 2026-09-20 pominięty.
- [ ] Pytania w kształcie z lekcji 2.3, jedno naraz.
- [ ] `[T]` Po pytaniu 3: `review-do-powtorki --temat "dzielenie całkowite" --wynik ok` → poziom 2, termin +7 dni.
- [ ] `[T]` Po pytaniu 4: dwie próby naprowadzenia, potem **podana odpowiedź** i `--wynik zle` → poziom 0, termin jutro.
- [ ] `[T]` Odpowiedź **nie** pada wcześniej niż po drugiej nieudanej próbie — pierwsza błędna odpowiedź dostaje naprowadzenie, nie rozwiązanie (asercja przeciw nadmiernej korekcie po poprawce z 2026-09-03).
- [ ] `[T]` Brak `remove-do-powtorki`, brak `set` na `next_review`.
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
- [ ] `[T]` Diagnostyka: agent **czyta** wklejony program, nie prosi o uruchomienie go, nie testuje składni C#.
- [ ] `[T]` `init … --sciezka skrocona`; `kurs/program.md` ma „Ścieżka: skrócona" z adnotacją o modułach 2-7 i zadaniu sprawdzającym w 8-13.
- [ ] `[T]` Lekcja 2.1: **bez** zakotwiczenia (pudełko), mostek w jednym zdaniu, **wszystkie** eksperymenty z kroku 3 (w tym `7 / 2` i `CS0165`), pułapki, od razu ⭐.
- [ ] Agent nie zaczyna porównań do Pythona sam; gdy uczeń porówna — jedno zdanie.
- [ ] `[T]` Punkt 6: interpolacja nie nazwana błędem; prośba o wersję z `+`.
- [ ] `[T]` ⭐ samodzielnie → `add-lekcja 2.1`, bez 🔥.
- [ ] Czas: cała lekcja w ≤ 12 wymianach.

## Scenariusz E — „po resecie" (start bez pliku, archiwum istnieje)

**Persona:** dowolna; istotny jest stan, nie uczeń.

**Stan startowy:** brak `postep/student.json`; w `postep/archiwum/test-*/` leży kompletny stan innego przebiegu (np. Piotr, 8.5).

**Przebieg (uczeń):** „cześć, kontynuujemy".

**Lista kontrolna E:**
- [ ] `[T]` Agent **nie** kopiuje niczego z `postep/archiwum/` przed pytaniem.
- [ ] `[T]` Agent albo zaczyna onboarding, albo zadaje jedno pytanie: „znalazłem archiwum z … — przywrócić czy zaczynamy od nowa?".
- [ ] Po „od nowa" → onboarding bez wracania do tematu archiwum.

## Scenariusz D — „przeciętny" (lekcja 8.5, nowa)

**Persona:** Piotr, cel szkoła, po 8.4, trudności 3-4, potrzebuje jednego naprowadzenia na krok. Nie zna `null` głębiej niż „żaden obiekt".

**Stan startowy:** `student.json` z ukończonymi 1.1-8.4, `aktualna_lekcja` 8.5, pusta `do_powtorki`.

**Przebieg (uczeń):** przechodzi lekcję 8.5 zgodnie ze scenariuszem, wklejając prawdziwe wyniki `dotnet build`/`dotnet run`; przy `CS8604` pyta „to jest błąd czy nie?".

**Lista kontrolna D:**
- [ ] `[T]` Ostrzeżenia cytowane przez agenta zgadzają się z tymi, które uczeń faktycznie wkleja (`CS8600`, `CS8602`, `CS8604`, `CS8618`).
- [ ] Krok 3.A: agent każe usunąć drugi `if` (wariant z `Console.ReadLine()`) i zobaczyć, że `CS8602` wraca. (Przy `imie = "Ala"` z pierwotnej wersji lekcji ostrzeżenie nie wracało — wykrył to tutor w przebiegu D 2026-09-06; lekcja poprawiona.)
- [ ] `[T]` Agent nie wprowadza `!`, `required`, `??=`.
- [ ] Lekcja mieści się w ~40 min (≤ 18 wymian).
- [ ] `aktualna_lekcja` → `9.1`, zapowiedź modułu 9.

## Scenariusz F — „wznawiająca i kończący" (wznowienie, zaliczenie, zakończenie kursu)

Dwa niezależne stany, dwa krótkie przebiegi. Sprawdza mechanizmy dodane 2026-09-06: pole `wznowienie` (schema 3), tabelę „Zaliczenie lekcji" ze skillu `lekcja` i zapis `aktualna_lekcja = ukończony` po 14.7.

### F1 — wznowienie w środku ćwiczenia ⭐ i zaliczenie

**Persona:** Ola, cel praca, ścieżka pełna, po 1.1-3.2, w 4.1 zrobiła 🔥 i utknęła w ⭐ (oceny). Odpowiada konkretnie, wkleja kod i wyniki.

**Stan startowy:** `student.json` z ukończonymi 1.1-3.2, `aktualna_lekcja` 4.1, `ukonczone_cwiczenia` z `4.1/warmup`, `wznowienie` = `{lekcja 4.1, krok 5, cwiczenie main, przeszkoda „dla wpisu abc program wypisuje Nie ma takiej oceny zamiast To nie jest ocena"}`; `kurs/zadania/08-if-a.cs` (działająca rozgrzewka) i `08-if-b.cs` (⭐ z błędem z przeszkody).

**Przebieg (uczennica):**
1. „Cześć, kontynuujemy" → oczekiwane: powitanie, **wznowienie od ⭐** (nie od zakotwiczenia 4.1), przypomnienie przeszkody słowami tutora, pytanie, czy ma odpowiedź.
2. „Już wiem — sprawdzałam tylko zakres, a nie wynik TryParse. Poprawiłam" + wkleja poprawiony `08-if-b.cs` i wyniki dla `6`, `9`, `abc`.
3. Na pytanie o jedną decyzję odpowiada (czemu `TryParse` sprawdzany osobno, przed łańcuchem `else if`).
4. Pyta o trudność → „3". Mówi, że kończy na dziś.

**Lista kontrolna F1:**
- [ ] `[T]` Punkt 1: agent zaczyna od ćwiczenia ⭐ i przeszkody; **nie** prowadzi kroków 1-4 lekcji 4.1 od nowa; nie wywołuje `wznowienie --wyczysc` (lekcja się zgadza).
- [ ] `[T]` `add-lekcja --id 4.1` dopiero **po** wyjaśnieniu decyzji przez uczennicę (punkt 3), nie po samym wklejeniu kodu.
- [ ] `[T]` Po sesji: `wznowienie` = `null`, `aktualna_lekcja` = `4.2`, `ukonczone_cwiczenia` ma `4.1/main`, `end-session` wykonane.
- [ ] Review kodu: pytanie o oczekiwany wynik / jedna rzecz dobra, bez wklejania poprawionego kodu.

### F2 — zakończenie kursu po 14.7 i wejście w moduł 15

**Persona:** Marek, cel praca, ścieżka pełna, wszystkie lekcje 1.1-14.6 ukończone, w 14.7 przerwał na kroku 4 (plan dalszej nauki).

**Stan startowy:** `student.json` z ukończonymi 1.1-14.6, `aktualna_lekcja` 14.7, `wznowienie` = `{lekcja 14.7, krok 4, przeszkoda „pisze plan dalszej nauki — wybór między ASP.NET Core a narzędziami CLI"}`, notatka `projekt: menedżer zadań CLI`.

**Przebieg (uczeń):**
1. „Cześć, kontynuujemy — plan mam gotowy" + wkleja trzy punkty planu (ASP.NET Core minimal API, EF Core z SQLite, jeden projekt open source).
2. Odpowiada na pytania o plan, zamyka kurs (krok 5: uruchamia projekt, patrzy na `01-hello.cs`), trudność → „2".
3. „A ten dodatek o async — chcę go zrobić w następnej sesji."
4. Kończy.

**Lista kontrolna F2:**
- [ ] `[T]` `add-lekcja --id 14.7`, potem `set --field aktualna_lekcja --value ukończony` **kończy się `OK`** (nie `BŁĄD: id lekcji ma postać M.L`) — regresja z 3c14693.
- [ ] `[T]` Punkt 3: `set --field aktualna_lekcja --value 15.1` wykonane (agent, sekcja o module 15) i `end-session`.
- [ ] `[T]` `wznowienie` = `null` po `add-lekcja 14.7`; brak `wznowienie --krok` po ukończeniu kursu.
- [ ] Gratulacje mówią o **50 lekcjach**, nie 49.

## Scenariusz G — „sprawdzający" (ścieżka skrócona w modułach 8-13, zadanie sprawdzające na wejściu)

**Persona:** Bartek, dwa lata Pythona (skrypty do pracy), cel narzędzia, ścieżka skrócona, po 1.1-7.4. Zna klasy z Pythona, nie zna C#-owych zaskoczeń (współdzielenie referencji, `==` na obiektach, `null` w tablicy obiektów).

**Stan startowy:** `student.json` z `sciezka: skrocona`, ukończone 1.1-7.4, `aktualna_lekcja` 8.1, notatka o Pythonie; `kurs/program.md` z adnotacją o zadaniu sprawdzającym w modułach 8-13.

**Przebieg (uczeń):**
1. „Cześć, kontynuujemy" → oczekiwane: powitanie, stan (8.1), **propozycja zadania sprawdzającego** (⭐ z 8.1: biblioteka `List<Ksiazka>`) z wyborem: spróbować teraz albo pełna lekcja.
2. Wybiera próbę; wkleja działający `21-klasy-b.cs` i wyniki (dwie książki z jednym złym rokiem; pusty Enter od razu).
3. Na pytanie o decyzję odpowiada (np. czemu `najstarsza` startuje od `biblioteka[0]`, a nie od zera).
4. Przechodzi **wszystkie** eksperymenty z kroku 3 (3.A-3.F), wklejając prawdziwe wyniki; przy 3.D zgaduje **źle** („Mruczek").
5. Czyta pułapki, pyta o jedną. Trudność → „2". Kończy.

**Lista kontrolna G:**
- [ ] `[T]` Punkt 1: agent zaczyna od ⭐ jako zadania sprawdzającego i daje wybór; **nie** prowadzi kroku 1 (zakotwiczenie) ani kroku 2 (mostek) przed próbą.
- [ ] `[T]` Punkty 2-3: udane ⭐ + wyjaśniona decyzja → agent **nie** wraca do kroków 1-2, ale **krok 3 w całości** (wszystkie sekcje A-F; w szczególności 3.D i 3.E) — nie pomija eksperymentów mimo udanego zadania.
- [ ] `[T]` Punkt 4: zła odpowiedź przy 3.D nie kończy ścieżki skróconej (to jest właśnie zaskoczenie, które ma wyjść) — jedno pytanie naprowadzające, potem fakt.
- [ ] Pułapki podane jako lista do przeczytania, nie jako krok 4 w całości.
- [ ] `[T]` `add-lekcja 8.1` po eksperymentach; `add-cwiczenie 8.1/main`; `aktualna_lekcja` = `8.2`; bez `set sciezka pelna`.
- [ ] Brak porównań do Pythona z inicjatywy agenta; gdy uczeń porówna — jedno zdanie.

