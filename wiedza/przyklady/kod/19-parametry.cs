// Parametry domyślne i nazwane — lekcja 7.2
//
// Uruchomienie:  dotnet run 19-parametry.cs

// Parametry opcjonalne muszą stać NA KOŃCU (inaczej CS1737).
void Powitaj(string imie, string powitanie = "Cześć", bool wykrzyknik = true)
{
    Console.WriteLine($"{powitanie}, {imie}{(wykrzyknik ? "!" : ".")}");
}

Powitaj("Ala");                          // obie domyślne
Powitaj("Bo", "Dzień dobry");            // druga podana
Powitaj("Cyd", wykrzyknik: false);       // środkowy POMINIĘTY — stąd nazwa
Powitaj(powitanie: "Hej", imie: "Ewa");  // nazwane pozwalają zmienić kolejność

// PRZECIĄŻANIE — używasz go od lekcji 1.1, nie wiedząc o tym.
// Jedna nazwa, pięć różnych rodzajów danych:
Console.WriteLine(42);
Console.WriteLine("tekst");
Console.WriteLine(true);
Console.WriteLine(3.14);
Console.WriteLine('z');

// Własnego przeciążenia jeszcze NIE napiszesz:
//   int Dodaj(int a, int b) => a + b;
//   double Dodaj(double a, double b) => a + b;   // CS0128
// Metody w pliku bez klasy to funkcje lokalne, a tych nie da się przeciążać.
// W module 8, gdy poznasz klasy, będzie można. To jeden z konkretnych
// powodów, dla których klasy istnieją.

// Na razie: jedna metoda na szerszym typie zwykle wystarcza.
double Dodaj(double a, double b) => a + b;
Console.WriteLine(Dodaj(2, 3));      // int sam zamienia się na double

// params — dowolna liczba argumentów, w środku zwykła tablica.
int Suma(params int[] liczby)
{
    int suma = 0;
    foreach (int x in liczby) suma = suma + x;
    return suma;
}
Console.WriteLine(Suma(1, 2, 3));
Console.WriteLine(Suma());
