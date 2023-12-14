# Sprawozdanie z projektu z przedmiotu Metody Wytwarzania Oprogramowania

A tu jest [film](https://github.com/StanislawMalinski/mwo/blob/master/Film.mkv)

## Aplikacja

Backend jak i Frontend zostały wykonane przy pomocy technologi .NET komponenty te porozumiewając się poprzez protokół https. 

### Konfiguracja
Projekt wymaga programu .NET w wersji 8.

### Uruchomienie
W celu uruchomienia projektu należy w odzielnych procesach uruchomić oba te komponenty. W tym celu należy użyć dwóch komend:

```
dotnet run --project WebApp/WebApp.csproj 
dotnet run --project Films/Films.csproj
```
Po wykonaniu tych czynności, pod adresem http://localhost:5118/films można korzystać z aplikacji filmów.

## Testy
Testy interfejsu zostały wykonane przy pomocy oprogramowania Selenium, w języku Java.

### Konfiguracja
Dzięki narzędziu Maven odpowdnie wersje wszystkich narzędzi tej części projektu są automatycznie pobierane.

### Uruchomienie
W celu uruchomienia testów należy uruchomić aplikacje tak jak zostało to sprecyzowane w sekcji **Aplikacja**, następnie wystarczy w głównym katalogu wpisać następujące polecenie:
```
mvn clean install test
```

## Pipe-line
Pipe-line zostały wykonany przy pomocy Github'owego narzędzia **Actions**. Jest on uruchamiany w przypadku tworzenia nowego **Pull Request'a** na gałąź master. W skład jego odpowiedzialności wchodzi kolejno
- Zbudowanie oraz uruchomienie Backendu
- Zbudowanie oraz uruchomienie Frontendu
- Uruchomienie testów interfejsu urzytkownika
- W przypadku błędu podczas kompilacji czy testów zostaje dodany nowy **bug** w aplikacji AzureDevOps.
- Zakończenie pracy Aplikacji

### Konfiguracja
W celu stworzenia tej części projektu należało wykonać pare czynności:
1. Wygenerowanie tokenu dostępu do AzureDevOps.
2. Wygenerowanie tokenu dostępu do GitHub.
3. Stworzenie sekretów środowiskowych dla tokenów z kroku 1. oraz 2.


