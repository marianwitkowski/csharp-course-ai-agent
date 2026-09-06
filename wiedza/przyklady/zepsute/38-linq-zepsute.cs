// ZEPSUTE — lekcja 13.1 (LINQ)
// Objaw: "posortowałem listę przez OrderBy, a wypisuje się w starej kolejności".
// Program się kompiluje bez ostrzeżeń. Znajdź przyczynę i napraw — bez pisania własnej pętli sortującej.
List<int> punkty = new List<int> { 42, 7, 19, 88, 3 };

punkty.OrderBy(p => p);

foreach (int p in punkty)
{
    Console.WriteLine(p);
}

// --- Dla narzedzia/sprawdz-przyklady.sh: tak ten plik ma się zachowywać (odtwarzane w CI,
// --- liczby w zapisie z kropką, kody diagnostyk zamiast treści komunikatów). Dla ciebie: objaw jest w nagłówku.
// CI-wyjscie: 42
// CI-wyjscie: 7
// CI-wyjscie: 19
// CI-wyjscie: 88
// CI-wyjscie: 3
