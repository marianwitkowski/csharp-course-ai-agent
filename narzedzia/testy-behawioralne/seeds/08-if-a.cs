// Lekcja 4.1 — rozgrzewka: wiek

Console.Write("Podaj wiek: ");
string wpis = Console.ReadLine();

int wiek;
if (int.TryParse(wpis, out wiek))
{
    if (wiek >= 18)
    {
        Console.WriteLine("Pełnoletni");
    }
    else
    {
        Console.WriteLine("Niepełnoletni");
    }
}
else
{
    Console.WriteLine("To nie jest liczba");
}
