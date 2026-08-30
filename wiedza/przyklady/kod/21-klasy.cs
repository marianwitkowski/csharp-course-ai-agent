// Klasy i obiekty — lekcja 8.1
//
// Uruchomienie:  dotnet run 21-klasy.cs
//
// Klasa musi stać NA KOŃCU pliku, po instrukcjach — tak jak enum z lekcji 2.2.

Kot mruczek = new Kot();
mruczek.Imie = "Mruczek";
mruczek.Wiek = 3;
Console.WriteLine($"{mruczek.Imie} ma {mruczek.Wiek} lat");

// Cały obiekt przekazany do metody jednym argumentem.
Opisz(mruczek);

// Pola startują z wartościami domyślnymi: 0 dla liczb, null dla tekstu.
Kot pusty = new Kot();
Console.WriteLine($"[{pusty.Imie}] [{pusty.Wiek}]");
Console.WriteLine(pusty.Imie == null);

// DWIE ZMIENNE, JEDEN OBIEKT — to klucze do mieszkania z lekcji 7.3.
Kot c = mruczek;
c.Imie = "Filemon";
Console.WriteLine(mruczek.Imie);        // Filemon!

// == na obiektach pyta "czy to TEN SAM obiekt", nie "czy mają to samo w środku".
Kot d = new Kot();
d.Imie = "Filemon";
Console.WriteLine(mruczek == c);        // True  — ten sam obiekt
Console.WriteLine(mruczek == d);        // False — dwa obiekty, ta sama treść

// Bez ToString() wypisuje się nazwa klasy. Naprawa w lekcji 8.2.
Console.WriteLine(mruczek);

// new Kot[3] to TRZY PUSTE MIEJSCA, nie trzy koty.
Kot[] koty = new Kot[3];
koty[0] = new Kot();
Console.WriteLine(koty[1] == null);     // True — koty[1].Imie dałoby NullReferenceException

void Opisz(Kot kot) => Console.WriteLine($"{kot.Imie}, {kot.Wiek} lat");

class Kot
{
    public string Imie;
    public int Wiek;
}
