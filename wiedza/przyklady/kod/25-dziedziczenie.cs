// Dziedziczenie — lekcja 9.1
//
// Uruchomienie:  dotnet run 25-dziedziczenie.cs

Kot k = new Kot("Mruczek", 3, true);
Pies p = new Pies("Burek", 5);

k.Przedstaw();      // metoda odziedziczona — nie ma jej w klasie Kot
p.Przedstaw();
k.Drapie();         // metoda własna kota — pies jej nie ma

// Wszystko dziedziczy po object — stamtąd pochodzi ToString i GetType.
Console.WriteLine(k.GetType());

class Zwierze
{
    public string Imie { get; }
    public int Wiek { get; }

    // protected: widzi to ta klasa I klasy dziedziczące (private by nie wystarczyło).
    protected int energia = 100;

    public Zwierze(string imie, int wiek)
    {
        Imie = imie;
        Wiek = wiek;
    }

    public void Przedstaw() => Console.WriteLine($"Jestem {Imie}, mam {Wiek} lat, energia {energia}");
}

class Kot : Zwierze
{
    public bool Wychodzacy { get; }

    // base(...) wykonuje się PRZED ciałem tego konstruktora.
    // Bez niego: CS7036, bo Zwierze wymaga imienia i wieku.
    public Kot(string imie, int wiek, bool wychodzacy) : base(imie, wiek)
    {
        Wychodzacy = wychodzacy;
    }

    public void Drapie()
    {
        energia = energia - 5;      // działa dzięki protected
        Console.WriteLine($"{Imie} drapie kanapę, zostało {energia} energii");
    }
}

class Pies : Zwierze
{
    public Pies(string imie, int wiek) : base(imie, wiek) { }
}
