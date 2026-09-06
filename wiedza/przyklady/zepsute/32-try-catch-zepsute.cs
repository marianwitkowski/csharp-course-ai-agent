// ZEPSUTE — lekcja 11.1 (try / catch)
// Objaw: gdy użytkownik wpisze "abc", program wypisuje "Podwojone: 0" i kończy się, jakby nic się nie stało.
// Nie ma wyjątku, nie ma komunikatu. Znajdź, gdzie ginie informacja o błędzie, i napraw tak,
// żeby użytkownik dowiedział się, co poszło nie tak.
Console.Write("Podaj liczbę: ");
int liczba = 0;

try
{
    liczba = int.Parse(Console.ReadLine() ?? "");
}
catch (FormatException)
{
}

Console.WriteLine($"Podwojone: {liczba * 2}");

// --- Dla narzedzia/sprawdz-przyklady.sh: tak ten plik ma się zachowywać (odtwarzane w CI,
// --- liczby w zapisie z kropką, kody diagnostyk zamiast treści komunikatów). Dla ciebie: objaw jest w nagłówku.
// CI-wejscie: abc
// CI-wyjscie: Podaj liczbę: Podwojone: 0
