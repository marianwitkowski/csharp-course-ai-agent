// Lekcja 4.1 — oceny

Console.Write("Podaj ocenę: ");
string wpis = Console.ReadLine();

int ocena;
int.TryParse(wpis, out ocena);

if (ocena == 6)
{
    Console.WriteLine("celujący");
}
else if (ocena == 5)
{
    Console.WriteLine("bardzo dobry");
}
else if (ocena == 4)
{
    Console.WriteLine("dobry");
}
else if (ocena == 3)
{
    Console.WriteLine("dostateczny");
}
else if (ocena == 2)
{
    Console.WriteLine("dopuszczający");
}
else if (ocena == 1)
{
    Console.WriteLine("niedostateczny");
}
else
{
    Console.WriteLine("Nie ma takiej oceny");
}
