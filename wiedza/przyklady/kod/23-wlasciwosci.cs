// Właściwości — lekcja 8.3
//
// Uruchomienie:  dotnet run 23-wlasciwosci.cs

// Właściwość ze sprawdzaniem: konstruktor pilnuje tylko momentu tworzenia,
// właściwość pilnuje KAŻDEGO zapisu.
Kot k = new Kot();
k.Wiek = -500;
Console.WriteLine(k.Wiek);          // 0, nie -500

// Właściwość liczona — nie ma własnego pola, liczy się przy każdym odczycie,
// więc nie może rozjechać się z danymi.
Prostokat p = new Prostokat();
p.Szerokosc = 3;
p.Wysokosc = 4;
Console.WriteLine(p.Pole);          // 12
p.Szerokosc = 10;
Console.WriteLine(p.Pole);          // 40

// private set: odczytać można, ustawić z zewnątrz nie (CS0272).
Konto konto = new Konto("Ala");
konto.Wplac(100);
Console.WriteLine($"{konto.Wlasciciel}: {konto.Saldo}");
// konto.Saldo = 1000000;   // CS0272

class Kot
{
    private int wiek;                    // pole zaplecza: camelCase, private

    public int Wiek                      // właściwość: PascalCase, public
    {
        get { return wiek; }
        set
        {
            if (value < 0) wiek = 0;     // `value` to wartość podana przy przypisaniu
            else wiek = value;
        }
    }
}

class Prostokat
{
    // Właściwości automatyczne — skrót na pole zaplecza + prosty get/set.
    public double Szerokosc { get; set; }
    public double Wysokosc { get; set; }

    public double Pole => Szerokosc * Wysokosc;
}

class Konto
{
    public string Wlasciciel { get; set; }
    public decimal Saldo { get; private set; }

    public Konto(string wlasciciel) => Wlasciciel = wlasciciel;

    public void Wplac(decimal kwota)
    {
        if (kwota > 0) Saldo = Saldo + kwota;
    }
}
