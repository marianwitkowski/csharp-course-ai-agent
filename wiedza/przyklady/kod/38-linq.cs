// LINQ: Where, Select, OrderBy — lekcja 13.1
//
// Uruchomienie:  dotnet run 38-linq.cs

List<int> liczby = new List<int> { 5, 12, 3, 20, 8 };

// Where wybiera KTÓRE. Select mówi CO z każdego. OrderBy — czym się kierować.
Console.WriteLine(string.Join(", ", liczby.Where(x => x > 5)));
Console.WriteLine(string.Join(", ", liczby.Select(x => x * 2)));
Console.WriteLine(string.Join(", ", liczby.OrderBy(x => x)));
Console.WriteLine(string.Join(", ", liczby.OrderByDescending(x => x)));

// Źródło zostaje nietknięte — LINQ zawsze buduje NOWY wynik.
Console.WriteLine($"źródło bez zmian: {string.Join(", ", liczby)}");

// Select może zmienić typ: z liczb robią się teksty.
Console.WriteLine(string.Join(", ", liczby.Select(x => $"[{x}]")));

List<Uczen> uczniowie = new List<Uczen>
{
    new Uczen { Imie = "Ala", Klasa = "1A", Punkty = 90 },
    new Uczen { Imie = "Bartek", Klasa = "1B", Punkty = 55 },
    new Uczen { Imie = "Celina", Klasa = "1A", Punkty = 70 }
};

// Łańcuch czyta się po polsku: weź uczniów, zostaw powyżej 60,
// z każdego weź imię, posortuj alfabetycznie.
var wynik = uczniowie
    .Where(u => u.Punkty > 60)
    .Select(u => u.Imie)
    .OrderBy(i => i);

Console.WriteLine(string.Join(", ", wynik));

// KOLEJNOŚĆ MA ZNACZENIE: po Select(u => u.Imie) w rękach jest już tekst,
// a tekst nie ma właściwości Punkty. Odwrotna kolejność się nie kompiluje.

// Wynik zapytania to IEnumerable — da się po nim przejść, ale nie dodać.
// wynik.Add("Damian");   // error CS1061: brak definicji „Add”
List<string> lista = wynik.ToList();
lista.Add("Damian");
Console.WriteLine($"po ToList i Add: {string.Join(", ", lista)}");

// ODROCZONE WYKONANIE: Where zapamiętuje przepis, nie wynik.
List<int> zrodlo = new List<int> { 1, 2 };
var zapytanie = zrodlo.Where(x => x > 0);
zrodlo.Add(3);
Console.WriteLine($"przepis widzi trójkę: {string.Join(", ", zapytanie)}");

var zamrozone = zrodlo.Where(x => x > 0).ToList();
zrodlo.Add(4);
Console.WriteLine($"ToList nie widzi czwórki: {string.Join(", ", zamrozone)}");

// Zmiana kolekcji w trakcie przeglądania kończy się wyjątkiem.
try
{
    foreach (int x in zrodlo.Where(v => v > 0))
    {
        zrodlo.Add(99);
    }
}
catch (InvalidOperationException e)
{
    Console.WriteLine($"złapane: {e.Message}");
}

// LINQ działa na wszystkim, po czym da się przejść foreach-em.
int[] tab = { 3, 1, 2 };
Console.WriteLine(string.Join(", ", tab.OrderBy(x => x)));
Console.WriteLine($"liter 'a' w tekście: {"Ala ma kota".Count(c => c == 'a')}");

class Uczen
{
    public string Imie { get; set; } = "";
    public string Klasa { get; set; } = "";
    public int Punkty { get; set; }
}
