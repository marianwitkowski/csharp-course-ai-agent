// Operatory i wyrażenia — lekcja 2.4
//
// Uruchomienie:  dotnet run 05-operatory.cs

int a = 17;
int b = 5;

Console.WriteLine(a + b);    // 22
Console.WriteLine(a - b);    // 12
Console.WriteLine(a * b);    // 85
Console.WriteLine(a / b);    // 3  — dzielenie całkowite
Console.WriteLine(a % b);    // 2  — reszta z dzielenia

// Przypisania skrócone
int licznik = 10;
licznik += 5;    // to samo co licznik = licznik + 5
licznik -= 3;
licznik *= 2;
Console.WriteLine(licznik);  // 24

// Inkrementacja
int i = 0;
Console.WriteLine(i++);      // 0 — wypisuje, POTEM zwiększa
Console.WriteLine(i);        // 1
Console.WriteLine(++i);      // 2 — zwiększa, POTEM wypisuje

// Porównania dają bool
Console.WriteLine(a > b);    // True
Console.WriteLine(a == b);   // False
Console.WriteLine(a != b);   // True

// `+` na tekstach nie dodaje — dokleja.
string imie = "Anna";
string nazwisko = "Kowalska";
Console.WriteLine(imie + " " + nazwisko);

// Ta sama pułapka z liczbą: "30" + 5 to "305", nie 35.
string wiekTekst = "30";
Console.WriteLine(wiekTekst + 5);
