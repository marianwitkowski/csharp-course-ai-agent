// ZEPSUTE — lekcja 9.2 (virtual / override)
// Objaw: lista zwierząt ma psa i kota; pętla wypisuje dla psa "...wydaje dźwięk", dla kota "Miau!".
// `Pies.Dzwiek()` wywołany bezpośrednio działa. Kompilator ostrzega — przeczytaj ostrzeżenie.
List<Zwierze> zwierzeta = new List<Zwierze>();
zwierzeta.Add(new Pies());
zwierzeta.Add(new Kot());

foreach (Zwierze z in zwierzeta)
{
    Console.WriteLine(z.Dzwiek());
}

class Zwierze
{
    public virtual string Dzwiek()
    {
        return "...wydaje dźwięk";
    }
}

class Pies : Zwierze
{
    public string Dzwiek()
    {
        return "Hau!";
    }
}

class Kot : Zwierze
{
    public override string Dzwiek()
    {
        return "Miau!";
    }
}

// --- Dla narzedzia/sprawdz-przyklady.sh: tak ten plik ma się zachowywać (odtwarzane w CI,
// --- liczby w zapisie z kropką, kody diagnostyk zamiast treści komunikatów). Dla ciebie: objaw jest w nagłówku.
// CI-ostrzezenie: CS0114
// CI-wyjscie: ...wydaje dźwięk
// CI-wyjscie: Miau!
