// Agregacje i grupowanie — lekcja 13.2
//
// Uruchomienie:  dotnet run 39-agregacje.cs

List<int> liczby = new List<int> { 5, 12, 3, 20, 8 };

// Agregacje zwracają JEDNĄ wartość — nie potrzebują ToList.
Console.WriteLine($"{liczby.Count()} {liczby.Sum()} {liczby.Min()} {liczby.Max()} {liczby.Average()}");

// Lambda przy Count to WARUNEK (które liczyć)...
Console.WriteLine($"powyżej 5: {liczby.Count(x => x > 5)}");

Console.WriteLine($"Any > 100: {liczby.Any(x => x > 100)}, All > 0: {liczby.All(x => x > 0)}");

// Pusta kolekcja: Sum i Count dają 0, Max i Average rzucają wyjątek.
List<int> pusta = new List<int>();
Console.WriteLine($"pusta: Sum={pusta.Sum()} Count={pusta.Count()} Any={pusta.Any()} All={pusta.All(x => x > 5)}");

try
{
    Console.WriteLine(pusta.Max());
}
catch (InvalidOperationException e)
{
    Console.WriteLine($"Max na pustej: {e.Message}");
}

List<Uczen> uczniowie = new List<Uczen>
{
    new Uczen { Imie = "Ala", Klasa = "1A", Punkty = 90 },
    new Uczen { Imie = "Bartek", Klasa = "1B", Punkty = 55 },
    new Uczen { Imie = "Celina", Klasa = "1A", Punkty = 70 }
};

// ...a przy Sum i Max to WYBÓR WARTOŚCI (co dodać, co porównać).
Console.WriteLine($"suma punktów: {uczniowie.Sum(u => u.Punkty)}, najlepszy: {uczniowie.Max(u => u.Punkty)}");

// GroupBy dzieli na kubełki. Key to wartość, która je połączyła,
// a sama grupa jest kolekcją — da się po niej przejść i policzyć wszystko.
foreach (var grupa in uczniowie.GroupBy(u => u.Klasa))
{
    Console.WriteLine($"{grupa.Key}: {grupa.Count()} osób, suma {grupa.Sum(u => u.Punkty)}");

    foreach (Uczen u in grupa)
    {
        Console.WriteLine($"   - {u.Imie}");
    }
}

// Po GroupBy łańcuch operuje już na GRUPACH, nie na uczniach.
foreach (var g in uczniowie.GroupBy(u => u.Klasa).OrderByDescending(g => g.Count()))
{
    Console.WriteLine($"{g.Key} -> {g.Count()}");
}

// ToDictionary: pierwsza lambda wskazuje klucz, druga wartość.
// Powtórzony klucz to ArgumentException.
var punktyWg = uczniowie.ToDictionary(u => u.Imie, u => u.Punkty);
Console.WriteLine($"Ala ma {punktyWg["Ala"]}");

// Słownik też jest kolekcją — element ma Key i Value.
Console.WriteLine(string.Join(", ", punktyWg.Where(p => p.Value > 60).Select(p => p.Key)));

class Uczen
{
    public string Imie { get; set; } = "";
    public string Klasa { get; set; } = "";
    public int Punkty { get; set; }
}
