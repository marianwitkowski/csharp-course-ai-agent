// foreach, break i continue — lekcja 5.3
//
// Uruchomienie:  dotnet run 13-foreach.cs

string imie = "Anna";

// Wersja z `for`: potrzebny licznik, warunek, krok i indeksowanie.
for (int i = 0; i < imie.Length; i++)
{
    Console.Write(imie[i] + " ");
}
Console.WriteLine();

// To samo przez `foreach` — bez licznika, bez indeksu, bez pomyłki o jeden.
// Tekst jest ciągiem znaków; w module 6 tak samo pójdą kolekcje.
foreach (char znak in imie)
{
    Console.Write(znak + " ");
}
Console.WriteLine();

// Zmiennej z foreach NIE da się zmienić (CS1656) — foreach służy do oglądania:
// foreach (char znak in imie) { znak = 'x'; }

// break: przerywa całą pętlę, gdy odpowiedź jest już znana.
string slowo = "abc123";
bool maCyfre = false;
foreach (char znak in slowo)
{
    if (znak >= '0' && znak <= '9')
    {
        maCyfre = true;
        break;
    }
}
Console.WriteLine($"Czy zawiera cyfrę: {maCyfre}");

// continue: kończy JEDEN obieg i przechodzi do następnego.
foreach (char znak in "a1b2c3")
{
    if (znak >= '0' && znak <= '9')
    {
        continue;
    }
    Console.Write(znak);
}
Console.WriteLine();

// break przerywa TYLKO najbliższą pętlę — zewnętrzna leci dalej.
for (int i = 1; i <= 3; i++)
{
    for (int j = 1; j <= 3; j++)
    {
        if (j == 2) { break; }
        Console.Write($"{i}-{j} ");
    }
}
Console.WriteLine();

// foreach po pustym tekście nie wykonuje się ani razu.
int obiegi = 0;
foreach (char znak in "")
{
    obiegi++;
}
Console.WriteLine($"Pusty tekst: {obiegi} obiegów");
