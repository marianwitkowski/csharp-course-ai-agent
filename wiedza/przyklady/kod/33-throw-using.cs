// throw, własne wyjątki, using — lekcja 11.2
//
// Uruchomienie:  dotnet run 33-throw-using.cs

// Metoda, która WYPISUJE komunikat, decyduje za wszystkich.
// Metoda, która RZUCA wyjątek, zgłasza fakt i zostawia decyzję wywołującemu.
try { UstawWiek(200); }
catch (ArgumentOutOfRangeException e) { Console.WriteLine(e.Message); }

try { Console.WriteLine(Srednia(new int[0])); }
catch (ArgumentException e) { Console.WriteLine(e.Message); }

// Własny typ wyjątku — gdy ktoś ma go łapać OSOBNO od innych.
try { Wyplac(1000, 100); }
catch (SaldoException e) { Console.WriteLine($"Problem z saldem: {e.Message}"); }

// using: Dispose wywołuje się SAM — także gdy w środku poleci wyjątek.
try
{
    using (Zasob z = new Zasob("plik.txt"))
    {
        z.Uzyj();
        throw new Exception("coś poszło źle");
    }
}
catch (Exception)
{
    Console.WriteLine("złapane na zewnątrz — a zasób i tak został zamknięty");
}

// Krótszy zapis: Dispose na końcu bloku, w którym stoi deklaracja.
Pokaz();
void Pokaz()
{
    using Zasob z = new Zasob("krótki");
    z.Uzyj();
    Console.WriteLine("koniec metody");
}

// samo `throw;` przekazuje TEN SAM wyjątek dalej, nie gubiąc śladu pochodzenia.
try { Posrednik(); }
catch (FormatException) { Console.WriteLine("złapane wyżej"); }

void Posrednik()
{
    try { int.Parse("x"); }
    catch (FormatException) { Console.WriteLine("zapisuję do dziennika..."); throw; }
}

void UstawWiek(int wiek)
{
    if (wiek < 0 || wiek > 120)
    {
        // nameof wstawia nazwę parametru — po zmianie nazwy komunikat nie skłamie
        throw new ArgumentOutOfRangeException(nameof(wiek), $"Wiek musi być z zakresu 0-120, dostałem {wiek}");
    }
}

double Srednia(int[] oceny)
{
    if (oceny.Length == 0) throw new ArgumentException("Nie da się policzyć średniej z pustej listy.");
    int suma = 0;
    foreach (int o in oceny) suma = suma + o;
    return (double)suma / oceny.Length;
}

void Wyplac(decimal kwota, decimal saldo)
{
    if (kwota > saldo) throw new SaldoException($"Brak środków: masz {saldo}, chcesz {kwota}");
}

class SaldoException : Exception
{
    public SaldoException(string komunikat) : base(komunikat) { }
}

// using działa TYLKO na klasach z IDisposable (inaczej CS1674).
class Zasob : IDisposable
{
    private string nazwa;
    public Zasob(string nazwa) { this.nazwa = nazwa; Console.WriteLine($"otwarto {nazwa}"); }
    public void Uzyj() => Console.WriteLine($"używam {nazwa}");
    public void Dispose() => Console.WriteLine($"zamknięto {nazwa}");
}
