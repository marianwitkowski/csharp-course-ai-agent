var magazyn = new Magazyn("zadania.json");
List<Zadanie> zadania = magazyn.Wczytaj();

string komenda = args.Length > 0 ? args[0] : "lista";

if (komenda == "dodaj" && args.Length > 1)
{
    zadania.Add(new Zadanie { Tresc = args[1] });
    magazyn.Zapisz(zadania);
    Console.WriteLine($"Dodano: {args[1]}");
}
else if (komenda == "zrobione" && args.Length > 1)
{
    int numer;
    if (!int.TryParse(args[1], out numer) || numer < 1 || numer > zadania.Count)
    {
        Console.WriteLine("Nie ma zadania o takim numerze.");
        return;
    }

    zadania[numer - 1].Zrobione = true;
    magazyn.Zapisz(zadania);
    Console.WriteLine($"Zrobione: {zadania[numer - 1].Tresc}");
}
else
{
    if (zadania.Count == 0)
    {
        Console.WriteLine("Brak zadań. Dodaj pierwsze: dotnet run dodaj \"treść\"");
        return;
    }

    Console.WriteLine($"Zadania ({zadania.Count(z => !z.Zrobione)} do zrobienia):");
    for (int i = 0; i < zadania.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {zadania[i].Opis()}");
    }
}
