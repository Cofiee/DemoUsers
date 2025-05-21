/*  Aplikacja stara siê iœæ w kierunku "vertiacl slices" czego przyk³adem jest
 *  folder Users, który zawiera funkcjonalnoœæ zwi¹zan¹ z u¿ytkownikami.
 *  Potem mo¿na to sobie wyeksportowaæ do osobnej dllki.
 *  Raczej wychodzê z za³o¿enia ¿e potencjalna duplikacja kodu jest mniejszym zmartwieniem 
 *  gdy trzeba wydzieliæ cross cutting concerns. Ani¿eli gdy powstaj¹ poziome warstwy, 
 *  gdzie ka¿dy ficzer jest rozsmarowany po ca³ej solucji.
 *  
 *  DDD w ka¿dym slice jest g³ównie definiowane przez zale¿noœci. Je¿eli w Dtos
 *  nie ma ¿adnych usingów to znaczy, ¿e trzeba siê 15 razy zastanowiæ zanim siê coœ takiego wprowadzi.
 *  Je¿eli serwisy nie dotykaj¹ do EntityFramework to tak samo nowe serwisy te¿ nie powinny.
 *  £adnie to widaæ na Data gdzie Repozytorium korzysta z Dtos i Entity jako ta warstwa poœrednicz¹ca,
 *  ale z poziomu interfejsu nie ma mowy o ¿adnych SqlParameters czy innych dziwactw.
 *  
 *  Wszystkimi technologiami mo¿na zarz¹dzaæ w formie scentralizowanego rejestru, 
 *  w celu unikniêcia problemów z ich wersjonowaniem i zarz¹dzaniem nimi.
 *  https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management
 *  tutaj na razie nie implementowa³em.
 *  
 *  Ka¿da technologia jest obudowana w ³adny interfejs,
 *  poniewa¿ technologiê trzeba móc ³atwo zmieniaæ.
 *  
 *  Obecnie logika aplikacji jest na tyle skromna, ¿e nie ma sensu wprowadzania
 *  dodatkowebo bloatwareu interfejsami dla kontrolerów serwisów etc.
 *  
 *  Do mapowania encji na DTO u¿ywam AutoMappera, bo jest najprostszy w u¿yciu.
 *  Mapy rejestruje siê raz i u¿ywam wszêdzie.
 *  
 *  ILogger na razie standardowy, technologie jak Serilog, NLog, maj¹ odpowiednie implementacje
 *  tego interfejsu, wiêc mo¿na je ³atwo podmieniæ.
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
            // bez ¿adnych message queue topiców etc apka jest za ma³a
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<Program>();
            });

            // Automapper rejestruje siê sam, je¿eli chce siê stworzyæ modu³owe wczytywanie z wielu projektów
            // To da siê zrobiæ globalnie dostepne IAutomapperConfigurationExpression do którego zostan¹ dodane profile
            // i zainicjalizowaæ automapper po wczytaniu modu³ów.
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Rejestracja "modu³u" z logik¹ dla funkcjonalnoœci u¿ytkowników.
            // Sam modu³ ³atwo mo¿na wyci¹gn¹æ do osobnej dllki i wczytaæ i zarejestrowaæ
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