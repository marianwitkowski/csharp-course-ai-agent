// ZEPSUTE — lekcja 9.2 (virtual / override)
// Objaw: lista zwierząt ma psa i kota, a pętla wypisuje dwa razy "...wydaje dźwięk".
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
