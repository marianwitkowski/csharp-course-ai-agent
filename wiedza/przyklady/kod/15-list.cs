// List<T> — lekcja 6.2
//
// Uruchomienie:  dotnet run 15-list.cs

List<string> zakupy = new List<string>();
zakupy.Add("chleb");
zakupy.Add("mleko");
zakupy.Add("ser");

// Lista ma Count, tablica ma Length. Obie bez nawiasów.
Console.WriteLine($"Pozycji: {zakupy.Count}");
Console.WriteLine(string.Join(", ", zakupy));

// Add działa — w tablicy dawało CS1061.
zakupy.Add("masło");
Console.WriteLine($"Po dołożeniu: {zakupy.Count}");

// Dwa sposoby usuwania: po wartości i po numerze.
zakupy.Remove("mleko");
zakupy.RemoveAt(0);
Console.WriteLine(string.Join(", ", zakupy));

// Remove zwraca bool — mówi, czy było co usuwać.
Console.WriteLine(zakupy.Remove("kawior"));

// IndexOf daje -1, gdy nie znajdzie. Zero jest prawidłowym indeksem!
Console.WriteLine(zakupy.Contains("ser"));
Console.WriteLine(zakupy.IndexOf("kawior"));

// NIE WOLNO zmieniać listy w trakcie foreach — InvalidOperationException.
// Zbieraj do drugiej listy:
List<int> liczby = [1, 2, 3];
List<int> podwojone = new List<int>();
foreach (int x in liczby) podwojone.Add(x * 2);
Console.WriteLine(string.Join(" ", podwojone));

liczby.Sort();
Console.WriteLine(string.Join(" ", liczby));
