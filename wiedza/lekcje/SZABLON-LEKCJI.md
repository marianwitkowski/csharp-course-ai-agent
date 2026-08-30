---
lekcja: X.Y
tytul: Krótki, konkretny tytuł
modul: NN-nazwa-modulu
przyklady: NN-plik.cs (jeśli w wiedza/przyklady/kod/ jest coś na temat)
aktualizacja: [moduł NN], [ogólne]
czas_min: 30-60
zalozenia: co uczeń musi już umieć (lekcje poprzedzające)
---

<!-- KONWENCJA KOMEND:
     Komendy pisane w wersji macOS/Linux. Różnice na Windows dotyczą wyłącznie
     powłoki (`type` zamiast `cat`, `dir` zamiast `ls`, ścieżki z `\`).
     Sama komenda `dotnet` jest identyczna na wszystkich systemach.
     Uczeń pracuje w `kurs/zadania/` — każde ćwiczenie to JEDEN plik `.cs`.
     Uruchamianie: `dotnet run NN-temat.cs` z katalogu `kurs/zadania`.
     AGENT NIGDY NIE URUCHAMIA KODU UCZNIA — uruchamia uczeń i wkleja wynik. -->

# Lekcja X.Y — Tytuł

## Cel
Po lekcji uczeń:
- konkret 1
- konkret 2
- konkret 3

## Krok 1 — Zakotwiczenie (3-5 min)
**Pytanie wejściowe:**
> „Coś z życia, co aktywuje intuicję — bez terminologii technicznej."

**Co chcesz usłyszeć:** krótkie naprowadzenie, co odpowiedź ma uchwycić.
**Czego NIE rób:** nie wprowadzaj jeszcze terminu technicznego.

## Krok 2 — Mostek (3-5 min)
**Mostek konceptualny:** „To, co opisałeś — [intuicja] — w C# nazywamy [termin]."

**Minimalny przykład (2-3 linijki — w C# z instrukcjami najwyższego poziomu
plik naprawdę może mieć trzy linijki, korzystaj z tego):**
```csharp
// najprostsze, co pokazuje koncept
```

**Pytania:**
- „Co tu jest [X]? Co [Y]?"
- „Co robi linia 2?"

## Krok 3 — Eksperyment (10-15 min)
Mini-zadania (uczeń sam pisze i uruchamia — `dotnet run NN-temat.cs`):
1. zadanie 1
2. zadanie 2
3. zadanie 3

**Po każdym pytaj:** „Co zobaczyłeś? Czy się spodziewałeś?"
**Gdy kompilator odmówi:** „Wklej dokładnie, co powiedział — razem z kodem `CSxxxx`" → czytacie komunikat razem.

## Krok 4 — Pogłębienie (10-15 min)
Wariacje i „co jeśli":
- „Co się stanie, gdy...?"
- „A jeśli zamiast X dasz Y?"

**Typowe błędy do sprowokowania:**
- konkretny kod `CSxxxx`, np. `CS0029` (niezgodność typów)
- wynik zaskakujący, ale bez błędu — np. dzielenie całkowite dające `0`

> Najcenniejszy moment lekcji to ten, w którym program **działa**, ale wypisuje
> coś innego, niż uczeń zakładał. Zaplanuj przynajmniej jeden taki moment.

## Krok 5 — Ćwiczenie
→ Wywołaj skill `cwiczenie`. Sugerowany kontekst:
- temat: [konkretny temat]
- elementy do użycia: [...]

Propozycje (agent może wziąć wprost):
- 🔥 [rozgrzewka]
- ⭐ [główne]
- ⚡ [gwiazdka]

## Pułapki
- Pułapka 1 — opis krótko + jak ją rozpoznać
- Pułapka 2

## Aktualizacja 2026 (z `AKTUALIZACJE.md`)
Co uczeń może zobaczyć w starym poradniku i co robimy zamiast tego:
- punkt 1 (z sekcji `[moduł NN]`)
- punkt 2

**Kolejność:** najpierw dzisiejszy sposób. Stara forma tylko jako „spotkasz to
w cudzym kodzie, to znaczy tyle a tyle" — jedno zdanie, bez rozwijania.

## Notatki tutora
- Jeśli uczeń mówi X → reaguj Y
- Częsta dygresja, której unikać
- Co powiedzieć, gdy uczeń wydaje się znudzony / przytłoczony

## Po lekcji
- Notatka w `kurs/lekcje/X.Y-temat.md` (3-5 zdań o tym, co zostało zrozumiane + 1 pułapka)
- `postep`: dopisz lekcję `X.Y` do `ukonczone_lekcje` z subiektywną trudnością (1-5)
- `aktualna_lekcja` ustaw na kolejną
- Jeśli uczeń się potknął → wpis do `do_powtorki`
