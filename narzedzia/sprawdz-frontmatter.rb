#!/usr/bin/env ruby
# Sprawdza nagłówki YAML (frontmatter) agenta, skilli i lekcji.
# Uruchomienie z katalogu głównego repozytorium:  ruby narzedzia/sprawdz-frontmatter.rb
# Kod wyjścia 0 = wszystko w porządku, 1 = lista usterek na wyjściu.
require "yaml"

PLIKI = Dir[".claude/agents/*.md"] + Dir[".claude/skills/*/SKILL.md"] + Dir["wiedza/lekcje/*.md"] + Dir[".agents/skills/*/SKILL.md"]
usterki = []
lekcje = Hash.new { |h, k| h[k] = [] }

PLIKI.sort.each do |plik|
  linie = File.readlines(plik, chomp: true, encoding: "UTF-8")
  unless linie[0] == "---" && (koniec = linie[1..].index("---"))
    usterki << "#{plik}: brak nagłówka YAML między dwiema liniami ---"
    next
  end
  begin
    dane = YAML.safe_load(linie[1..koniec].join("\n")) || {}
  rescue Psych::SyntaxError => e
    usterki << "#{plik}: niepoprawny YAML — #{e.message.sub(/^\(<unknown>\): /, '')}"
    next
  end

  if plik.start_with?(".claude/") || plik.start_with?(".agents/skills/")
    %w[name description].each { |k| usterki << "#{plik}: brak pola #{k}" unless dane[k] }
    if plik.start_with?(".agents/skills/")
      nazwa = File.basename(File.dirname(plik))
      zrodlo = ".claude/skills/#{nazwa}/SKILL.md"
      usterki << "#{plik}: brak kanonicznego skilla #{zrodlo}" unless File.exist?(zrodlo)
      usterki << "#{plik}: brak odwołania do #{zrodlo}" unless linie.join("\n").include?("../../../#{zrodlo}")
    end
  elsif !plik.end_with?("SZABLON-LEKCJI.md")
    %w[lekcja tytul modul czas_min zalozenia].each { |k| usterki << "#{plik}: brak pola #{k}" unless dane[k] }
    lekcje[dane["lekcja"].to_s] << plik
    unless plik =~ %r{/#{dane["lekcja"].to_s.split(".").map { |n| format("%02d", n.to_i) }.join(".")}-}
      usterki << "#{plik}: pole lekcja (#{dane["lekcja"]}) nie zgadza się z nazwą pliku"
    end
    przyklady = dane["przyklady"].to_s
    przyklady.scan(/[\w-]+\.cs/).each do |cs|
      usterki << "#{plik}: brak pliku wiedza/przyklady/kod/#{cs}" unless File.exist?("wiedza/przyklady/kod/#{cs}")
    end
  end
end

lekcje.each { |id, pliki| usterki << "numer lekcji #{id} użyty w: #{pliki.join(', ')}" if pliki.size > 1 }

{
  ".codex/agents/csharp-tutor.toml" => %w[name description developer_instructions],
  ".codex/config.toml" => %w[max_concurrent_threads_per_session]
}.each do |plik, pola|
  unless File.exist?(plik)
    usterki << "#{plik}: brak pliku"
    next
  end
  tresc = File.read(plik, encoding: "UTF-8")
  pola.each { |pole| usterki << "#{plik}: brak pola #{pole}" unless tresc.match?(/^#{pole}\s*=/) }
end

if usterki.empty?
  puts "OK: #{PLIKI.size} plików, #{lekcje.size} lekcji, nagłówki poprawne"
else
  puts usterki
  exit 1
end
