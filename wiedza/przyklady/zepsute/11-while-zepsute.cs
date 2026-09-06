// ZEPSUTE — lekcja 5.1 (while)
// Objaw: program ma wypisać liczby od 1 do 10, a wypisuje jedenaście liczb i zaczyna od 0.
// Skompiluj, uruchom, policz linie, znajdź przyczynę, napraw — bez zmiany warunku `i <= 10`.
int i = 0;
while (i <= 10)
{
    Console.WriteLine(i);
    i = i + 1;
}

// --- Dla narzedzia/sprawdz-przyklady.sh: tak ten plik ma się zachowywać (odtwarzane w CI,
// --- liczby w zapisie z kropką, kody diagnostyk zamiast treści komunikatów). Dla ciebie: objaw jest w nagłówku.
// CI-wyjscie: 0
// CI-wyjscie: 1
// CI-wyjscie: 2
// CI-wyjscie: 3
// CI-wyjscie: 4
// CI-wyjscie: 5
// CI-wyjscie: 6
// CI-wyjscie: 7
// CI-wyjscie: 8
// CI-wyjscie: 9
// CI-wyjscie: 10
