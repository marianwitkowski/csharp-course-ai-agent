// ZEPSUTE — lekcja 8.2 (konstruktory)
// Objaw: `new Kot("Mruczek", 5)` daje kota, który ma 0 lat. Imię jest poprawne.
// Program się kompiluje, ale kompilator wypisuje dwa ostrzeżenia — przeczytaj je, zanim zaczniesz szukać.
Kot mruczek = new Kot("Mruczek", 5);
Console.WriteLine(mruczek);

class Kot
{
    public string Imie;
    public int Wiek;

    public Kot(string imie, int Wiek)
    {
        Imie = imie;
        Wiek = Wiek;
    }

    public override string ToString()
    {
        return $"{Imie} ({Wiek} lat)";
    }
}
