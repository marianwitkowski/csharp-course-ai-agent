// Stałe i typy wyliczeniowe — lekcja 2.2
//
// Uruchomienie:  dotnet run 03-const-enum.cs

const double StawkaVat = 0.23;
const string NazwaSklepu = "Warzywniak";

Console.WriteLine($"{NazwaSklepu}: VAT wynosi {StawkaVat:P0}");

// StawkaVat = 0.08;   // CS0131 — stałej nie da się zmienić. O to chodzi.

DzienTygodnia dzis = DzienTygodnia.Sroda;
Console.WriteLine(dzis);          // Sroda — enum wypisuje się nazwą
Console.WriteLine((int)dzis);     // 2     — pod spodem to liczba

// Enum daje kompilatorowi wiedzę, jakie wartości są w ogóle dopuszczalne.
// `string dzien = "śrida";` przeszłoby bez słowa; `DzienTygodnia.Srida` nie.

enum DzienTygodnia
{
    Poniedzialek,   // 0
    Wtorek,         // 1
    Sroda,          // 2
    Czwartek,       // 3
    Piatek,         // 4
    Sobota,         // 5
    Niedziela,      // 6
}
