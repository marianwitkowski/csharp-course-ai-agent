---
name: pomoc
description: Wyświetla uczniowi pogrupowaną listę dostępnych komend kursu C# — frazy, którymi może sterować agentem. Pokazuje kategorie start, lekcja, ćwiczenia, quizy, postęp, reset, środowisko. Użyj gdy uczeń mówi "lista komend", "pomoc", "help", "co mogę zrobić?", "jakie są komendy?", "nie pamiętam co mam wpisać".
---

# Cel

Pokazać uczniowi **szybką ściągawkę** dostępnych komend bez zmuszania go do otwierania README.

# Co wyświetlić

Wypisz w czacie poniższą listę. **Nie modyfikuj** kategorii ani ikon — uczeń może ją znać z pamięci.

```
📋 LISTA KOMEND KURSU C#

🚀 Start i kontynuacja
  • ucz mnie C#                → start kursu lub powitanie
  • kontynuujemy               → następna lekcja
  • pokaż program kursu        → zawartość kurs/program.md
  • zmień program kursu        → edycja celu/tempa

📚 W trakcie lekcji
  • nie rozumiem [konceptu]    → wraca do podstaw nowym kątem
  • daj mi przykład            → minimalny przykład kodu
  • co to znaczy [termin]?     → wyjaśnienie sokratejskie
  • powtórzmy tę lekcję        → od początku
  • powtórzmy [temat]          → krótka powtórka jednego konceptu

✏️  Ćwiczenia i review
  • daj mi zadanie             → ćwiczenie z bieżącej lekcji
  • daj mi więcej zadań        → dodatkowe ćwiczenia
  • sprawdź moje zadanie       → review kodu
  • skończyłem [rozgrzewkę/główne/gwiazdkę] → review konkretnego rozwiązania
  • nie działa mi              → pomoc w debugowaniu
  • nie chce się skompilować   → czytamy komunikat kompilatora
  • pokaż gwiazdkę             → odsłania zadanie ⚡

🎯 Quizy i powtórki
  • quiz                       → szybki (3 pytania)
  • quiz pełny                 → pełny (5-7 pytań)
  • quiz słabe                 → z tematów do powtórki

📊 Postęp
  • pokaż postępy              → podsumowanie student.json
  • gdzie skończyliśmy?        → bieżąca lekcja + ostatnia sesja
  • co mam do powtórki?        → lista do_powtorki
  • co umiem najlepiej?        → lista mocne_strony

🔄 Reset i backup
  • zresetuj kurs              → reset miękki (z backupem)
  • pełny reset kursu          → reset pełny (z backupem)
  • cofnij reset               → przywrócenie z archiwum
  • pokaż backupy              → lista postep/backups/ (kopie stanu) i postep/archiwum/ (archiwa po resecie)

🛠️  Środowisko i pomoc
  • sprawdź .NET               → weryfikacja `dotnet --version` (min. 10.0)
  • jak uruchomić kod?         → odsyła do JAK-PISAC-KOD.md
  • lista komend / pomoc       → ten widok

⚙️  Tryb pracy (dla autora kursu)
  • tryb autora                → włącz tryb modyfikacji curriculum
  • tryb student               → wróć do trybu nauki (domyślny)

💡 Nie musisz pamiętać dokładnych fraz — agent zrozumie też "wyczyść kurs",
   "co robiłam ostatnio", "zrób mi test" itp.

⚠️  Czego agent NIE zrobi: nie uruchomi twojego programu. `dotnet run` należy
   do ciebie — wynik wklejasz do czatu i wtedy rozmawiamy.
```

# Wariant skrócony

Jeśli uczeń poprosi o „krótką pomoc" / „tylko najważniejsze":

```
🎯 NAJWAŻNIEJSZE KOMENDY

  • kontynuujemy               → następna lekcja
  • daj mi zadanie             → nowe ćwiczenie
  • sprawdź moje zadanie       → review kodu
  • nie działa mi              → debugowanie
  • quiz                       → szybka powtórka
  • pokaż postępy              → twój stan
  • lista komend               → pełna lista
```

# Ściąga komend `dotnet` — na życzenie

Jeśli uczeń pyta „jakie są komendy .NET?" (a nie komendy kursu), pokaż to:

```
🔧 KOMENDY .NET, KTÓRE POZNASZ W TYM KURSIE

  dotnet run nazwa.cs        uruchom program              (lekcja 1.1)
  dotnet --version           sprawdź wersję SDK           (lekcja 1.1)
  dotnet build nazwa.cs      skompiluj bez uruchamiania   (lekcja 1.2)
  dotnet new console         załóż projekt                (lekcja 14.1)
  dotnet test                uruchom testy                (lekcja 14.4)
  dotnet publish             zbuduj program do rozdania   (lekcja 14.4)

Zadania uruchamiasz z katalogu kurs/zadania/. Każde ćwiczenie to jeden
plik .cs — żadnych projektów ani solucji aż do modułu 14.

Te komendy działają identycznie na macOS, Linuksie i Windows.
```

# Twarde zasady

- **Wypisuj listę w jednym bloku** — nie dziel na wiele wiadomości.
- **Nie wymyślaj nowych komend** poza listą. Uczeń pyta o coś, czego nie ma → „tego nie ma, ale możesz [...]".
- **Po pokazaniu listy** zadaj jedno pytanie: „Z czego dziś korzystamy?" — żeby nie zostać w trybie „wyświetlam pomoc i czekam".
- **Nie pokazuj listy** w środku trwającej lekcji bez wyraźnej prośby — wybija z rytmu.
- **Nie pokazuj komend `dotnet` z lekcji, których uczeń jeszcze nie miał**, jako rzeczy do użycia teraz. Ściąga wyżej podaje numery lekcji właśnie po to.
