// ref, out i zasięg — lekcja 7.3
//
// Uruchomienie:  dotnet run 20-ref-out.cs

// LICZBA: metoda dostaje KOPIĘ. Zmiana w metodzie nie wychodzi na zewnątrz.
void Zwieksz(int x) { x = x + 1; }
int a = 5;
Zwieksz(a);
Console.WriteLine($"liczba po metodzie: {a}");        // nadal 5

// TABLICA: metoda dostaje wskazanie na TĘ SAMĄ tablicę. Zmiana zostaje.
void ZmienPierwszy(int[] t) { t[0] = 99; }
int[] liczby = { 1, 2, 3 };
ZmienPierwszy(liczby);
Console.WriteLine($"tablica po metodzie: {liczby[0]}");  // 99

// out: oddaj więcej niż jedną wartość. Metoda MUSI przypisać każdy out.
void PodzielZReszta(int x, int y, out int wynik, out int reszta)
{
    wynik = x / y;
    reszta = x % y;
}
PodzielZReszta(17, 5, out int calosc, out int r);
Console.WriteLine($"{calosc} reszty {r}");

// Własna metoda w konwencji Try — dokładnie jak int.TryParse z lekcji 2.3:
// przez return idzie bool "czy się udało", przez out sama wartość.
bool SprobujPodzielic(int x, int y, out int wynik)
{
    if (y == 0) { wynik = 0; return false; }   // out musi dostać wartość TU TEŻ
    wynik = x / y;
    return true;
}
if (SprobujPodzielic(10, 0, out int w)) Console.WriteLine($"Wynik: {w}");
else Console.WriteLine("Nie da się podzielić przez zero.");

// ref: daj metodzie dostęp do samej zmiennej. Używaj rzadko —
// z wywołania nie widać, że coś się zmieniło.
void ZwiekszNaprawde(ref int x) { x = x + 1; }
ZwiekszNaprawde(ref a);
Console.WriteLine($"po ref: {a}");   // teraz 6

// ZASIĘG: zmienna żyje do zamykającej klamry swojego bloku.
// Zadeklarowana w pętli powstaje NA NOWO w każdym obiegu.
for (int i = 0; i < 3; i++)
{
    int licznik = 0;
    licznik = licznik + 1;
    Console.Write(licznik + " ");     // 1 1 1, nie 1 2 3
}
Console.WriteLine();
