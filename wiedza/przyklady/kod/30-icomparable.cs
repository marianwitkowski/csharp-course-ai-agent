// IComparable — lekcja 10.2
//
// Uruchomienie:  dotnet run 30-icomparable.cs

// Bez IComparable Sort() rzuciłby InvalidOperationException.
// Sort był gotowy i czekał na kontrakt.
List<Uczen> lista = new List<Uczen>
{
    new Uczen("Cyd", 3), new Uczen("Ala", 5), new Uczen("Bo", 4), new Uczen("Ada", 5)
};
lista.Sort();
Console.WriteLine(string.Join(" ", lista));

// Array.Sort też pyta o IComparable — jeden kontrakt, wiele narzędzi.
Uczen[] tablica = { new Uczen("Cyd", 3), new Uczen("Ala", 5) };
Array.Sort(tablica);
Console.WriteLine(string.Join(" ", tablica));

// Znak wyniku CompareTo: ujemny = przed, zero = równe, dodatni = za.
Console.WriteLine($"{5.CompareTo(3)} {3.CompareTo(5)} {4.CompareTo(4)} {"Ala".CompareTo("Bo")}");

// foreach działa na tekście, tablicy, liście i słowniku dzięki IEnumerable.
// Zmienna typu "coś, po czym da się przejść":
IEnumerable<int> cos = new List<int> { 1, 2, 3 };
foreach (int x in cos) Console.Write(x + " ");
Console.WriteLine();

// PUŁAPKA: wersje sortowane jako tekst wychodzą źle — "1.10.0" jest przed "1.2.0".
string[] wersje = { "1.10.0", "1.9.0", "1.2.0" };
Array.Sort(wersje);
Console.WriteLine(string.Join(" ", wersje));

class Uczen : IComparable<Uczen>
{
    public string Imie { get; }
    public int Ocena { get; }
    public Uczen(string imie, int ocena) { Imie = imie; Ocena = ocena; }

    // Sortowanie po ocenie, a przy równych ocenach — alfabetycznie po imieniu.
    public int CompareTo(Uczen? inny)
    {
        if (inny == null) return 1;
        int poOcenie = Ocena.CompareTo(inny.Ocena);
        if (poOcenie != 0) return poOcenie;
        return Imie.CompareTo(inny.Imie);
    }

    public override string ToString() => $"{Imie}({Ocena})";
}
