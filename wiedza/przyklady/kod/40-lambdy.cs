// Lambdy i Func — lekcja 13.3
//
// Uruchomienie:  dotnet run 40-lambdy.cs

// Func trzyma DZIAŁANIE zamiast danych. Ostatni typ w nawiasach
// ostrych to zawsze wynik, wcześniejsze to parametry.
Func<int, int> podwoj = x => x * 2;
Func<int, int, int> dodaj = (a, b) => a + b;
Func<bool> zawsze = () => true;

// Action nic nie zwraca — to odpowiednik metody void.
Action<string> wypisz = t => Console.WriteLine($"[{t}]");

Console.WriteLine($"{podwoj(5)} {dodaj(2, 3)} {zawsze()}");
wypisz("cześć");

// Wersja z klamrami: return OBOWIĄZKOWY.
// W wersji jednolinijkowej return jest zakazany.
Func<int, string> opis = x =>
{
    if (x > 10)
    {
        return "duża";
    }

    return "mała";
};

Console.WriteLine(opis(20));

List<int> liczby = new List<int> { 5, 12, 3, 20, 8 };

// Własna metoda przyjmująca funkcję: nie wie, co zrobi z liczbą.
// Decyduje ten, kto ją wywołuje.
Console.WriteLine(Zastosuj(4, x => x * 2));
Console.WriteLine(Zastosuj(4, x => x + 100));

int Zastosuj(int wartosc, Func<int, int> operacja) => operacja(wartosc);

// To jest Where napisane od zera. Cała reszta LINQ to odroczone wykonanie
// i to, że działa na każdej kolekcji.
Console.WriteLine(string.Join(", ", Filtruj(liczby, x => x > 5)));

List<int> Filtruj(List<int> zrodlo, Func<int, bool> warunek)
{
    List<int> wynik = new List<int>();

    foreach (int x in zrodlo)
    {
        if (warunek(x))
        {
            wynik.Add(x);
        }
    }

    return wynik;
}

// Zwykła metoda zamiast lambdy — przekazywana PO NAZWIE, bez nawiasów.
// Nawiasy znaczyłyby "wywołaj i weź wynik".
Console.WriteLine(string.Join(", ", liczby.Where(Duza)));

bool Duza(int x) => x > 5;

// Lambda pamięta ZMIENNĄ, nie jej ówczesną wartość — a zapytanie wykonuje się
// dopiero przy odczycie. Stąd wynik dla progu 9, nie 4.
int prog = 4;
var ponad = liczby.Where(x => x > prog);
prog = 9;
Console.WriteLine($"próg zmieniony po napisaniu zapytania: {string.Join(", ", ponad)}");

// Słownik działań — tu Func naprawdę się opłaca.
var dzialania = new Dictionary<string, Func<double, double, double>>
{
    ["+"] = (a, b) => a + b,
    ["-"] = (a, b) => a - b,
    ["*"] = (a, b) => a * b
};

Console.WriteLine($"3 * 4 = {dzialania["*"](3, 4)}");
