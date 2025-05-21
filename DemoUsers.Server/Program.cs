/*  Aplikacja stara si� i�� w kierunku "vertiacl slices" czego przyk�adem jest
 *  folder Users, kt�ry zawiera funkcjonalno�� zwi�zan� z u�ytkownikami.
 *  Potem mo�na to sobie wyeksportowa� do osobnej dllki.
 *  Raczej wychodz� z za�o�enia �e potencjalna duplikacja kodu jest mniejszym zmartwieniem 
 *  gdy trzeba wydzieli� cross cutting concerns. Ani�eli gdy powstaj� poziome warstwy, 
 *  gdzie ka�dy ficzer jest rozsmarowany po ca�ej solucji.
 *  
 *  DDD w ka�dym slice jest g��wnie definiowane przez zale�no�ci. Je�eli w Dtos
 *  nie ma �adnych using�w to znaczy, �e trzeba si� 15 razy zastanowi� zanim si� co� takiego wprowadzi.
 *  Je�eli serwisy nie dotykaj� do EntityFramework to tak samo nowe serwisy te� nie powinny.
 *  �adnie to wida� na Data gdzie Repozytorium korzysta z Dtos i Entity jako ta warstwa po�rednicz�ca,
 *  ale z poziomu interfejsu nie ma mowy o �adnych SqlParameters czy innych dziwactw.
 *  
 *  Wszystkimi technologiami mo�na zarz�dza� w formie scentralizowanego rejestru, 
 *  w celu unikni�cia problem�w z ich wersjonowaniem i zarz�dzaniem nimi.
 *  https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management
 *  tutaj na razie nie implementowa�em.
 *  
 *  Ka�da technologia jest obudowana w �adny interfejs,
 *  poniewa� technologi� trzeba m�c �atwo zmienia�.
 *  
 *  Obecnie logika aplikacji jest na tyle skromna, �e nie ma sensu wprowadzania
 *  dodatkowebo bloatwareu interfejsami dla kontroler�w serwis�w etc.
 *  
 *  Do mapowania encji na DTO u�ywam AutoMappera, bo jest najprostszy w u�yciu.
 *  Mapy rejestruje si� raz i u�ywam wsz�dzie.
 *  
 *  ILogger na razie standardowy, technologie jak Serilog, NLog, maj� odpowiednie implementacje
 *  tego interfejsu, wi�c mo�na je �atwo podmieni�.
 */

using System.Reflection;
using DemoUsers.Server.Users;
using Microsoft.AspNetCore.Mvc;

namespace DemoUsers.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            // Czysty mediator, z lokalnymi handlerami
            // bez �adnych message queue topic�w etc apka jest za ma�a
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<Program>();
            });

            // Automapper rejestruje si� sam, je�eli chce si� stworzy� modu�owe wczytywanie z wielu projekt�w
            // To da si� zrobi� globalnie dostepne IAutomapperConfigurationExpression do kt�rego zostan� dodane profile
            // i zainicjalizowa� automapper po wczytaniu modu��w.
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Rejestracja "modu�u" z logik� dla funkcjonalno�ci u�ytkownik�w.
            // Sam modu� �atwo mo�na wyci�gn�� do osobnej dllki i wczyta� i zarejestrowa�
            // assembly resolverem
            builder.Services.AddUsersFeature();
            
            var app = builder.Build();

            app.UseDefaultFiles();
            app.MapStaticAssets();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.Services.AddExampleUsers();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();
            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}