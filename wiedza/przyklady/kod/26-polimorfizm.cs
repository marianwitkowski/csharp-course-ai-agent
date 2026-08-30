// virtual, override i polimorfizm — lekcja 9.2
//
// Uruchomienie:  dotnet run 26-polimorfizm.cs

// Jedna kolekcja typu bazowego, w środku różne typy.
List<Zwierze> zoo = new List<Zwierze>
{
    new Kot("Mruczek"),
    new Pies("Burek"),
    new Kot("Filemon")
};

// Jedno polecenie, różne odpowiedzi — i pętla NIE WIE, jakie to typy.
foreach (Zwierze z in zoo)
{
    Console.WriteLine($"{z.Imie}: {z.DajGlos()}");
}

// Przedstaw jest napisane RAZ w klasie bazowej, a woła kocią/psią wersję DajGlos.
foreach (Zwierze z in zoo) z.Przedstaw();

// Typ zmiennej mówi, czego wolno używać; typ obiektu decyduje, co się wykona.
Zwierze x = new Pies("Reks");
Console.WriteLine(x.DajGlos());     // hau
Console.WriteLine(x);               // Pies Reks — dzięki override ToString
Console.WriteLine(x.GetType());     // Pies

class Zwierze
{
    public string Imie { get; }
    public Zwierze(string imie) => Imie = imie;

    // virtual = "tę metodę WOLNO podmienić"
    public virtual string DajGlos() => "...";

    // NIE wirtualna: zachowanie ma być takie samo dla wszystkich
    public void Przedstaw() => Console.WriteLine($"{Imie} mówi: {DajGlos()}");

    public override string ToString() => Imie;
}

class Kot : Zwierze
{
    public Kot(string imie) : base(imie) { }
    // override = "podmieniam". Bez virtual w bazie: CS0506.
    // Bez override w pochodnej: CS0108 i metoda tylko UKRYTA, nie podmieniona.
    public override string DajGlos() => "miau";
}

class Pies : Zwierze
{
    public Pies(string imie) : base(imie) { }
    public override string DajGlos() => "hau";
    // base.ToString() woła wersję bazową — bez base. byłaby nieskończona rekurencja.
    public override string ToString() => $"Pies {base.ToString()}";
}
