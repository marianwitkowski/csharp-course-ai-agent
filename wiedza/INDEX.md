# Indeks bazy wiedzy — mapa kursu C# / .NET

> **Po co ten plik?** To **źródło prawdy** dla struktury kursu: ile jest modułów, ile lekcji, w jakiej kolejności i na czym każda się opiera. Skille `program-kursu` i `lekcja` czytają go w pierwszej kolejności. Jeśli inny plik podaje inną liczbę lekcji — to błąd dokumentacji, nie zmiana programu.

> **Status gotowych lekcji sokratejskich:** 49/49 w `wiedza/lekcje/` — **kurs kompletny**.

> **Uwaga o środowisku:** kurs jest **konsolowy i wieloplatformowy**. Cały kod działa tak samo na macOS, Linuksie i Windows, uruchamiany przez `dotnet`. Nie ma tu Windows Forms, WPF, WinUI ani Web Forms — dlaczego, wyjaśnia sekcja „Czego w kursie nie ma".

> **Uwaga o komendach:** przykłady w lekcjach są w wersji macOS/Linux (ścieżki z `/`). Na Windows separator to `\`, ale sama komenda `dotnet` jest identyczna na każdym systemie. Agent tłumaczy ścieżki w trakcie sesji.

---

## Katalogi bazy

| Katalog | Zawartość | Rola |
| --- | --- | --- |
| `wiedza/lekcje/` | 49 lekcji sokratejskich + `SZABLON-LEKCJI.md` | **scenariusze prowadzenia** — to czytasz w pierwszej kolejności |
| `wiedza/przyklady/kod/` | minimalne, działające programy `.cs` | materiał do eksperymentów i inspiracja na ćwiczenia |
| `wiedza/przyklady/zepsute/` | 10 programów z **jednym** błędem każdy, z objawem w nagłówku | ćwiczenia 🔧 naprawa — patrz sekcja „Zepsute programy" |
| `wiedza/AKTUALIZACJE.md` | delta „.NET Framework (2020) → .NET 10 (2026)" | prostuje to, co uczeń znajdzie w starszych poradnikach |

W tym kursie **nie ma katalogu `zrodlo/`**. Materiały źródłowe autora leżą poza
repozytorium (`.kb/`, wyłączone z kontroli wersji — patrz `NOTICE.md`), a cała
treść dydaktyczna została napisana od nowa wprost w `wiedza/lekcje/`.

---

## Program — 14 modułów, 49 lekcji

### Moduł 1 — Wprowadzenie i środowisko (2)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 1.1 | Czym jest C# i .NET — i pierwszy program (`dotnet run`, `Console.WriteLine`) | `01-hello.cs` | `[ogólne]`, `[moduł 1]` |
| 1.2 | Edytor, terminal, `.editorconfig` — workflow ucznia | — | `[moduł 1]` |

> Lekcja 1.1 jest **najdłuższa w kursie** (60-75 min) i jako jedyna bywa dzielona na dwie sesje — naturalna przerwa jest po kroku 2, gdy uczeń ma już działający program. Powstała ze sklejenia dawnych 1.1 i 1.2: pierwsza lekcja bez ani jednej linii kodu zniechęcała, a lekcja o samym `Console.WriteLine` była za cienka na osobne posiedzenie.

### Moduł 2 — Zmienne i typy (4)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 2.1 | Zmienne i typy proste (`int`, `double`, `string`, `bool`, `var`) | `02-zmienne.cs` | `[moduł 2]` |
| 2.2 | Stałe i typy wyliczeniowe (`const`, `enum`) | `03-const-enum.cs` | — |
| 2.3 | Konwersje typów — jawne, `Parse`, `TryParse` | `04-konwersje.cs` | `[moduł 2]` |
| 2.4 | Operatory i wyrażenia | `05-operatory.cs` | — |

### Moduł 3 — Rozmowa z użytkownikiem (2)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 3.1 | Wypisywanie — `WriteLine`, interpolacja `$"..."`, formatowanie | `06-interpolacja.cs` | `[moduł 3]` |
| 3.2 | Wejście — `Console.ReadLine`, walidacja przez `TryParse` | `07-wejscie.cs` | `[moduł 3]` |

> **Moduł 3 obywa się bez `if`.** `TryParse` zwraca `bool`, a `bool` się wypisuje — uczeń potrafi więc **wykryć** złe dane, zanim pozna instrukcję warunkową. Brak reakcji na wykryty błąd jest celowy: to motywacja wejściowa do modułu 4 i lekcja 3.2 kończy się właśnie tym.

### Moduł 4 — Decyzje (3)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 4.1 | `if` / `else if` / `else` | `08-if.cs` | — |

> **Dług z modułu 1:** lekcja 4.1 wprowadza pierwsze klamry, więc to **tutaj** pokazuje się, po co są wcięcia — dwie wersje tego samego `if`, zbita i sformatowana. Lekcja 1.2 celowo tego nie robi: w płaskim ciągu instrukcji nie ma czego wcinać, a przykład z `if` wyprzedzałby program.
| 4.2 | Operatory logiczne i operator warunkowy `?:` | `09-logiczne.cs` | — |
| 4.3 | `switch` — instrukcja i wyrażenie | `10-switch.cs` | `[moduł 4]` — `switch` jako wyrażenie |

### Moduł 5 — Pętle (3)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 5.1 | `while` i `do...while` | `11-while.cs` | — |
| 5.2 | `for` | `12-for.cs` | — |
| 5.3 | `foreach`, `break`, `continue` | `13-foreach.cs` | — |

> **`foreach` przed kolekcjami — świadoma decyzja.** W lekcji 5.3 uczeń nie zna jeszcze tablic ani list, więc `foreach` chodzi po **znakach tekstu** (`foreach (char znak in "Anna")`). To jedyna sekwencja, którą zna, a `char` był wzmiankowany w 2.1. Dzięki temu w module 6 `foreach` jest już opanowany i kolekcje można wprowadzić bez uczenia dwóch rzeczy naraz.

### Moduł 6 — Kolekcje (4)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 6.1 | Tablice — stały rozmiar, indeksowanie od zera | `14-tablice.cs` | `[moduł 6]` — indeksy `^1` i zakresy `..` |
| 6.2 | `List<T>` — kolekcja, która rośnie | `15-list.cs` | `[moduł 6]` |
| 6.3 | `Dictionary<TKey,TValue>` — klucz → wartość | `16-dictionary.cs` | `[moduł 6]` — `TryGetValue` |
| 6.4 | Tablice wielowymiarowe i tablice tablic | `17-wielowymiarowe.cs` | — |

### Moduł 7 — Metody (3)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 7.1 | Metody — parametry, wartość zwracana, `void`; kontrakt metody sprawdzany przez kod (`Sprawdz`) | `18-metody.cs` | — |
| 7.2 | Parametry domyślne i nazwane; przeciążanie do rozpoznania | `19-parametry.cs` | — |
| 7.3 | `ref`, `out`, zasięg zmiennych | `20-ref-out.cs` | — |

> **Testowanie zaczyna się w 7.1, nie w 14.4.** Krok 3.G lekcji 7.1 wprowadza `Sprawdz(opis, wynik, oczekiwane)` — sprawdzenie kontraktu metody napisane z `if` i interpolacji, bez żadnego narzędzia. Uczeń psuje `Dodaj` i widzi, że jedno z trzech sprawdzeń błędu nie łapie — to uczy doboru przypadków wcześniej niż xUnit. Od 7.1 ćwiczenia ⭐ i ⚡ mogą wymagać wywołań `Sprawdz`; xUnit w 14.4 jest wtedy „gotowym `Sprawdz`", nie nową filozofią.

> **Przeciążania uczeń w module 7 nie napisze — i to jest celowe.** Metody pisane w pliku bez klasy to funkcje lokalne, a tych nie da się przeciążać (`CS0128`). Lekcja 7.2 pokazuje więc przeciążanie do **rozpoznania**: `Console.WriteLine` przyjmuje `int`, `string`, `bool`, `double` i `char`, czyli uczeń używa przeciążeń od lekcji 1.1, nie wiedząc o tym. Własne napisze w module 8 — i to jest pierwszy konkretny powód, dla którego klasy istnieją. Parametry domyślne i nazwane działają w funkcjach lokalnych bez przeszkód i te uczeń pisze sam.

### Moduł 8 — Klasy i obiekty (5)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 8.1 | Klasa jako własny typ — pola i obiekty | `21-klasy.cs` | `[moduł 8]` |
| 8.2 | Konstruktory | `22-konstruktory.cs` | `[moduł 8]` — konstruktory podstawowe |
| 8.3 | Właściwości — `get`/`set`, właściwości automatyczne | `23-wlasciwosci.cs` | `[moduł 8]` |
| 8.4 | Modyfikatory dostępu i enkapsulacja | `24-enkapsulacja.cs` | — |
| 8.5 | `null` i typy nullowalne — `string?`, `is null`, `??`, `?.`, ostrzeżenia `CS86xx` | — | `[moduł 8]` — sprawdzanie `null` przez kompilator |

> **Lekcja 8.5 spłaca dług z lekcji 3.2.** Uczeń widzi `CS8600` od modułu 3 i słyszy „ostrzeżenie to nie błąd, wrócimy do tego". Wyjaśnienie wymaga `null` (8.1), `if` (4.1) i właściwości z `CS8618` (8.2-8.3) — dlatego dopiero tutaj, w pół godziny, a nie w module 3 w dwie. Lekcja jest krótka i celowo **zamyka** moduł 8: zanim uczeń napisze `Kot` i `Pies` (9.1), umie powiedzieć, których wartości w jego klasach może brakować. `??` wraca w 12.3 (`Deserialize`) i w projekcie (14.3), `?` w typie zwracanym — w 13.1 (`FirstOrDefault`).

> **Moduł 8 spłaca dwa długi z modułu 7.** Lekcja 8.2 pokazuje przeciążone konstruktory — czyli dokładnie to, czego uczeń nie mógł napisać w 7.2 (`CS0128`), bo funkcji lokalnych nie da się przeciążać. Lekcja 8.1 odpowiada też na pytanie z końca 7.3 („a jak oddać z metody trzy rzeczy") — własnym typem zamiast trzech `out`.

### Moduł 9 — Programowanie obiektowe (4)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 9.1 | Dziedziczenie — `:` i `base` | `25-dziedziczenie.cs` | — |
| 9.2 | `virtual` / `override` — polimorfizm | `26-polimorfizm.cs` | — |
| 9.3 | Klasy i metody abstrakcyjne | `27-abstrakcyjne.cs` | — |
| 9.4 | Składowe statyczne i klasy statyczne | `28-static.cs` | `[moduł 9]` |

> **Moduł 9 domyka trzy wątki ciągnące się przez cały kurs.** Lekcja 9.2 wyjaśnia `override`, które uczeń pisał w 8.2 przy `ToString()`, nie wiedząc, co znaczy (odpowiedź: `object.ToString` jest `virtual`). Lekcja 9.4 wyjaśnia, czemu `Console.WriteLine` i `Math.Round` woła się bez `new` — używa ich od lekcji 1.1. Lekcja 9.4 domyka też `static void Main` ze starych materiałów, wzmiankowane w 7.1.

### Moduł 10 — Interfejsy (3)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 10.1 | Interfejs jako kontrakt | `29-interfejsy.cs` | — |
| 10.2 | `ToString()`, `IComparable`, `IEnumerable` | `30-icomparable.cs` | — |
| 10.3 | Kompozycja zamiast dziedziczenia — kiedy co | `31-kompozycja.cs` | — |

> **`IEnumerable` uczeń rozpoznaje, nie implementuje.** Własna implementacja wymaga jawnej wersji nieogólnej i `IEnumerator` — trzech nowych rzeczy naraz, bez zysku dydaktycznego. Lekcja 10.2 wyjaśnia natomiast, **dlaczego** `foreach` działa jednakowo na tekście, tablicy, liście i słowniku: wszystkie implementują ten sam kontrakt. `IComparable<T>` uczeń pisze sam — to jedna metoda.

### Moduł 11 — Wyjątki (2)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 11.1 | `try` / `catch` / `finally` | `32-try-catch.cs` | — |
| 11.2 | `throw`, własne wyjątki, `using` i `IDisposable` | `33-throw-using.cs` | `[moduł 11]` |

> **Moduł 11 jest krótki celowo.** Uczeń poznał już trzy inne sposoby radzenia sobie z błędami: metody `TryCoś` (2.3, 6.3), wartości specjalne jak `-1` z `IndexOf` (6.2) i walidację w konstruktorze (8.2). Lekcja 11.2 zbiera to w jedną tabelę „co kiedy" — wyjątki są **jednym z czterech** narzędzi, nie domyślnym.

### Moduł 12 — Pliki i dane (4)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 12.1 | Pliki tekstowe — `File`, `StreamReader` / `StreamWriter` | `34-pliki.cs` | `[moduł 12]` |
| 12.2 | Ścieżki i katalogi — `Path`, `Directory` | `35-sciezki.cs` | — |
| 12.3 | JSON — `System.Text.Json` | `36-json.cs` | `[moduł 12]` — `System.Text.Json` zamiast Newtonsoft |
| 12.4 | Argumenty wiersza poleceń (`args`) | `37-argumenty.cs` | — |

> **JSON w pliku jednoplikowym wymaga dwóch dyrektyw.** Program uruchamiany przez `dotnet run plik.cs` ma domyślnie **wyłączoną** serializację opartą na refleksji — bez `#:property JsonSerializerIsReflectionEnabledByDefault=true` każdy przykład z lekcji 12.3 kończy się `InvalidOperationException`. Druga linia, `#:property PublishAot=false`, wycisza kilkanaście ostrzeżeń `IL2026`/`IL3050`, które inaczej zasypują wynik przy pierwszym uruchomieniu. Obie znikają w module 14, gdzie uczeń przechodzi na zwykły projekt z `.csproj`. To jedyne miejsce w kursie, gdzie dyrektywa `#:property` jest obowiązkowa.

