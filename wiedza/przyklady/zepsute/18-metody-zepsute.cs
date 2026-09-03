// ZEPSUTE — lekcja 7.1 (metody)
// Objaw: Sprawdz mówi BŁĄD dla Srednia(3, 4): jest 3, miało być 3,5. Suma działa.
// Program się kompiluje bez ostrzeżeń. Znajdź przyczynę, napraw tak, żeby oba sprawdzenia mówiły OK.
Sprawdz("Suma(3, 4)", Suma(3, 4), 7);
SprawdzDouble("Srednia(3, 4)", Srednia(3, 4), 3.5);

int Suma(int a, int b)
{
    return a + b;
}

double Srednia(int a, int b)
{
    return (a + b) / 2;
}

void Sprawdz(string opis, int wynik, int oczekiwane)
{
    if (wynik == oczekiwane)
    {
        Console.WriteLine($"OK      {opis} = {wynik}");
    }
    else
    {
        Console.WriteLine($"BŁĄD    {opis}: jest {wynik}, miało być {oczekiwane}");
    }
}

void SprawdzDouble(string opis, double wynik, double oczekiwane)
{
    if (wynik == oczekiwane)
    {
        Console.WriteLine($"OK      {opis} = {wynik}");
    }
    else
    {
        Console.WriteLine($"BŁĄD    {opis}: jest {wynik}, miało być {oczekiwane}");
    }
}
