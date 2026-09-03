// ZEPSUTE — lekcja 12.1 (pliki tekstowe)
// Objaw: program ma zapisać trzy linie do pliku, a po uruchomieniu w pliku jest tylko ostatnia.
// Skompiluj, uruchom, otwórz `dziennik.txt`, znajdź przyczynę, napraw. Usuń plik przed kolejną próbą.
string[] wpisy = { "poniedziałek: 30 min", "wtorek: 45 min", "środa: 20 min" };

foreach (string wpis in wpisy)
{
    File.WriteAllText("dziennik.txt", wpis + "\n");
}

Console.WriteLine(File.ReadAllText("dziennik.txt"));
