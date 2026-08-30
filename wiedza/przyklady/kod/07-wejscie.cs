// Wejście od użytkownika — lekcja 3.2
//
// Uruchomienie:  dotnet run 07-wejscie.cs
//
// Program zatrzyma się i poczeka, aż coś wpiszesz i naciśniesz Enter.

// Console.Write, nie WriteLine — kursor zostaje obok pytania.
Console.Write("Jak masz na imię? ");
string imie = Console.ReadLine();
Console.WriteLine($"Cześć, {imie}!");

// Uwaga: linia wyżej daje ostrzeżenie CS8600. To OSTRZEŻENIE, nie błąd —
// program się buduje i działa. Wyjaśnienie w lekcji 3.2.

// ReadLine ZAWSZE oddaje tekst, nawet gdy wpiszesz same cyfry.
// Dlatego liczbę trzeba z tego tekstu wydobyć.
Console.Write("Podaj rok urodzenia: ");
bool ok = int.TryParse(Console.ReadLine(), out int rok);

// TryParse wstawiony wprost na ReadLine — bez zmiennej po drodze.
// Dzięki temu nie ma ostrzeżenia CS8600.
Console.WriteLine($"Czy udało się odczytać liczbę: {ok}");
Console.WriteLine($"Odczytana wartość: {rok}");

// Gdy odczyt się nie powiedzie, `ok` jest False, a `rok` zostaje zerem.
// Zareagować na to nauczysz się w module 4 — dziś umiesz to wykryć.
