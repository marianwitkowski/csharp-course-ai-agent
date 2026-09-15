// Lekcja 4.1 — ocena

Console.Write("Podaj ocenę: ");
string wpis = Console.ReadLine();

int ocena;
int.TryParse(wpis, out ocena);

if (ocena == 5)
{
    Console.WriteLine("Bardzo dobry");
}
else if (ocena == 4)
{
    Console.WriteLine("Dobry");
}
else if (ocena == 3)
{
    Console.WriteLine("Dostateczny");
}
else
{
    Console.WriteLine("Nie ma takiej oceny");
}
