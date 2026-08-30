// Pętla for — lekcja 5.2
//
// Uruchomienie:  dotnet run 12-for.cs

// Trzy części nagłówka rozdzielone ŚREDNIKAMI:
// inicjalizacja (raz) ; warunek (przed każdym obiegiem) ; krok (po każdym obiegu)
for (int i = 0; i < 3; i++)
{
    Console.WriteLine($"Pompka {i}");
}

// Licznik znika po pętli — poniższa linia by się nie skompilowała (CS0103):
// Console.WriteLine(i);

// Liczenie w dół: zmieniają się wszystkie trzy części naraz.
for (int i = 5; i > 0; i--)
{
    Console.Write(i + " ");
}
Console.WriteLine("Start!");

// Krok nie musi wynosić 1.
for (int i = 0; i <= 20; i += 5)
{
    Console.Write(i + " ");
}
Console.WriteLine();

// To samo sumowanie co w lekcji 5.1, tylko krócej.
// `suma` nadal PRZED pętlą.
int suma = 0;
for (int i = 1; i <= 100; i++)
{
    suma = suma + i;
}
Console.WriteLine($"Suma: {suma}");

// Pętla w pętli: wewnętrzna przebiega CAŁA dla każdego obiegu zewnętrznej.
// Liczniki muszą mieć różne nazwy (inaczej CS0136).
for (int wiersz = 1; wiersz <= 3; wiersz++)
{
    for (int kolumna = 1; kolumna <= 3; kolumna++)
    {
        Console.Write($"{wiersz}-{kolumna} ");
    }
    Console.WriteLine();
}
