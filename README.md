# Sprawozdanie z projektu z przedmiotu Metody Wytwarzania Oprogramowania

A tu jest [film](https://github.com/StanislawMalinski/mwo/blob/master/Film.mkv)

### Aplikacja
Backend jak i Frontend zostały wykonane przy pomocy technologi .NET komponenty te porozumiewając się poprzez protokół https. 

W celu uruchomienia projektu należy w odzielnych procesach uruchomić oba te komponenty. W tym celu należy użyć dwóch komend:

```
dotnet run --project WebApp/WebApp.csproj 
dotnet run --project Films/Films.csproj
```
Po wykonaniu tych czynności, pod adresem http://localhost:5118/films można korzystać z aplikacji filmów.

### Testy
Testy interfejsu zostały wykonane przy pomocy oprogramowania Selenium, w języku Java.

W celu uruchomienia testów należy uruchomić aplikacje tak jak zostało to sprecyzowane w sekcji **Aplikacja**, następnie wystarczy w głównym katalogu wpisać następujące polecenie:
```
mvn clean install test
```

### Pipe-line
Pipe-line zostały wykonany przy pomocy Github'owego narzędzia **Actions**. Jest on uruchamiany w przypadku tworzenia nowego **Pull Request'a** na gałąź master. W skład jego odpowiedzialności wchodzi kolejno
- Zbudowanie oraz uruchomienie Backendu
- Zbudowanie oraz uruchomienie Frontendu
- Uruchomienie testów interfejsu urzytkownika
- W przypadku błędu podczas kompilacji czy testów zostaje dodany nowy **bug** w aplikacji AzureDevOps.
- Zakończenie pracy Aplikacji
