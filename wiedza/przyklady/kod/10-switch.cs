// switch — instrukcja i wyrażenie — lekcja 4.3
//
// Uruchomienie:  dotnet run 10-switch.cs

int przycisk = 2;

// Postać INSTRUKCJI: case / break / default.
switch (przycisk)
{
    case 1:
        Console.WriteLine("Cola");
        break;
    case 2:
        Console.WriteLine("Woda");
        break;
    default:
        Console.WriteLine("Nie ma takiego przycisku");
        break;
}

// Kilka etykiet pod rząd = ta sama reakcja dla kilku wartości.
switch (przycisk)
{
    case 1:
    case 2:
        Console.WriteLine("Napój zimny");
        break;
    case 3:
        Console.WriteLine("Napój ciepły");
        break;
}

// Postać WYRAŻENIA: krócej, gdy gałęzie tylko wybierają wartość.
// Nazwa zmiennej PRZED słowem switch, => zamiast dwukropka, _ zamiast default.
string napoj = przycisk switch
{
    1 => "Cola",
    2 => "Woda",
    3 => "Sok",
    _ => "Nie ma takiego przycisku",
};
Console.WriteLine(napoj);

// switch + enum: to domknięcie lekcji 2.2. Literówka w nazwie wartości
// nie przejdzie przez kompilator — przy zwykłym tekście przeszłaby.
DzienTygodnia dzis = DzienTygodnia.Sobota;

string plan = dzis switch
{
    DzienTygodnia.Sobota => "Wolne",
    DzienTygodnia.Niedziela => "Wolne",
    _ => "Praca",
};
Console.WriteLine(plan);

enum DzienTygodnia
{
    Poniedzialek, Wtorek, Sroda, Czwartek, Piatek, Sobota, Niedziela,
}
