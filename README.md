# DemoUsers

## Opis

Projekt sk³ada sie z 3 projektów: 
- demousers.client który jest aplikacj¹ Single page napisan¹ w REACT - która poprzez Vite jako proxy ³¹czy siê z frontendem.
- DemoUsers.Server który jest backendem ASP.NET Core Web API, który ³¹czy siê z "baz¹ danych".
- TestProject testy jednostkowe skromnej "logiki" i obs³ugi b³êdów przez handlery.

Aplikacja realizuje wszystkie podstawowe za³o¿enia

Dodatkowe komentarze dot decyzji implementacyjnych s¹ umieszczone w kodzie.

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
