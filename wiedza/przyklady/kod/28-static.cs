// Składowe statyczne — lekcja 9.4
//
// Uruchomienie:  dotnet run 28-static.cs

// Licznik należy do KLASY, nie do obiektu — jedna sztuka na cały program.
Console.WriteLine(Kot.Ile);

Kot a = new Kot("Mruczek");
new Kot("Filemon");
new Kot("Puszek");

Console.WriteLine(Kot.Ile);      // 3
Console.WriteLine(a.Imie);       // Mruczek — to cecha obiektu
// Console.WriteLine(a.Ile);     // CS0176 — statyczną wołaj przez nazwę klasy

// Klasa statyczna = zbiór narzędzi. Nie da się jej utworzyć (CS0723).
// Dokładnie tak działają Console i Math, których używasz od lekcji 1.1.
Console.WriteLine(Matma.Kwadrat(5));
Console.WriteLine(Matma.Szescian(3));
Console.WriteLine(Fizyka.Predkosc(2));
Console.WriteLine(Math.PI);      // to samo co nasze Fizyka.G — stała statyczna

class Kot
{
    public static int Ile { get; private set; }   // wspólne dla wszystkich kotów
    public string Imie { get; }                    // własne dla każdego

    public Kot(string imie)
    {
        Imie = imie;
        Ile = Ile + 1;
    }

    // Metoda obiektu widzi I swoje pola, I statyczne.
    public void Pokaz() => Console.WriteLine($"{Imie}, a wszystkich kotów jest {Ile}");

    // Odwrotnie NIE działa — metoda statyczna nie wie, o którego kota chodzi:
    // public static void Opisz() { Console.WriteLine(Imie); }   // CS0120
}

static class Matma
{
    public static int Kwadrat(int x) => x * x;
    public static int Szescian(int x) => x * x * x;
}

static class Fizyka
{
    public const double G = 9.81;                  // const jest automatycznie statyczna
    public static double Predkosc(double czas) => G * czas;
}
