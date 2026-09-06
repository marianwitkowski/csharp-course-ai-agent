// ZEPSUTE — lekcja 12.1 (pliki tekstowe)
// Objaw: program ma zapisać trzy linie do pliku, a po uruchomieniu w pliku jest tylko ostatnia.
// Skompiluj, uruchom, otwórz `dziennik.txt`, znajdź przyczynę, napraw. Usuń plik przed kolejną próbą.
string[] wpisy = { "poniedziałek: 30 min", "wtorek: 45 min", "środa: 20 min" };

foreach (string wpis in wpisy)
{
    File.WriteAllText("dziennik.txt", wpis + "\n");
}

Console.WriteLine(File.ReadAllText("dziennik.txt"));

// --- Dla narzedzia/sprawdz-przyklady.sh: tak ten plik ma się zachowywać (odtwarzane w CI,
// --- liczby w zapisie z kropką, kody diagnostyk zamiast treści komunikatów). Dla ciebie: objaw jest w nagłówku.
// CI-wyjscie: środa: 20 min
// CI-plik: dziennik.txt=środa: 20 min
