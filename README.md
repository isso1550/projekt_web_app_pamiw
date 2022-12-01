Tematyka
Projektowany system ma służyć firmie kurierskiej:
Menadżer będzie mógł zarzšdzać pracownikami swojej placówki. Pracownicy poczty będš mogli
rejestrować paczki, ich nadawców i adresatów. Paczki będš automatycznie przypisywane kurierom
którzy po ich dowiezieniu będš mogli zgłosić jej dostarczenie w systemie. Dodatkowo kurierzy będš
posiadać przypisane do nich pojazdy transportowe którymi zarzšdzać będzie menadżer.\
Etapy:\
1.[Wymagania ogólne](#wymagania-ogolne)\
2.[Aplikacja UI](#serwis-ui)
3.[Baza danych](#baza-danych)

# Wymagania ogolne
Instrukcja
1. Inicjalizacja:
   - zklonować repozytorium
   - wykonać zapisaną migrację -> dotnet ef database update
   - zainicjalizować baze danych (narazie nie posiadam końcówek do dodawania ról)
<pre>
  USE FirmaKurierska
  INSERT INTO Roles values ('Menadzer')
  INSERT INTO Roles values ('Kurier')
</pre>

2. Weryfikacja wymagań\
- Projekt powinien być przechowywany w repozytorium github
  
- Zastosowanie architektury cebulowej\
  -> Struktura kodu

- Powiązanie z interfejsem użytkownika odbywa się za pośrednictwem REST/SOAP.\
  -> Struktura kodu.\
  W kolejnych etapach projektu planuje poprawić niektóre elementy, żeby API bardziej przypominało oryginalną definicję REST.
- Stosowanie modeli domenowych (Domain-Driven Design).\
  -> Struktura kodu

- Logowanie błędów aplikacji przy pomocy dodatkowego frameworka\
  Logowanie przy pomocy <pre>
  public KurierzyController(ILogger<KurierzyController> logger)
        {
            _logger = logger;
        }
  </pre>
  Wywołać błąd można na przykład próbując zarejestrować użytkownika z nieistniejącym id roli (0 lub jakaś ogromna wartość 999).\
  Narazie informacje w przypadku błędu zwracane przez odpowiedź HTTP są niebezpiecznie szczegółowe. \
  Uzywałem tego do testowania, planuję poprawić to pod koniec projektu.
- Dodanie mechanizmu uwierzytelnienia i autoryzacji
- Obsługa systemu ról
- Identyfikacja użytkowników przy pomocy JSON Web Token\
  Te trzy podpunkty można sprawdzić jednocześnie.\
  Token JWT uzyskuje się przy użyciu /login - jako, że bazy danych nie ma w repozytorium proponuje dodać sobie menadżera (id roli 1 WAŻNE) przez /register.\
  Testowanie przez końcówkę /authorizeTest - bardzo prosta metoda, sprawdza czy wysłany token ma Claim odpowiadający roli id 1, zwraca status 


- Stworzenie testów jednostkowych jednego repozytorium z mockami bazy\
  Używam pakietu Bogus. Projekt znajduję się w KurierzyTests i wypełnia tabelkę Persons generowanymi danymi.

- Stworzenie serwisu agregujących kilka operacji (np: dodanie użytkownika do bazy i
wysłanie maila) \
  Funkcja domyślnie jest wyłączona (zmienna bool sendConfirmationEmail plik: KurierzyService). Wysyła maila potwierdzającego rejestrację użytkownika. Wykorzystałem serwis mailtrap.io
  
# Serwis UI
Instrukcja:
1. Inicjalizacja taka sama jak w przypadku wymagań ogólnych.
2. Weryfikacja wymagań:
- Zastosowanie biblioteki boostrap (obsługa urządzeń mobilnych)\
  Link do arkuszu w _Layout.cshtml. Użyłem serwisu getbootstrap.com\
- Wyświetlanie (przeglądanie) danych\
  Dział persons ze strony głównej.\
- Filtrowanie danych (AJAX)\
  Filtry znajdują się na dole strony głównej działu persons\
- Zastosowanie stronicowania (możliwe użycie gotowych kontrolek ajax)\
  Stronicowanie razem z filtrami\
- Widok dodania nowego rekordu\
  Po kliknięciu odpowieniej opcji na stronie głównej działu persons\
- Widok edycji rekordu\
  Po kliknięciu przycisku modify przy rekordzie na stronie głównej działu persons\
- Opcja usunięcia rekordu\
  Po kliknięciu przycisku delete przy rekordzie na stronie głównej działu persons i potwierdzeniu w przeglądarce\
- Opcja wgrania zdjęcia z możliwością przesłania pliku na server (np. SignalR)\
  Nie udało mi się tego zrobić\
- Wdrożenie nowoczesnego interfejsu użytkownikabazując na szablonie HTML \
  Szablon ze strony Bookswatch (szablon Quartz)
  
  # Baza danych
-  Inicjalizacja teraz możliwa przez strony aplikacji internetowej -> ważne jest żeby rola Menadżera miała id 1, bo właśnie na id 1 jest ustawiona autoryzacja w końcówkach API.
-  Dodałem zaległy mechanizm dodawania plików na serwer. System dostępny przez zakładkę Upload.
-  Relacje bazy danych są prezentowane na stronie /deliverers/index, gdzie trzecią kolumną jest lista paczek przypisanych do tego dostawcy (relacja 1-*).
   Ważne jest sposób jej wczytania -> index.cshtml.cs. Oczywiście jeśli nic się nie wyświetla to trzeba się upewnić, że dostawca rzeczywiście ma jakieś przypisane paczki.
- Strony roles, deliverers oraz packages są stronami wygenerowanymi przez Visual Studio i korzystają bezpośrednio z DBContext, aby oszczędzić trochę czasu. Oczywiście każdy realzowany przez stronę mechanizm mógłbym zastąpić klasą *DB, serwisem oraz kontrolerem w API, z którego korzystałyby żądania AJAX na danej stronie podobnie jak działa to w przypadku całego działu Persons.
- Użycie kluczy opisane w 1 etapie (np. dodawanie użytkownika z nieistniejącą rola -> klucz obcy)
- Z relacji 1-* korzystają tabele Deliverers oraz Packages => jeden dostawca ma wiele paczek
- Z relacji 1-1 korzystają tabele OfficeWorkers oraz Offices => jeden office ma jednego officeworker, czyli menadżera (uprościłem model firmy, żeby zaprezentować tę relację)
- Z relacj *-* korzystają tabele Deliverers oraz Vans => jeden dostawca ma wiele pojazdów, którymi może jeździć, a jeden pojazd ma wielu kierowców (dostawców, którzy są uprawnieni do prowadzenia go, a nie wielu kierowców jednocześnie)
