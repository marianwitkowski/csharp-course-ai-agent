// Metody — lekcja 7.1
//
// Uruchomienie:  dotnet run 18-metody.cs
//
// Zwróć uwagę: metody są NA DOLE, a wywołania na górze — i to działa.
// Kompilator widzi cały plik, zanim wykona pierwszą linię.

Powitaj("Ala");
Powitaj("Bo");

Console.WriteLine(Dodaj(2, 3));
Console.WriteLine(Bezpieczne(10, 0));

Linia();
Console.WriteLine(Srednia(5, 4, 3));
Linia();

for (int i = 1; i <= 4; i++) Opisz(i);

// void = nic nie oddaje. Coś robi.
void Powitaj(string imie)
{
    Console.WriteLine($"Cześć, {imie}! Miło cię widzieć.");
}

// int przed nazwą = tyle oddaje przez return.
int Dodaj(int a, int b)
{
    return a + b;
}

// Wczesne wyjście: zły przypadek załatwiony na początku.
int Bezpieczne(int a, int b)
{
    if (b == 0)
    {
        return 0;
    }
    return a / b;
}

void Linia() => Console.WriteLine("--------------------");

// 3.0, nie 3 — inaczej dzielenie całkowite z lekcji 2.3.
double Srednia(int a, int b, int c) => (a + b + c) / 3.0;

// Metoda zwracająca bool trafia wprost do if — to ta sama zasada co w 4.1.
bool CzyParzysta(int n) => n % 2 == 0;

void Opisz(int n)
{
    if (CzyParzysta(n)) Console.WriteLine($"{n} jest parzysta");
    else Console.WriteLine($"{n} jest nieparzysta");
}
