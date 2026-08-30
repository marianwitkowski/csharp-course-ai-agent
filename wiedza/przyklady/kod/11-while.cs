// Pętla while i do...while — lekcja 5.1
//
// Uruchomienie:  dotnet run 11-while.cs

// while to `if`, który się powtarza: warunek sprawdzany PRZED każdym obiegiem.
int licznik = 0;
while (licznik < 3)
{
    Console.WriteLine($"Obieg numer {licznik}");
    licznik = licznik + 1;   // BEZ tej linii pętla nigdy by się nie skończyła
}
Console.WriteLine("Koniec");

// while może wykonać się ZERO razy — gdy warunek jest fałszywy od początku.
int pusty = 5;
while (pusty < 3)
{
    Console.WriteLine("to się nie wypisze");
}

// do...while sprawdza warunek PO wnętrzu, więc wnętrze wykonuje się co najmniej raz.
// Zwróć uwagę na średnik po while — jedyne takie miejsce w C#.
int j = 10;
do
{
    Console.WriteLine($"do-while wykonało się dla j = {j}");
    j++;
}
while (j < 3);

// Sumowanie: zmienna `suma` PRZED pętlą. W środku zerowałaby się co obieg.
int suma = 0;
int n = 1;
while (n <= 100)
{
    suma = suma + n;
    n++;
}
Console.WriteLine($"Suma liczb od 1 do 100: {suma}");

// Odliczanie w dół.
int odliczanie = 5;
while (odliczanie > 0)
{
    Console.Write(odliczanie + " ");
    odliczanie--;
}
Console.WriteLine("Start!");
