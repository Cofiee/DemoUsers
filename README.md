# DemoUsers

## Opis

Projekt sk�ada sie z 3 projekt�w: 
- demousers.client kt�ry jest aplikacj� Single page napisan� w REACT - kt�ra poprzez Vite jako proxy ��czy si� z frontendem.
- DemoUsers.Server kt�ry jest backendem ASP.NET Core Web API, kt�ry ��czy si� z "baz� danych".
- TestProject testy jednostkowe skromnej "logiki" i obs�ugi b��d�w przez handlery.

Aplikacja realizuje wszystkie podstawowe za�o�enia

Dodatkowe komentarze dot decyzji implementacyjnych s� umieszczone w kodzie.

Wyja�nienie w sprawie braku uploadu obrazk�w base64
Podszed�bym do tego tak, �e awatar dodawany by�by przez modal w kt�rym by�y by dwa pola
jedno dla url drugie dla uploadu pliku (z limitami rozmiaru etc).
Gdy jedno zosta�oby wype�nione to drugie zosta�oby ustawione na read only.
Plik np .jpg zakodowa�bym w base64

Nast�pnie skorzysta�bym z strumienia do odbioru base64
https://learn.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-9.0#upload-large-files-with-streaming
i przes�a� bezpo�rednio do jakiego� blob storage i url do tego awatara zapisa� w encji u�ytkownika.
W przypadku url nast�pi�aby pr�ba pobrania pliku do blob storage i url do bloba zapisany w encji u�ytkownika.

Frontend po prostu dostaje url do obrazka i zaci�ga go z blob storage, zamiast z internetu.

## Uruchomienie aplikacji

## Wymagania wst�pne

- [Docker](https://docs.docker.com/get-docker/) zainstalowany na Twoim komputerze.

## Budowanie obrazu Dockera

Otw�rz terminal w katalogu g��wnym repozytorium (tam, gdzie znajduje si� Dockerfile) i uruchom:

`docker build -t demousers-server .`

- `-t` nadaje obrazowi nazw� `demousers-server`.
- `.` okre�la bie��cy katalog jako kontekst budowania.

## Uruchamianie kontenera

After building the image, run the container with:

`docker run -d -p 8080:8080 -p 8081:8081 --name demousers-server demousers-server`

- `-d` uruchamia kontener w trybie od��czonym (detached, czyli nie wy�wietli konsoli kontenera, opcjonalne).
- `-p 8080:8080 -p 8081:8081` mapuje porty 8080 i 8081 kontenera na porty komputera, je�eli s� zaj�te to wystarczy je zmieni�.
- `--name demousers-server` nadaje uruchomionemu kontenerowi nazw� (opcjonalne).
- `demousers-server` nazwa obrazu.

## Dost�p do aplikacji

- Aplikacja b�dzie dost�pna pod adresem [http://localhost:8080](http://localhost:8080).
