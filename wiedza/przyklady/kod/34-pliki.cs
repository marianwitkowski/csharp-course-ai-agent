// Pliki tekstowe — zapis i odczyt — lekcja 12.1
//
// Uruchomienie:  dotnet run 34-pliki.cs
// Program TWORZY pliki w katalogu, z którego go uruchomisz.

// Cały tekst naraz — nadpisuje plik, jeśli już istniał.
File.WriteAllText("notatka.txt", "Pierwsza linia\nDruga linia\n");
Console.WriteLine(File.ReadAllText("notatka.txt"));

// Lista linii — zapis i odczyt bez ręcznego dzielenia po "\n".
string[] zakupy = { "chleb", "mleko", "jajka" };
File.WriteAllLines("zakupy.txt", zakupy);

string[] wczytane = File.ReadAllLines("zakupy.txt");
Console.WriteLine($"pozycji: {wczytane.Length}, pierwsza: {wczytane[0]}");

// Dopisanie na koniec — bez kasowania tego, co było.
File.AppendAllText("zakupy.txt", "masło\n");
Console.WriteLine($"po dopisaniu: {File.ReadAllLines("zakupy.txt").Length}");

// Odczyt pliku, którego nie ma, to WYJĄTEK — nie pusty tekst.
try
{
    File.ReadAllText("nie-ma-mnie.txt");
}
catch (FileNotFoundException e)
{
    Console.WriteLine($"złapane: {e.GetType().Name}");
}

// Dwa sposoby zabezpieczenia. File.Exists czyta się lepiej,
// ale plik może zniknąć MIĘDZY sprawdzeniem a odczytem — dlatego try/catch
// zostaje jedyną pełną ochroną.
if (File.Exists("zakupy.txt"))
{
    Console.WriteLine("plik jest — czytam");
}

// StreamWriter: zapis linia po linii, gdy danych jest dużo
// albo powstają stopniowo. `using` gwarantuje domknięcie pliku.
using (StreamWriter pisarz = new StreamWriter("liczby.txt"))
{
    for (int i = 1; i <= 5; i++)
    {
        pisarz.WriteLine($"linia {i}");
    }
}

// Bez `using` dane zostają w buforze i plik bywa PUSTY.
using (StreamReader czytelnik = new StreamReader("liczby.txt"))
{
    string? linia;
    while ((linia = czytelnik.ReadLine()) != null)
    {
        Console.WriteLine($"> {linia}");
    }
}

// Wzorzec "wczytaj — popracuj — zapisz": stan przeżywa uruchomienie programu.
List<string> lista = new List<string>();

if (File.Exists("stan.txt"))
{
    lista = new List<string>(File.ReadAllLines("stan.txt"));
}

lista.Add($"wpis nr {lista.Count + 1}");
File.WriteAllLines("stan.txt", lista);
Console.WriteLine($"stan.txt ma teraz {lista.Count} wpisów (uruchom ponownie)");
