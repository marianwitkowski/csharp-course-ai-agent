// try, catch, finally — lekcja 11.1
//
// Uruchomienie:  dotnet run 32-try-catch.cs

// Bez try program by się przewrócił: "Unhandled exception".
try
{
    int liczba = int.Parse("abc");
    Console.WriteLine(liczba);        // NIE wykona się — po wyjątku reszta try jest pomijana
}
catch (FormatException e)
{
    Console.WriteLine("To nie jest liczba.");
    Console.WriteLine($"Szczegóły: {e.Message}");
}

Console.WriteLine("Program idzie dalej.");

// Kilka catch: wykonuje się DOKŁADNIE JEDEN, pierwszy pasujący.
// Kolejność od szczegółowego do ogólnego — odwrotna daje CS0160.
foreach (string wpis in new[] { "5", "abc", "0" })
{
    try
    {
        int liczba = int.Parse(wpis);
        Console.WriteLine($"100 / {liczba} = {100 / liczba}");
    }
    catch (FormatException)
    {
        Console.WriteLine($"'{wpis}': to nie liczba");
    }
    catch (DivideByZeroException)
    {
        Console.WriteLine($"'{wpis}': nie dzielę przez zero");
    }
}

// finally wykonuje się ZAWSZE — także po return.
Console.WriteLine(Test());

int Test()
{
    try
    {
        return 1;
    }
    finally
    {
        Console.WriteLine("finally mimo return");
    }
}

// ALE: do przewidywalnych sytuacji lepszy jest TryParse niż try/catch.
// Zły wpis użytkownika NIE jest sytuacją wyjątkową — zdarza się stale.
if (int.TryParse("abc", out int wynik)) Console.WriteLine(wynik);
else Console.WriteLine("TryParse: to nie liczba (bez wyjątku, bez kosztu)");
