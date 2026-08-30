// Konstruktory — lekcja 8.2
//
// Uruchomienie:  dotnet run 22-konstruktory.cs

// Gotowy obiekt jedną linią — i nie da się zapomnieć o danych.
Kot a = new Kot("Mruczek", 3);
Kot b = new Kot("Filemon");
Kot c = new Kot();

Console.WriteLine(a);
Console.WriteLine(b);
Console.WriteLine(c);

// Konstruktor sprawdza dane w JEDNYM miejscu — bo jest jedynym wejściem.
Kot dziwny = new Kot("", -5);
Console.WriteLine(dziwny);

class Kot
{
    public string Imie;
    public int Wiek;

    // TRZY konstruktory o tej samej nazwie — w lekcji 7.2 to samo dało CS0128.
    // Różnica: jesteśmy w klasie. To jest odpowiedź na tamtą przeszkodę.
    public Kot(string imie, int wiek)
    {
        if (string.IsNullOrWhiteSpace(imie)) imie = "bezimienny";
        if (wiek < 0) wiek = 0;
        Imie = imie;
        Wiek = wiek;
    }

    // : this(...) wywołuje tamten konstruktor — sprawdzanie zostaje w jednym miejscu.
    public Kot(string imie) : this(imie, 1) { }

    public Kot() : this("bezimienny", 1) { }

    // override = "zamiast domyślnego". Działa i w WriteLine, i w interpolacji.
    public override string ToString() => $"{Imie} ({Wiek} lat)";
}
