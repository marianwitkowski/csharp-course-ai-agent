// Operatory logiczne i operator warunkowy — lekcja 4.2
//
// Uruchomienie:  dotnet run 09-logiczne.cs

int wiek = 20;
bool maBilet = true;

Console.WriteLine(wiek >= 16 && maBilet);   // oba muszą być prawdą
Console.WriteLine(wiek < 16 || maBilet);    // wystarczy jeden
Console.WriteLine(!maBilet);                // odwrócenie

// Dwa warunki w jednym `if` zamiast zagnieżdżania z lekcji 4.1.
if (maBilet && wiek >= 16)
{
    Console.WriteLine("Wpuszczam");
}

// SKRÓCONE OBLICZANIE: lewa strona jest fałszem, więc prawa NIE jest liczona
// — mimo że samo 10 / zero przewróciłoby program.
int zero = 0;
Console.WriteLine(zero != 0 && 10 / zero > 1);

// Pierwszeństwo: && wiąże mocniej niż || — jak mnożenie przed dodawaniem.
Console.WriteLine(true || false && false);     // True
Console.WriteLine((true || false) && false);   // False

// Operator warunkowy: wybiera jedną z dwóch WARTOŚCI (nie czynności).
int w = 15;
string opis = w >= 18 ? "pełnoletni" : "niepełnoletni";
Console.WriteLine(opis);

// Zakres piszemy dwoma warunkami — zapis 16 <= w <= 120 w C# nie istnieje.
// Poniżej wyjdzie False, bo w wynosi 15. Zmień w na 20 i uruchom ponownie.
Console.WriteLine(w >= 16 && w <= 120);
