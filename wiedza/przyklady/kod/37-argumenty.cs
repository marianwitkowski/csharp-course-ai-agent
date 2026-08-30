// Argumenty wiersza poleceń — lekcja 12.4
//
// Uruchomienie:  dotnet run 37-argumenty.cs -- zakupy.txt --liczby
//
// PODWÓJNY MYŚLNIK jest obowiązkowy: oddziela argumenty dla `dotnet run`
// od argumentów dla twojego programu. Bez niego args będzie puste.

// args jest dostępne bez deklarowania i NIGDY nie jest null —
// przy braku argumentów to pusta tablica.
Console.WriteLine($"argumentów: {args.Length}");

for (int i = 0; i < args.Length; i++)
{
    Console.WriteLine($"  [{i}] {args[i]}");
}

// Sprawdź długość, ZANIM sięgniesz po args[0] — inaczej IndexOutOfRangeException.
// Wypisanie sposobu użycia przy braku argumentów to standard każdego narzędzia.
if (args.Length == 0)
{
    Console.WriteLine("Użycie: dotnet run 37-argumenty.cs -- <plik> [--liczby]");
    return;
}

// Rozbieranie argumentów pętlą: przełącznik osobno, nazwa pliku osobno.
// Dzięki temu kolejność NIE MA znaczenia — tak działają prawdziwe narzędzia.
// Sam args[0] nie wystarczy: gdy użytkownik napisze `-- --liczby dane.txt`,
// pod args[0] siedzi przełącznik, a nie nazwa pliku.
string? sciezka = null;
bool tylkoLiczby = false;

foreach (string a in args)
{
    if (a == "--liczby")
    {
        tylkoLiczby = true;
    }
    else
    {
        sciezka = a;
    }
}

if (sciezka == null)
{
    Console.WriteLine("Nie podałeś nazwy pliku.");
    return;
}

if (!File.Exists(sciezka))
{
    Console.WriteLine($"Nie ma pliku: {sciezka}");
    return;
}

Console.WriteLine($"--- {Path.GetFileName(sciezka)}, tryb liczb: {tylkoLiczby}");

int licznik = 0;

foreach (string linia in File.ReadAllLines(sciezka))
{
    // out _ znaczy "wynik mnie nie interesuje, sprawdzam tylko czy się udało".
    if (tylkoLiczby && !int.TryParse(linia, out _))
    {
        continue;
    }

    licznik++;
    Console.WriteLine(linia);
}

Console.WriteLine($"--- pasujących linii: {licznik}");
