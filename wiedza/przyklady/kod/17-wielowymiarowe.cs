// Tablice wielowymiarowe — lekcja 6.4
//
// Uruchomienie:  dotnet run 17-wielowymiarowe.cs

int[,] plansza = new int[2, 3];   // 2 wiersze, 3 kolumny
plansza[0, 0] = 1;
plansza[1, 2] = 9;

// Length liczy WSZYSTKIE pola. Do wymiarów służy GetLength (z nawiasami!).
Console.WriteLine($"Length={plansza.Length}, wierszy={plansza.GetLength(0)}, kolumn={plansza.GetLength(1)}");

// Zagnieżdżona pętla z lekcji 5.2 — tutaj ma po co być.
// WriteLine po pętli wewnętrznej kończy wiersz.
for (int w = 0; w < plansza.GetLength(0); w++)
{
    for (int k = 0; k < plansza.GetLength(1); k++) Console.Write(plansza[w, k] + " ");
    Console.WriteLine();
}

// Tabliczka mnożenia. {x,4} wyrównuje do czterech znaków.
int[,] tabliczka = new int[5, 5];
for (int w = 0; w < tabliczka.GetLength(0); w++)
    for (int k = 0; k < tabliczka.GetLength(1); k++)
        tabliczka[w, k] = (w + 1) * (k + 1);

for (int w = 0; w < tabliczka.GetLength(0); w++)
{
    for (int k = 0; k < tabliczka.GetLength(1); k++) Console.Write($"{tabliczka[w, k],4}");
    Console.WriteLine();
}

// foreach działa, ale nie mówi, w którym wierszu i kolumnie jesteś.
int suma = 0;
foreach (int pole in tabliczka) suma = suma + pole;
Console.WriteLine($"Suma wszystkich pól: {suma}");

// Tablica tablic: wiersze mogą mieć RÓŻNĄ długość. Dostęp dwoma nawiasami.
// Każdy wiersz trzeba utworzyć osobno — bez tego NullReferenceException.
int[][] postrzepiona = new int[2][];
postrzepiona[0] = new int[] { 1, 2 };
postrzepiona[1] = new int[] { 3, 4, 5 };
Console.WriteLine($"{postrzepiona[0].Length} i {postrzepiona[1].Length}, element [1][2] = {postrzepiona[1][2]}");
