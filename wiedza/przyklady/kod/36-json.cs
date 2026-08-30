#:property JsonSerializerIsReflectionEnabledByDefault=true
#:property PublishAot=false
// JSON — zapis obiektów do pliku — lekcja 12.3
//
// Uruchomienie:  dotnet run 36-json.cs
//
// PIERWSZA LINIA JEST OBOWIĄZKOWA. W aplikacji jednoplikowej serializacja JSON
// jest domyślnie wyłączona i bez tej dyrektywy program kończy się wyjątkiem:
// "Reflection-based serialization has been disabled for this application."
//
// Druga dyrektywa wycisza ostrzeżenia IL2026/IL3050 — kompilator sprawdza pliki
// jednoplikowe pod kątem kompilacji do samodzielnego pliku wykonywalnego,
// a JSON tego sposobu nie lubi. Przy `dotnet run` nie ma to znaczenia.

using System.Text.Json;
using System.Text.Encodings.Web;

Kot mruczek = new Kot { Imie = "Mruczek", Wiek = 3 };

// Serializacja: obiekt -> tekst.
Console.WriteLine(JsonSerializer.Serialize(mruczek));

// Deserializacja: tekst -> obiekt. Może się nie udać, stąd Kot? i ?.
Kot? odczytany = JsonSerializer.Deserialize<Kot>("{\"Imie\":\"Filemon\",\"Wiek\":5}");
Console.WriteLine($"{odczytany?.Imie} ma {odczytany?.Wiek} lat");

// WriteIndented — dla człowieka. UnsafeRelaxedJsonEscaping — żeby "Żółw"
// nie zapisało się jako \u017B\u00F3\u0142w. "Unsafe" dotyczy wklejania
// takiego tekstu wprost na stronę WWW, nie zapisu do pliku.
var opcje = new JsonSerializerOptions
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
};

Console.WriteLine(JsonSerializer.Serialize(new Kot { Imie = "Żółw", Wiek = 1 }, opcje));

// PUŁAPKA 1: serializowane są WŁAŚCIWOŚCI, nie pola. Klasa z samym polem
// daje pusty obiekt — bez błędu, bez ostrzeżenia.
Console.WriteLine($"klasa z polem:        {JsonSerializer.Serialize(new ZPolem())}");
Console.WriteLine($"klasa z właściwością: {JsonSerializer.Serialize(new ZWlasciwoscia())}");

// PUŁAPKA 2: dopasowanie nazw jest domyślnie wrażliwe na wielkość liter.
Kot? maly = JsonSerializer.Deserialize<Kot>("{\"imie\":\"Bo\"}");
Console.WriteLine($"bez opcji:  [{maly?.Imie}]");   // puste!

var luzne = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
Kot? ok = JsonSerializer.Deserialize<Kot>("{\"imie\":\"Bo\"}", luzne);
Console.WriteLine($"z opcjami:  [{ok?.Imie}]");

// Cała lista jednym wywołaniem — niezależnie od liczby właściwości.
List<Kot> koty = new List<Kot>
{
    new Kot { Imie = "Mruczek", Wiek = 3 },
    new Kot { Imie = "Filemon", Wiek = 5 }
};

File.WriteAllText("koty.json", JsonSerializer.Serialize(koty, opcje));

List<Kot>? wczytane = JsonSerializer.Deserialize<List<Kot>>(File.ReadAllText("koty.json"));
Console.WriteLine($"wczytano {wczytane?.Count}, pierwszy: {wczytane?[0].Imie}");

// Zagnieżdżony obiekt — kompozycja z lekcji 10.3 przekłada się na strukturę pliku.
Osoba ala = new Osoba { Imie = "Ala", Adres = new Adres { Ulica = "Kwiatowa", Miasto = "Kraków" } };
Console.WriteLine(JsonSerializer.Serialize(ala, opcje));

// Zepsuty JSON to wyjątek — łapany tak samo jak wszystko w module 11.
try
{
    JsonSerializer.Deserialize<Kot>("{to nie jest json}");
}
catch (JsonException e)
{
    Console.WriteLine($"nieprawidłowy JSON: {e.Message}");
}

class Kot
{
    public string Imie { get; set; } = "";
    public int Wiek { get; set; }
}

class ZPolem
{
    public string Imie = "Mruczek";
}

class ZWlasciwoscia
{
    public string Imie { get; set; } = "Mruczek";
}

class Adres
{
    public string Ulica { get; set; } = "";
    public string Miasto { get; set; } = "";
}

class Osoba
{
    public string Imie { get; set; } = "";
    public Adres Adres { get; set; } = new Adres();
}
