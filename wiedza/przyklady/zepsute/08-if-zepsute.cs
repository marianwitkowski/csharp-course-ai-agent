// ZEPSUTE — lekcja 4.1 (if / else)
// Objaw: dla wieku 18 program wypisuje "niepełnoletni", a osiemnastolatek jest pełnoletni.
// Dla 17 i 19 działa poprawnie. Skompiluj, uruchom z wejściem 18, znajdź przyczynę, napraw.
Console.Write("Podaj wiek: ");
bool ok = int.TryParse(Console.ReadLine(), out int wiek);

if (ok)
{
    if (wiek > 18)
    {
        Console.WriteLine("pełnoletni");
    }
    else
    {
        Console.WriteLine("niepełnoletni");
    }
}
else
{
    Console.WriteLine("to nie jest liczba");
}
