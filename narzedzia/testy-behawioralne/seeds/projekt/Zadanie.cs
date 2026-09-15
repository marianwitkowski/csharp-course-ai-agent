class Zadanie
{
    public string Tresc { get; set; } = "";
    public bool Zrobione { get; set; }

    public string Opis()
    {
        string znacznik = Zrobione ? "x" : " ";
        return $"[{znacznik}] {Tresc}";
    }
}
