// Ścieżki i katalogi — Path i Directory — lekcja 12.2
//
// Uruchomienie:  dotnet run 35-sciezki.cs
// Program TWORZY katalog "archiwum" w katalogu, z którego go uruchomisz.

// Path.Combine sam wstawia właściwy separator — na macOS/Linux "/",
// w Windows "\". Ręczne sklejanie tekstem działa tylko na jednym systemie.
string sciezka = Path.Combine("dane", "raporty", "styczen.txt");
Console.WriteLine(sciezka);

// Rozbieranie ścieżki na części.
Console.WriteLine($"nazwa pliku:     {Path.GetFileName(sciezka)}");
Console.WriteLine($"bez rozszerzenia:{Path.GetFileNameWithoutExtension(sciezka)}");
Console.WriteLine($"rozszerzenie:    {Path.GetExtension(sciezka)}");   // ".txt" — Z KROPKĄ
Console.WriteLine($"katalog:         {Path.GetDirectoryName(sciezka)}");

// Ścieżka względna liczy się od katalogu URUCHOMIENIA, nie od położenia pliku .cs.
Console.WriteLine($"jestem w: {Directory.GetCurrentDirectory()}");
Console.WriteLine($"pełna ścieżka: {Path.GetFullPath("notatka.txt")}");

// Tworzenie katalogu. CreateDirectory nie protestuje, gdy katalog już jest —
// dlatego nie trzeba go poprzedzać sprawdzeniem.
Directory.CreateDirectory(Path.Combine("archiwum", "2026"));
Console.WriteLine($"archiwum istnieje: {Directory.Exists("archiwum")}");

// Zapis do podkatalogu — ścieżka składana przez Path.Combine.
string plik = Path.Combine("archiwum", "2026", "notatka.txt");
File.WriteAllText(plik, "treść\n");

// Przeglądanie zawartości katalogu. GetFiles zwraca ŚCIEŻKI, nie same nazwy.
foreach (string p in Directory.GetFiles(Path.Combine("archiwum", "2026")))
{
    Console.WriteLine($"  plik: {p}  (nazwa: {Path.GetFileName(p)})");
}

foreach (string k in Directory.GetDirectories("archiwum"))
{
    Console.WriteLine($"  katalog: {k}");
}

// Filtr po rozszerzeniu — wzorzec, nie wyrażenie regularne.
string[] txt = Directory.GetFiles(Path.Combine("archiwum", "2026"), "*.txt");
Console.WriteLine($"plików .txt: {txt.Length}");

// Kopiowanie i usuwanie pojedynczego pliku.
File.Copy(plik, Path.Combine("archiwum", "kopia.txt"), overwrite: true);
File.Delete(Path.Combine("archiwum", "kopia.txt"));

// UWAGA: Directory.Delete z recursive: true kasuje katalog RAZEM z zawartością,
// bez pytania i bez kosza. Uruchomione na złej ścieżce — nic nie odzyskasz.
// Zostawione tu zakomentowane celowo.
// Directory.Delete("archiwum", recursive: true);

Console.WriteLine("gotowe — zajrzyj do katalogu archiwum/");
