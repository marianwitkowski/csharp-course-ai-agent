# Kontroler kursu C# w Codex

## Routing

- Gdy użytkownik chce uczyć się C#, kontynuować lekcję, zrobić quiz, dostać ćwiczenie, sprawdzić kod, zobaczyć postęp albo skonfigurować środowisko kursu, użyj projektowego agenta `csharp_tutor`.
- Jeśli w tej sesji istnieje już wątek `csharp_tutor`, przekaż mu kolejną wiadomość. W przeciwnym razie uruchom dokładnie jeden taki wątek.
- Poczekaj na odpowiedź tutora i przekaż użytkownikowi wyłącznie jego odpowiedź przeznaczoną dla ucznia, bez streszczania ani dopisywania własnej lekcji.
- Nie uruchamiaj równoległych agentów podczas kursu. Stan między sesjami przechowuje `postep/student.json`, nie pamięć kontrolera.
- Jeśli jesteś już agentem `csharp_tutor`, nie stosuj powyższego routingu ponownie: wykonaj zadanie tutora bez tworzenia dalszych agentów.

## Tryb autora

- Prośby o zmianę programu, materiałów w `wiedza/`, konfiguracji agenta, skilli albo dokumentacji repozytorium obsługuje główny Codex, nie `csharp_tutor`.
- Fraza `tryb autora` wymaga istniejącego dwuetapowego potwierdzenia opisanego w `.claude/agents/csharp-tutor.md`. Dopiero po pełnym potwierdzeniu wykonuj zmiany autorskie w głównym wątku.
- Bez aktywnego trybu autora zachowaj ograniczenia zapisu z trybu student.

## Źródła prawdy

- Zachowanie tutora: `.claude/agents/csharp-tutor.md`.
- Umiejętności kursu: `.claude/skills/*/SKILL.md`; pliki `.agents/skills/*/SKILL.md` są wyłącznie adapterami Codex.
- Struktura kursu: `wiedza/INDEX.md`.
- Stan ucznia: `postep/student.json`; zapis wyłącznie przez narzędzie `postep`.
