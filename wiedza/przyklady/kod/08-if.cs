// Instrukcja warunkowa — lekcja 4.1
//
// Uruchomienie:  dotnet run 08-if.cs
//
// Wartości są wpisane na stałe, żeby dało się uruchomić bez wpisywania
// czegokolwiek. Zmieniaj je i uruchamiaj ponownie.

int wiek = 20;

if (wiek >= 18)
{
    Console.WriteLine("Pełnoletni");
}
else
{
    Console.WriteLine("Niepełnoletni");
}

// Łańcuch else if — wykona się DOKŁADNIE JEDNA gałąź, ta pierwsza pasująca.
int ocena = 4;

if (ocena == 5)
{
    Console.WriteLine("bardzo dobry");
}
else if (ocena == 4)
{
    Console.WriteLine("dobry");
}
else if (ocena == 3)
{
    Console.WriteLine("dostateczny");
}
else
{
    Console.WriteLine("nieznana ocena");
}

// PUŁAPKA: bez klamer do `if` należy tylko PIERWSZA instrukcja.
// Wcięcie sugeruje co innego — i właśnie dlatego zawsze piszemy klamry.
int maly = 15;
if (maly >= 18)
    Console.WriteLine("A — nie wypisze się");
Console.WriteLine("B — wypisze się ZAWSZE, mimo wcięcia w kodzie wyżej");

// Porównanie tekstów działa przez ==, ale wielkość liter ma znaczenie.
string haslo = "abc";
Console.WriteLine(haslo == "abc");
Console.WriteLine(haslo == "ABC");
