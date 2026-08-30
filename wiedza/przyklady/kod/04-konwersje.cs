// Konwersje typów — lekcja 2.3
//
// Uruchomienie:  dotnet run 04-konwersje.cs

// 1. Konwersja bez straty — int mieści się w double, C# robi to sam.
int liczbaCalkowita = 7;
double zPrzecinkiem = liczbaCalkowita;
Console.WriteLine(zPrzecinkiem);        // 7

// 2. Konwersja ze stratą — trzeba napisać wprost, bo coś ginie.
double cena = 19.99;
int cenaCalkowita = (int)cena;
Console.WriteLine(cenaCalkowita);       // 19 — obcina, NIE zaokrągla

Console.WriteLine(Math.Round(cena));    // 20 — do zaokrąglania jest Math.Round

// 3. Dzielenie całkowite — najczęstszy cichy błąd początkującego.
Console.WriteLine(7 / 2);               // 3   — dwie liczby całkowite
Console.WriteLine(7.0 / 2);             // 3,5 — jedna z nich ma przecinek

// 4. Tekst na liczbę — Parse rzuca wyjątkiem, gdy tekst nie jest liczbą.
string wpisane = "42";
int liczba = int.Parse(wpisane);
Console.WriteLine(liczba + 1);          // 43

// 5. TryParse — wersja, która nie wywala programu. To jest ta,
//    której będziesz używać przy danych od użytkownika.
string podejrzane = "czterdzieści dwa";
if (int.TryParse(podejrzane, out int wynik))
{
    Console.WriteLine($"Udało się: {wynik}");
}
else
{
    Console.WriteLine($"\"{podejrzane}\" to nie jest liczba.");
}

// 6. Liczba na tekst.
Console.WriteLine(liczba.ToString() + "0");   // 420 — sklejenie, nie dodawanie
