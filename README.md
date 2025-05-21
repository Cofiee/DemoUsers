# DemoUsers

## Opis

Projekt sk³ada sie z 3 projektów: 
- demousers.client który jest aplikacj¹ Single page napisan¹ w REACT - która poprzez Vite jako proxy ³¹czy siê z frontendem.
- DemoUsers.Server który jest backendem ASP.NET Core Web API, który ³¹czy siê z "baz¹ danych".
- TestProject testy jednostkowe skromnej "logiki" i obs³ugi b³êdów przez handlery.

Aplikacja realizuje wszystkie podstawowe za³o¿enia

Dodatkowe komentarze dot decyzji implementacyjnych s¹ umieszczone w kodzie.

Wyjaœnienie w sprawie braku uploadu obrazków base64
Podszed³bym do tego tak, ¿e awatar dodawany by³by przez modal w którym by³y by dwa pola
jedno dla url drugie dla uploadu pliku (z limitami rozmiaru etc).
Gdy jedno zosta³oby wype³nione to drugie zosta³oby ustawione na read only.
Plik np .jpg zakodowa³bym w base64

Nastêpnie skorzysta³bym z strumienia do odbioru base64
https://learn.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-9.0#upload-large-files-with-streaming
i przes³a³ bezpoœrednio do jakiegoœ blob storage i url do tego awatara zapisa³ w encji u¿ytkownika.
W przypadku url nast¹pi³aby próba pobrania pliku do blob storage i url do bloba zapisany w encji u¿ytkownika.

Frontend po prostu dostaje url do obrazka i zaci¹ga go z blob storage, zamiast z internetu.

## Uruchomienie aplikacji

## Wymagania wstêpne

- [Docker](https://docs.docker.com/get-docker/) zainstalowany na Twoim komputerze.

## Budowanie obrazu Dockera

Otwórz terminal w katalogu g³ównym repozytorium (tam, gdzie znajduje siê Dockerfile) i uruchom:

`docker build -t demousers-server .`

- `-t` nadaje obrazowi nazwê `demousers-server`.
- `.` okreœla bie¿¹cy katalog jako kontekst budowania.

## Uruchamianie kontenera

After building the image, run the container with:

`docker run -d -p 8080:8080 -p 8081:8081 --name demousers-server demousers-server`

- `-d` uruchamia kontener w trybie od³¹czonym (detached, czyli nie wyœwietli konsoli kontenera, opcjonalne).
- `-p 8080:8080 -p 8081:8081` mapuje porty 8080 i 8081 kontenera na porty komputera, je¿eli s¹ zajête to wystarczy je zmieniæ.
- `--name demousers-server` nadaje uruchomionemu kontenerowi nazwê (opcjonalne).
- `demousers-server` nazwa obrazu.

## Dostêp do aplikacji

- Aplikacja bêdzie dostêpna pod adresem [http://localhost:8080](http://localhost:8080).
