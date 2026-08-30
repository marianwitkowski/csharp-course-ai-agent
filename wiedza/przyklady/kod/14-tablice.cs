// Tablice — lekcja 6.1
//
// Uruchomienie:  dotnet run 14-tablice.cs

string[] imiona = { "Ala", "Bo", "Cyd" };
Console.WriteLine($"{imiona[0]} / {imiona[1]} / {imiona[2]}");
Console.WriteLine($"Liczba elementów: {imiona.Length}");
// Ostatni indeks to Length - 1. imiona[3] dałoby IndexOutOfRangeException.

// Tablica utworzona przez `new` startuje wypełniona wartościami domyślnymi.
int[] oceny = new int[3];
Console.WriteLine($"{oceny[0]}, {oceny[1]}, {oceny[2]}");

oceny[0] = 5; oceny[1] = 4; oceny[2] = 3;
foreach (int ocena in oceny) Console.Write(ocena + " ");
Console.WriteLine();

// for, gdy potrzebny numer elementu
for (int i = 0; i < imiona.Length; i++) Console.WriteLine($"{i}: {imiona[i]}");

// Tablicę MOŻNA zmieniać, ale NIE MOŻNA jej powiększyć.
imiona[1] = "Basia";
Console.WriteLine(string.Join(", ", imiona));
// imiona.Add("Ewa");   // CS1061 — tablica nie ma Add. Od tego jest List (6.2).

// Wzorce, które wracają do końca kursu: suma i największy.
int[] wszystkie = { 5, 3, 4, 2, 5 };
int suma = 0;
foreach (int o in wszystkie) suma = suma + o;
Console.WriteLine($"Suma: {suma}, średnia: {(double)suma / wszystkie.Length:F2}");

int najwieksza = wszystkie[0];   // NIE od zera — przy ujemnych dałoby zły wynik
foreach (int o in wszystkie) if (o > najwieksza) najwieksza = o;
Console.WriteLine($"Największa: {najwieksza}");

// Indeks od końca i zakres (C# 8) — do rozpoznania, nie do zapamiętania.
Console.WriteLine(imiona[^1]);
Console.WriteLine(string.Join(",", imiona[0..2]));