> **`System.Text.Json` serializuje właściwości, nie pola.** Klasa z publicznym polem daje `{}` — bez błędu i bez ostrzeżenia. To najkonkretniejszy argument za właściwościami w całym kursie i domyka lekcję 8.3, która podawała cztery powody teoretyczne.

### Moduł 13 — LINQ (3)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 13.1 | LINQ — `Where`, `Select`, `OrderBy` | `38-linq.cs` | — |
| 13.2 | Agregacje i grupowanie — `Count`, `Sum`, `Max`, `GroupBy` | `39-agregacje.cs` | — |
| 13.3 | Wyrażenia lambda i `Func<>` — co siedzi pod LINQ | `40-lambdy.cs` | — |

> **Lambda dostaje wyjaśnienie dopiero w 13.3, po dwóch lekcjach używania.** Odwrotna kolejność — najpierw `Func<>` i delegaty, potem LINQ — wymaga od ucznia przyjęcia na wiarę, po co komu funkcja w zmiennej. Lekcje 13.1-13.2 budują tę potrzebę, a 13.3 pokazuje, że `Where` da się napisać samemu w ośmiu liniach. Wtedy `Func<>` jest odpowiedzią na pytanie, które uczeń już ma.

> **Typów anonimowych (`new { u.Imie, u.Punkty }`) kurs nie wprowadza.** W `Select` są kuszące, ale to nowy rodzaj typu tuż przed końcem kursu. Uczeń, który potrzebuje dwóch wartości naraz, wypisuje je w `foreach` albo mapuje na własną klasę.

