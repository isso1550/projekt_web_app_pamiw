Tematyka
Projektowany system ma służyć firmie kurierskiej:
Menadżer będzie mógł zarzšdzać pracownikami swojej placówki. Pracownicy poczty będš mogli
rejestrować paczki, ich nadawców i adresatów. Paczki będš automatycznie przypisywane kurierom
którzy po ich dowiezieniu będš mogli zgłosić jej dostarczenie w systemie. Dodatkowo kurierzy będš
posiadać przypisane do nich pojazdy transportowe którymi zarzšdzać będzie menadżer.


# Wymagania ogólne
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
 1. Zastosowanie biblioteki boostrap (obsługa urządzeń mobilnych)
 2. Wyświetlanie (przeglądanie) danych
 3. Filtrowanie danych (AJAX)
 4. Zastosowanie stronicowania (możliwe użycie gotowych kontrolek ajax)
 5. Widok dodania nowego rekordu
 6. Widok edycji rekordu
 7. Opcja usunięcia rekordu
 8. Opcja wgrania zdjęcia z możliwością przesłania pliku na server (np. SignalR)
 10.Wdrożenie nowoczesnego interfejsu użytkownikabazując na szablonie HTML 
