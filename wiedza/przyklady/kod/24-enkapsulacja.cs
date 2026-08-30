// Modyfikatory dostępu i enkapsulacja — lekcja 8.4
//
// Uruchomienie:  dotnet run 24-enkapsulacja.cs

Konto k = new Konto("Ala");
k.Wplac(100);
k.Wplac(-50);                            // odrzucone
Console.WriteLine(k.SprobujWyplacic(500));   // False — więcej niż saldo
Console.WriteLine(k.SprobujWyplacic(30));    // True
Console.WriteLine($"{k.Wlasciciel}: {k.Saldo}, operacji: {k.LiczbaOperacji}");
k.PokazHistorie();

// Czego NIE da się zrobić z zewnątrz:
//   k.saldo = 999;        CS0122 — pole prywatne
//   k.Saldo = 999;        brak set
//   k.historia.Clear();   CS0122 — lista prywatna
//   k.Wlasciciel = "Bo";  brak set (tylko konstruktor)
// Niezmiennik: saldo zawsze zgadza się z historią i nigdy nie jest ujemne.

class Konto
{
    // Domyślnie w klasie wszystko jest private — słowo można pominąć,
    // ale pisane wprost lepiej widać intencję.
    private decimal saldo;
    private List<string> historia = new List<string>();

    public string Wlasciciel { get; }        // ustawiane tylko w konstruktorze
    public decimal Saldo => saldo;           // tylko odczyt
    public int LiczbaOperacji => historia.Count;

    public Konto(string wlasciciel) => Wlasciciel = wlasciciel;

    public void Wplac(decimal kwota)
    {
        if (kwota <= 0)
        {
            Console.WriteLine("Kwota musi być dodatnia.");
            return;
        }
        saldo = saldo + kwota;
        Zapisz($"wpłata {kwota}");
    }

    // Konwencja Try z lekcji 6.3 i 7.3 — we własnej klasie.
    public bool SprobujWyplacic(decimal kwota)
    {
        if (kwota <= 0 || kwota > saldo) return false;
        saldo = saldo - kwota;
        Zapisz($"wypłata {kwota}");
        return true;
    }

    public void PokazHistorie()
    {
        foreach (string wpis in historia) Console.WriteLine($"  {wpis}");
    }

    // Metoda prywatna: pomocnik dla wnętrza klasy, nie usługa dla świata.
    private void Zapisz(string opis) => historia.Add(opis);
}
