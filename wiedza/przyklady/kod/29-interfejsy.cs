// Interfejsy — lekcja 10.1
//
// Uruchomienie:  dotnet run 29-interfejsy.cs

// Polimorfizm BEZ wspólnej klasy bazowej — łączy je tylko umiejętność.
IPiszacy[] rzeczy = { new Dlugopis(), new Kreda(), new Olowek() };
foreach (IPiszacy r in rzeczy) Console.WriteLine(r.Pisz("test"));

// Kreda implementuje DWA interfejsy — na dwie klasy bazowe C# by nie pozwolił (CS1721).
Kreda k = new Kreda();
Console.WriteLine(k.Pisz("x"));
Console.WriteLine(k.Sciera());

// Metoda wymaga tylko UMIEJĘTNOŚCI, nie konkretnego typu.
// Zadziała też z klasami, które napiszesz za rok.
Napisz(new Dlugopis(), "raz");
Napisz(new Kreda(), "dwa");

void Napisz(IPiszacy narzedzie, string tekst) => Console.WriteLine(narzedzie.Pisz(tekst));

interface IPiszacy
{
    string Pisz(string tekst);      // sam nagłówek, bez ciała
}

interface IScieralny
{
    string Sciera();
}

// Implementacja MUSI być public (inaczej CS0737).
// Brak implementacji: CS0535.
class Dlugopis : IPiszacy { public string Pisz(string t) => $"długopis: {t}"; }
class Olowek : IPiszacy { public string Pisz(string t) => $"ołówek: {t}"; }

class Kreda : IPiszacy, IScieralny
{
    public string Pisz(string t) => $"kreda: {t}";
    public string Sciera() => "starte";
}
