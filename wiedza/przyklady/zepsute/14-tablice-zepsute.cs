// ZEPSUTE — lekcja 6.1 (tablice)
// Objaw: program wypisuje wszystkie cztery temperatury, a potem przerywa działanie wyjątkiem
// IndexOutOfRangeException. Przeczytaj komunikat (który indeks?), znajdź przyczynę, napraw.
int[] temperatury = { 12, 15, 9, 20 };

for (int i = 0; i <= temperatury.Length; i++)
{
    Console.WriteLine($"Dzień {i + 1}: {temperatury[i]} stopni");
}
