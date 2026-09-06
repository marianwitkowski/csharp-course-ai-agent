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

// --- Dla narzedzia/sprawdz-przyklady.sh: tak ten plik ma się zachowywać (odtwarzane w CI,
// --- liczby w zapisie z kropką, kody diagnostyk zamiast treści komunikatów). Dla ciebie: objaw jest w nagłówku.
// CI-wejscie: 18
// CI-wyjscie: Podaj wiek: niepełnoletni
