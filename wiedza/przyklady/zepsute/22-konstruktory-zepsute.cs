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

// --- Dla narzedzia/sprawdz-przyklady.sh: tak ten plik ma się zachowywać (odtwarzane w CI,
// --- liczby w zapisie z kropką, kody diagnostyk zamiast treści komunikatów). Dla ciebie: objaw jest w nagłówku.
// CI-ostrzezenie: CS1717
// CI-ostrzezenie: CS0649
// CI-wyjscie: Mruczek (0 lat)
