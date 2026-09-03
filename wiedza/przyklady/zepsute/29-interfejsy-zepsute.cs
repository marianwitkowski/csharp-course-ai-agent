// ZEPSUTE — lekcja 10.1 (interfejsy)
// Objaw: program nie kompiluje się. Kompilator mówi, że klasa nie implementuje składowej interfejsu,
// choć metoda o "takiej" nazwie jest w klasie. Przeczytaj komunikat dokładnie — z kodem CSxxxx — i napraw.
IOpisywalny p = new Produkt("Mleko", 3.49m);
Console.WriteLine(p.Opis());

interface IOpisywalny
{
    string Opis();
}

class Produkt : IOpisywalny
{
    private string nazwa;
    private decimal cena;

    public Produkt(string nazwa, decimal cena)
    {
        this.nazwa = nazwa;
        this.cena = cena;
    }

    public string opis()
    {
        return $"{nazwa} — {cena} zł";
    }
}
