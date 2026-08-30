// Kompozycja zamiast dziedziczenia — lekcja 10.3
//
// Uruchomienie:  dotnet run 31-kompozycja.cs
//
// Ta lekcja nie wprowadza ŻADNEJ nowej składni — tylko decyzje projektowe.

// JEDNA klasa Pracownik zamiast hierarchii Handlowiec/Kierownik/KierownikHandlowiec.
Pracownik a = new Pracownik("Ala", 5000, new Etat());
Pracownik b = new Pracownik("Bo", 4000, new ZProwizja(1500));
Console.WriteLine($"{a.Imie}: {a.Wyplata()}");
Console.WriteLine($"{b.Imie}: {b.Wyplata()}");

// Czego dziedziczenie NIE potrafi: wymiany części w trakcie działania.
// Obiekt utworzony jako Handlowiec byłby handlowcem do końca życia.
b.Sposob = new Etat();
Console.WriteLine($"{b.Imie} po zmianie zasad: {b.Wyplata()}");

// Kompozycja bez interfejsu — zwykłe "ma". Pole będące innym obiektem.
new Samochod("Fiat", new Silnik(70)).Odpal();

// Koszyk MA listę, a nie JEST listą. Gdyby dziedziczył po List<decimal>,
// odziedziczyłby Clear, RemoveAt i wszystko, przed czym chronimy go w 8.4.
Koszyk koszyk = new Koszyk();
koszyk.Dodaj(19.99m);
koszyk.Dodaj(5.50m);
Console.WriteLine($"pozycji: {koszyk.Liczba}, suma: {koszyk.Suma():F2}");

interface ISposobWynagrodzenia { decimal Oblicz(decimal podstawa); }

class Etat : ISposobWynagrodzenia
{
    public decimal Oblicz(decimal podstawa) => podstawa;
}

class ZProwizja : ISposobWynagrodzenia
{
    private decimal prowizja;
    public ZProwizja(decimal prowizja) => this.prowizja = prowizja;
    public decimal Oblicz(decimal podstawa) => podstawa + prowizja;
}

class Pracownik
{
    public string Imie { get; }
    private decimal podstawa;
    public ISposobWynagrodzenia Sposob { get; set; }

    public Pracownik(string imie, decimal podstawa, ISposobWynagrodzenia sposob)
    {
        Imie = imie;
        this.podstawa = podstawa;
        Sposob = sposob;
    }

    public decimal Wyplata() => Sposob.Oblicz(podstawa);
}

class Silnik
{
    public int Moc { get; }
    public Silnik(int moc) => Moc = moc;
    public string Uruchom() => $"warkot ({Moc} KM)";
}

class Samochod
{
    public string Marka { get; }
    private Silnik silnik;
    public Samochod(string marka, Silnik silnik) { Marka = marka; this.silnik = silnik; }
    public void Odpal() => Console.WriteLine($"{Marka}: {silnik.Uruchom()}");
}

class Koszyk
{
    private List<decimal> ceny = new List<decimal>();
    public void Dodaj(decimal cena) => ceny.Add(cena);
    public int Liczba => ceny.Count;
    public decimal Suma()
    {
        decimal suma = 0;
        foreach (decimal c in ceny) suma = suma + c;
        return suma;
    }
}
