// Zmienne i typy proste — lekcja 2.1
//
// Uruchomienie:  dotnet run 02-zmienne.cs

int wiek = 30;
double wzrost = 1.75;
string imie = "Anna";
bool pelnoletni = true;

Console.WriteLine(wiek);
Console.WriteLine(wzrost);
Console.WriteLine(imie);
Console.WriteLine(pelnoletni);

// `var` — typ wnioskowany z wartości po prawej stronie.
// To nadal jest typ ustalony na stałe, tylko nie trzeba go wypisywać.
var liczbaKotow = 2;
Console.WriteLine(liczbaKotow.GetType());   // System.Int32

// Zmiana wartości: bez powtarzania typu.
wiek = 31;
Console.WriteLine(wiek);

// Wartość domyślna — zmienna zadeklarowana bez wartości NIE da się odczytać.
// Poniższa linia nie skompiluje się (CS0165); odkomentuj i zobacz sam.
// int licznik;
// Console.WriteLine(licznik);