### Moduł 14 — Projekt i dalsze kroki (7)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 14.1 | Wybór projektu i rozpisanie na kroki — `dotnet new console`, `.csproj` | — | `[moduł 14]` |
| 14.2 | Git — historia projektu: `init`, `status`, `add`, `commit`, `diff`, `restore`, `.gitignore` | — | `[moduł 14]` |
| 14.3 | Implementacja — od szkieletu do działania | — | — |
| 14.4 | Testy xUnit, README, `dotnet publish` | — | `[moduł 14]` |
| 14.5 | Git — gałęzie, scalanie, konflikt; zdalne repozytorium i pull request | — | `[moduł 14]` |
| 14.6 | AI w pracy programisty — jak korzystać i jak weryfikować | — | `[moduł 14]` |
| 14.7 | Mapa ekosystemu — co dalej (ASP.NET Core, EF Core, Blazor, WPF) | — | `[moduł 14]` |

> **Git wchodzi w 14.2, nie w module 1 — i to jest decyzja.** Przez trzynaście modułów uczeń ma po jednym pliku na ćwiczenie; historia zmian jednoplikowego programu na trzydzieści linii niczego nie uczy, a `git` byłby trzecim narzędziem do opanowania obok edytora i terminala w tej samej lekcji. W 14.1 powstaje projekt z kilku plików, który będzie rósł przez kilka sesji — dopiero wtedy jest co wersjonować. Repozytorium zakłada się w **`kurs/projekt/`**, nie w katalogu aplikacji, bo w 14.4 obok niej stanie projekt testowy i oba mają być w jednej historii.

