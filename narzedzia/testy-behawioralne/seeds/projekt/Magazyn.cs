using System.Text.Json;

class Magazyn
{
    private readonly string sciezka;

    public Magazyn(string sciezka)
    {
        this.sciezka = sciezka;
    }

    public List<Zadanie> Wczytaj()
    {
        if (!File.Exists(sciezka))
        {
            return new List<Zadanie>();
        }

        string json = File.ReadAllText(sciezka);
        return JsonSerializer.Deserialize<List<Zadanie>>(json) ?? new List<Zadanie>();
    }

    public void Zapisz(List<Zadanie> zadania)
    {
        var opcje = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(sciezka, JsonSerializer.Serialize(zadania, opcje));
    }
}
