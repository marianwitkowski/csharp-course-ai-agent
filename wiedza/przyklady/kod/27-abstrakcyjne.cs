// Klasy abstrakcyjne — lekcja 9.3
//
// Uruchomienie:  dotnet run 27-abstrakcyjne.cs

// Zmienna może być typu abstrakcyjnego. Obiekt — nie.
// Figura f = new Figura();   // CS0144
Figura[] figury = { new Kolo(2), new Kwadrat(3) };

foreach (Figura f in figury)
{
    Console.WriteLine($"{f.Nazwa}: pole {f.Pole():F2}");
    f.Opisz();
}

// Wzorzec szablonu: baza ustala kolejność kroków, pochodna wypełnia jeden.
new RaportDzienny().Generuj();

abstract class Figura
{
    public string Etykieta { get; set; } = "bez nazwy";

    // abstract = brak ciała, pochodna MUSI napisać (inaczej CS0534).
    public abstract double Pole();
    public abstract string Nazwa { get; }

    // Klasa abstrakcyjna to normalna klasa z dziurami — może mieć zwykłe składowe.
    public void Opisz() => Console.WriteLine($"  ({Etykieta})");
}

class Kolo : Figura
{
    private double r;
    public Kolo(double r) => this.r = r;
    public override double Pole() => Math.PI * r * r;
    public override string Nazwa => "Koło";
}

class Kwadrat : Figura
{
    private double a;
    public Kwadrat(double a) => this.a = a;
    public override double Pole() => a * a;
    public override string Nazwa => "Kwadrat";
}

abstract class Raport
{
    public void Generuj()
    {
        Console.WriteLine("=== POCZĄTEK ===");
        Tresc();
        Console.WriteLine("=== KONIEC ===");
    }

    protected abstract void Tresc();
}

class RaportDzienny : Raport
{
    protected override void Tresc() => Console.WriteLine("Dane z dzisiaj");
}
