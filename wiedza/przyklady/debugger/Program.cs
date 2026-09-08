// Lekcja 7.4 — program do debugowania. Dwa fragmenty w jednym pliku:
// pętla z lekcji 5.2 (krok 3.A) i zepsute metody z lekcji 7.1 (kroki 3.B i 3.C).
// Uruchomienie z tego katalogu: dotnet run

int suma = 0;
for (int i = 1; i <= 5; i++)
{
    suma = suma + i;
}
Console.WriteLine(suma);

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
