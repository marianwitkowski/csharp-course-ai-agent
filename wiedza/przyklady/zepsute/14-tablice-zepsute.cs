// ZEPSUTE — lekcja 6.1 (tablice)
// Objaw: program wypisuje wszystkie cztery temperatury, a potem przerywa działanie wyjątkiem
// IndexOutOfRangeException. Przeczytaj komunikat (który indeks?), znajdź przyczynę, napraw.
int[] temperatury = { 12, 15, 9, 20 };

for (int i = 0; i <= temperatury.Length; i++)
{
    Console.WriteLine($"Dzień {i + 1}: {temperatury[i]} stopni");
}

// --- Dla narzedzia/sprawdz-przyklady.sh: tak ten plik ma się zachowywać (odtwarzane w CI,
// --- liczby w zapisie z kropką, kody diagnostyk zamiast treści komunikatów). Dla ciebie: objaw jest w nagłówku.
// CI-wyjscie: Dzień 1: 12 stopni
// CI-wyjscie: Dzień 2: 15 stopni
// CI-wyjscie: Dzień 3: 9 stopni
// CI-wyjscie: Dzień 4: 20 stopni
// CI-wyjatek: IndexOutOfRangeException
