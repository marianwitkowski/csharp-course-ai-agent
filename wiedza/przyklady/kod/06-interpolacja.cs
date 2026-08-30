// Interpolacja i formatowanie — lekcja 3.1
//
// Uruchomienie:  dotnet run 06-interpolacja.cs

string imie = "Anna";
int wiek = 32;

// Dwa zapisy, ten sam wynik. Drugi jest krótszy i czyta się go po kolei.
Console.WriteLine("Cześć, " + imie + "! Masz " + wiek + " lat.");
Console.WriteLine($"Cześć, {imie}! Masz {wiek} lat.");

// Bez znaku $ klamry są zwykłymi znakami:
Console.WriteLine("Cześć, {imie}!");

// W klamrach może być całe wyrażenie, nie tylko nazwa zmiennej.
Console.WriteLine($"{2 + 2} koty");
Console.WriteLine($"Za rok będziesz mieć {wiek + 1} lat.");

// Dwukropek oddziela CO wypisać od TEGO, JAK to wypisać.
double cena = 19.5;
Console.WriteLine($"Cena: {cena}");        // 19,5
Console.WriteLine($"Cena: {cena:F2}");     // 19,50 — dokładnie dwa miejsca
Console.WriteLine($"Rabat: {0.23:P0}");    // 23%  — sam dopisuje procent

// Klamra jako zwykły znak — podwajamy ją, tak jak \" w lekcji 1.1.
Console.WriteLine($"Klamra: {{tak}}");

// Write nie przechodzi do nowej linii — przyda się przy pytaniach (lekcja 3.2).
Console.Write("Suma: ");
Console.Write(2 + 3);
Console.WriteLine();

// \n to nowa linia w środku tekstu; @ przed cudzysłowem wyłącza znaczenie ukośnika.
Console.WriteLine("Linia1\nLinia2");
Console.WriteLine("C:\\dane\\2026");
Console.WriteLine(@"C:\dane\2026");
