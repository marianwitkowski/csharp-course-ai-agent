// Dictionary — lekcja 6.3
//
// Uruchomienie:  dotnet run 16-dictionary.cs

Dictionary<string, int> wiek = new Dictionary<string, int>();

wiek["Ala"] = 30;     // indeksator: dodaje ALBO nadpisuje po cichu
wiek.Add("Bo", 25);   // Add: przy istniejącym kluczu rzuca ArgumentException
wiek["Cyd"] = 40;

Console.WriteLine($"Wpisów: {wiek.Count}, Ala ma {wiek["Ala"]}");

wiek["Ala"] = 31;     // nadpisanie
Console.WriteLine(wiek["Ala"]);

// wiek["Nikt"] — KeyNotFoundException. Bezpiecznie przez TryGetValue:
// ten sam wzorzec "Try" co int.TryParse z lekcji 2.3.
bool jest = wiek.TryGetValue("Nikt", out int ile);
Console.WriteLine($"Znaleziono: {jest}, wartość: {ile}");

if (wiek.TryGetValue("Ala", out int wiekAli))
{
    Console.WriteLine($"Ala ma {wiekAli} lat.");
}

// Przejście po parach. Kolejność NIE jest obiecana.
foreach (var para in wiek) Console.WriteLine($"{para.Key} -> {para.Value}");

// Zliczanie — zadanie, dla którego słownik istnieje.
string zdanie = "ala ma kota ala";
Dictionary<string, int> ileRazy = new Dictionary<string, int>();
foreach (string slowo in zdanie.Split(' '))
{
    if (ileRazy.ContainsKey(slowo)) ileRazy[slowo] = ileRazy[slowo] + 1;
    else ileRazy[slowo] = 1;
}
foreach (var para in ileRazy) Console.Write($"{para.Key}={para.Value} ");
Console.WriteLine();