> **Gałęzie dopiero po testach (14.5).** Scalanie gałęzi bez testów to zgadywanie, czy program nadal działa; z testami — `dotnet test` przed `git merge` jest regułą, którą uczeń może zastosować od razu do etapu 2 własnego projektu. Pull request pojawia się w 14.5 jako pojęcie i komendy `remote`/`push`, bez wymagania konta — kurs nie może zakładać, że uczeń chce publikować kod.

> **Moduł 14 to jedyne miejsce w kursie z projektem i `.csproj`.** Przez trzynaście modułów uczeń pracuje na plikach pojedynczych (`dotnet run plik.cs`), bo projekt wymagałby tłumaczenia `.csproj`, katalogów `bin`/`obj` i budowania, zanim uczeń napisze cokolwiek. Lekcja 14.1 wprowadza projekt wtedy, gdy jest do czego: wiele plików, testy, wydanie programu.

> **Kanoniczną drogą jest `dotnet new console`, nie `dotnet project convert`.** Konwersja przenosi ustawienia pliku pojedynczego, w tym `PublishAot=true`, przy którym JSON z lekcji 12.3 rzuca ten sam wyjątek, co bez dyrektywy. Świeży `dotnet new console` tego ustawienia nie ma, więc obietnica z 12.3 („w projekcie żadna dyrektywa nie będzie potrzebna") jest prawdziwa. Konwersja pojawia się w 14.1 jako uwaga na marginesie, wraz z instrukcją usunięcia tej linii.

> **Zasada „cienki `Program.cs`" jest wprowadzona w 14.1, a nie w 14.4.** Logika w klasach `public`, `Console.WriteLine` tylko w `Program.cs` — bez tego testy w 14.4 są niewykonalne i uczeń musi przepisywać działający kod. Atrybuty `[Fact]`/`[Theory]` to jedyne atrybuty w całym kursie; 14.4 nazywa je jednym zdaniem i nie rozwija.

> **Lekcja 14.6 (AI) nie ma odpowiednika w materiałach źródłowych.** Jej sens jest praktyczny — patrz niżej.

### Moduł 15 — Dodatek po kursie: asynchroniczność (2, opcjonalny)

| Lekcja | Temat | Przykłady | Aktualizacja |
| --- | --- | --- | --- |
| 15.1 | `Task`, `async` i `await` — czekanie bez stania w miejscu; `CS4014`, `CS4032` | — | `[moduł 15]` |
| 15.2 | Kilka operacji naraz — `Task.WhenAll`, kolejność wyników, wyjątek z jednego zadania | — | `[moduł 15]` |

> **Moduł 15 nie jest częścią kursu fundamentów — jest dodatkiem po nim.** Lekcja 14.7 kończy kurs (`aktualna_lekcja` → `ukończony`); moduł 15 uruchamia się na życzenie ucznia („chcę async", „co dalej po kursie"), agent ustawia wtedy `aktualna_lekcja` na `15.1`. Powód: asynchroniczność jest pierwszą rzeczą, o którą uczeń potknie się w ASP.NET Core, i recenzja zewnętrzna wskazała ją jako największą lukę merytoryczną — ale wprowadzona w rdzeniu kursu (przed projektem) dawałaby kod, który „działa dziwnie" bez widocznej przyczyny. Po projekcie uczeń ma `Magazyn`, testy i `Stopwatch`, więc może **zmierzyć** różnicę, zamiast w nią wierzyć.

> **Bez sieci — nadal.** `Task.Delay` udaje wolną operację; pliki na dysku są za szybkie, żeby zobaczyć różnicę między „po kolei" a „naraz". `HttpClient` zostaje w mapie 14.7. Jej sens jest praktyczny: asystenci masowo podpowiadają `class Program`, `static void Main` i `Newtonsoft.Json`, bo takich przykładów widzieli najwięcej. Wiedza z `AKTUALIZACJE.md` jest dokładnie tym, co pozwala uczniowi odsiać przestarzałą odpowiedź — i to czyni lekcję ćwiczeniem z całego kursu, nie dygresją.

---

## Podsumowanie liczbowe

| Moduł | Lekcje |
| --- | --- |
| 1. Wprowadzenie i środowisko | 2 |
| 2. Zmienne i typy | 4 |
| 3. Rozmowa z użytkownikiem | 2 |
| 4. Decyzje | 3 |
| 5. Pętle | 3 |
| 6. Kolekcje | 4 |
| 7. Metody | 3 |
| 8. Klasy i obiekty | 5 |
| 9. Programowanie obiektowe | 4 |
| 10. Interfejsy | 3 |
| 11. Wyjątki | 2 |
| 12. Pliki i dane | 4 |
| 13. LINQ | 3 |
| 14. Projekt i dalsze kroki | 7 |
| **Razem — kurs** | **49** |
| 15. Dodatek po kursie: asynchroniczność (opcjonalny) | 2 |
| **Razem — pliki lekcji** | **51** |

**Ten plik jest źródłem prawdy dla liczby 49** (lekcje kursu) i **51** (pliki w `wiedza/lekcje/`, z dodatkiem). Jeśli inny plik podaje inną liczbę — to błąd dokumentacji.

---

## Zepsute programy — ćwiczenia 🔧 naprawa

Realna praca częściej wygląda jak *przeczytaj → zrozum → znajdź → popraw* niż *napisz od zera*. Od modułu 4 dziesięć lekcji ma czwarty poziom ćwiczenia: uczeń dostaje program z **jednym** błędem, **objawem** w nagłówku i bez przyczyny. Pliki pisze i weryfikuje autor kursu (objaw i naprawa uruchomione na .NET 10); agent ich nie tworzy — to jedyny sposób, żeby dać uczniowi cudzy zepsuty kod bez łamania zasady „agent nie pisze `.cs` za ucznia".

| Lekcja | Plik | Rodzaj błędu | Objaw |
| --- | --- | --- | --- |
| 4.1 | `08-if-zepsute.cs` | liczy źle | `18` → „niepełnoletni" (`>` zamiast `>=`) |
| 5.1 | `11-while-zepsute.cs` | liczy źle | 11 liczb od `0` zamiast 10 od `1` |
| 6.1 | `14-tablice-zepsute.cs` | wywraca się | `IndexOutOfRangeException` po ostatnim elemencie (`<=` `Length`) |
| 7.1 | `18-metody-zepsute.cs` | liczy źle | `Sprawdz` mówi `BŁĄD`: `3` zamiast `3,5` (dzielenie całkowite w `double`) |
| 8.2 | `22-konstruktory-zepsute.cs` | liczy źle | kot ma `0 lat`; dwa ostrzeżenia `CS1717`, `CS0649` (`Wiek = Wiek` bez `this`) |
| 9.2 | `26-polimorfizm-zepsute.cs` | liczy źle | pies „wydaje dźwięk"; `CS0114` (brak `override`) |
| 10.1 | `29-interfejsy-zepsute.cs` | nie kompiluje się | `CS0535` (`opis` zamiast `Opis`) |
| 11.1 | `32-try-catch-zepsute.cs` | liczy źle | „Podwojone: 0" po `abc`, bez komunikatu (pusty `catch`) |
| 12.1 | `34-pliki-zepsute.cs` | liczy źle | w pliku tylko ostatnia linia (`WriteAllText` w pętli) |
| 13.1 | `38-linq-zepsute.cs` | liczy źle | `OrderBy` „nie sortuje" (wynik nieprzypisany) |

Każdy plik jest w materiale **do tej lekcji włącznie** — bez konstrukcji z przodu. Dobór: jeden błąd na plik, z klasy, której uczy lekcja; proporcja trzech rodzajów (kompilacja / logika / wyjątek) celowo przechylona ku „kompiluje się i liczy źle", bo to te błędy uczeń będzie znajdował najdłużej. Skrypt `narzedzia/sprawdz-przyklady.sh` buduje je i pilnuje, że `29` nadal **nie** kompiluje się, a reszta tak.

Reszta lekcji (moduły 2-3 i pozostałe lekcje modułów 4-13) nie ma jeszcze plików 🔧 — to pierwsza partia; format sprawdzony na dziesięciu.

## Zależności między modułami — czego nie wolno przestawić

Kolejność nie jest przypadkowa. Trzy miejsca są sztywne:

- **8 przed 9 przed 10.** Nie da się uczyć dziedziczenia bez klasy, ani interfejsu bez metody wirtualnej. To najdłuższy łańcuch w kursie.
- **7 przed 8.** Metoda w klasie to ta sama metoda co samodzielna, tylko z `this`. Uczeń, który nie rozumie parametrów i wartości zwracanej, w module 8 utknie na czymś innym, niż mu się wydaje.
- **11 przed 12.** Każda operacja na pliku może się nie udać. Bez wyjątków lekcje 12.1–12.4 uczyłyby ignorowania błędów.
- **14.1 → 14.2 → 14.3 → 14.4 → 14.5.** Git wymaga istniejącego projektu (14.1), implementacja commituje po każdym kroku (14.2), gałęzie scala się z testami w ręku (14.4).

Reszta ma pewien luz: moduł 13 (LINQ) można przesunąć za 12 albo przed 12,
zależnie od tego, na czym uczniowi zależy.

---

## Czego w kursie nie ma (świadome decyzje)

Program XL („C# (.NET) Developer XL") wymienia znacznie więcej niż te 49 lekcji.
Poniższe tematy są **świadomie** poza kursem dla początkujących — nie jako
przeoczenie, tylko dlatego, że każdy z nich wymaga fundamentu, który ten kurs
dopiero buduje.

| Temat | Dlaczego pominięty | Gdzie wspomniany |
| --- | --- | --- |
| Windows Forms, WPF, WinUI | Działają **tylko na Windows** — kurs musi działać też na macOS i Linuksie. Poza tym GUI odciąga uwagę od języka: uczeń debuguje układ kontrolek zamiast logiki | 14.7 |
| ASP.NET Web Forms, GridView, ObjectDataSource | Technologia wycofana — **nie istnieje** w .NET Core ani w .NET 5+. Uczenie jej w 2026 to uczenie ślepej uliczki | 14.7 — jednym zdaniem, jako kontekst historyczny |
| ASP.NET Core, MVC, Razor, Blazor | Wymagają rozumienia HTTP, cyklu żądanie-odpowiedź i asynchroniczności. Osobny kurs | 14.7 |
| Bazy danych, SQL, Entity Framework Core | Wymagają SQL, a SQL to osobny język. Kurs uczy C#, nie dwóch języków naraz | 14.7 |
| Wzorce: MVC, MVVM, Onion, DDD, DI/IoC | Poziom architektoniczny. Wzorzec rozwiązuje problem, którego początkujący jeszcze nie ma — bez tego problemu wzorzec to pusty rytuał | 14.7 |
| Typy generyczne własne (`class Pudelko<T>`) | Uczeń **używa** `List<T>` i `Dictionary<K,V>` od modułu 6, ale własnych generyków nie pisze — potrzeba pojawia się dopiero przy bibliotekach | 14.7 |
| `async` / `await` **w rdzeniu kursu** | Wprowadzone przed projektem daje kod, który „działa dziwnie" bez widocznej przyczyny. Jest **dodatkiem po kursie**: moduł 15 (2 lekcje, opcjonalny), na plikach i `Task.Delay`, bez sieci | 14.7 → 15.1 |
| wielowątkowość (`Thread`, `Parallel`, `lock`), `Task.Run`, `CancellationToken` | Moduł 15 uczy czekania bez blokowania, nie równoległych obliczeń. To osobny temat z własnymi pułapkami | 15.1 — jedno zdanie |
| Metody rozszerzające | Wymagają klas statycznych (9.4) i pewności w czytaniu sygnatur. Uczeń **korzysta** z nich w module 13 (LINQ to metody rozszerzające), ale swoich nie pisze | 13.1 — jedno zdanie |
| `record`, `struct` | Każdy z nich to wariant czegoś, co uczeń dopiero co poznał. Wprowadzone równolegle z klasą rozmywają obraz | 14.7 |
| `!` (null-forgiving), `required`, `??=`, `Nullable<T>` z `HasValue` | Podstawy `null` i `string?` są w 8.5; te cztery to warianty dla zaawansowanych, a `!` uczy uciszać kompilator zamiast go słuchać | 8.5 — jedno zdanie w Pułapkach |
| Refleksja, `unsafe`, wskaźniki | Nigdy dla początkującego | — |
| Visual Studio (pełne IDE) | Tylko Windows, ciężkie, ukrywa `dotnet` za przyciskami. Kurs uczy narzędzi wiersza poleceń, bo one działają wszędzie i pokazują, co się naprawdę dzieje | 1.2 — wzmianka jako alternatywa dla użytkowników Windows |

**Uczeń pytający o którykolwiek z tych tematów** dostaje jedno zdanie, co to
jest, i odesłanie do lekcji **14.7** („mapa ekosystemu"). Nie rozwijaj — to
najkrótsza droga do rozjechania programu.
