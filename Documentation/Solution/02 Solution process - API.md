# 02 Solution process - API

## API

För vår backend valde vi att göra ett .NET Core Api med olika calls via Controllers samt att vi använde oss av Entity Frameworks - Code First för vår databas, med vissa initialt seedade testdata.

Vi använde oss av Controllers, Models och Services (Repositoryies) men skippade DTO (Data Transfer Object). Vi tog beslut på att inte använda den då vi dels inte behövde dem samtidigt som huvudfokus för uppgiften låg på Azure och vi ville komma snabbare fram till just den delen i uppgiften. I vanliga fall hade vi haft med DTOs.



## Databas

Vi modellerade upp vår databas utefter vår databasdesign vi tog fram i samband med planeringen. Vår connectionstring lade vi i en appsettingsfil samt som tidigare nämnt samt att vi initialt seedade in lite data. Det första vi gjorde i vår API var att få igång databasen i Azure så vi hade den rullande. Vi kände att detta var ett viktigt moment att få till redan från start, så att vi inte satt och utvecklade hela vår backend för att i slutändan inte få vår databas i Azure att fungera. Vi valde SQL som databas i Azure.

### Connectionstring

Vår connectionstring hämtade vi i Azure efter vi skapat vår databas. Eftersom vi lade den i vår appsettings.json. Tanken var att sedan även i Azure Portal skapa en keyvault som höll vår connectionstring som kopplade ihop våra andra resurser som också låg i Azure Portal (Frontend, API, SQL).

### KeyVault

Key Vault var väldigt lurigt att få till och vi lyckades tyvärr inte riktigt helavägen eftersom det endast finns ej aktuell, svår eller bristfällig information  Men detta är inte den enda anledningen, utan tekniska problem ställde även till det för oss också så att vi i slutändan övergav idén i brist på tid till deadline av projektet. 

Till en början lyckades vi få till en lösning relativt snabbt genom att vi lyckades vi hitta ett youtubeklipp med en enkel lösning som var lätt att modifiera och implementera.

Följande länk hittas youtubeklippet: [[.Net Core] With Azure: Using Azure Key Vault to store Secrets](https://www.youtube.com/watch?v=yRf-doZMIBw)

Istället som för klippet, där implementationen av klassen han skapade låg i program.cs, använde vi den istället i vår SpaceParkContext där vår connectionstring till vår databas finns, samt att vi även försäkrade oss med en lokal fallback utifall vår secret slutade fungera av någon anledning:

![](https://github.com/PGBSNH19/spacepark-grupp-1-spacepark/blob/master/Documentation/Solution/img/keyvault_implementation.PNG)

Dock visade sig detta fungera endast för utvecklingsmiljö, så att så fort vi körde det genom våra pipelines och upp till vårt API App Service i Azure, fungerade det inte längre. Felmeddelande om servicen inte lyckades att Autentisera sig mot Key Vault. Något saknades. Efter mycket experimenterande lyckades vi få till det genom Connected Services Azure Key Vault och några fler nugets som installerades i samband. Genom att använda Connected Services, så lades det till nödvändiga  parametrar och kod i bland annat launchsettings.json & Program.cs.

För att inte merga in detta  direkt i master, då vi inte kunde testa skarpt i vår riktiga Backend  App i Azure, använde vi Build -> Publish to Azure, där vi genom konfigurationer skapade upp en temporär App Service. 

Nu fungerade det äntligen med externt för samtliga medlemmar. Men, så fort vi gjorde en merge av branchen till Master, och vår release pipeline laddade upp till vår riktiga Service App för vårt Backend, så slutade hela sidan att fungera. I Loggarna stod det att den inte klarade att starta imagen.

**<u>För att komma vidare samt att kursens fokus låg på Azure, tog hela gruppen ett beslut på att klistra in hela connectionstringen synligt i vår OnConfiguring metod.</u>** Nu fungerade det externt i vår Backend API i Azure att komma åt databasen.

![](https://github.com/PGBSNH19/spacepark-grupp-1-spacepark/blob/master/Documentation/Solution/img/connectionstring_real_implementation.PNG)



## Loggning

Vi började med att endast använda microsofts egna loggningsverktyg och logga till debugkonsollen, vilket fungerade helt ok och gjorde att vi kunde se i utvecklingsmiljön vad som händer vid varje anrop. I en skarpare version av en applikation lämnar det dock en del att önska. 

På grund av att inte alla hade tillgång till azure-kontot och på grund av tidsbrist så valde vi bort lösningen att använda Azure Application Insights. Istället valde vi att använda oss av serilog och logga direkt in i vår databas men även till debugkonsollen. Det kan ifrågasättas hur klokt detta är men följande anledningar stod bakom vårat val:

1. Vi loggar inget som är känsligt i och med denna applikation.
2. För att vi ville testa något nytt.

En nackdel dock med att logga till en databas är att det inte ger stöd för strukturerad logging, vilket hade varit fint att ha i en verklig applikation för att på ett enkelt sätt kunna processa och dra slutsatser från den data som loggas.

För att möjliggöra logging med serilog som vi tänkt behövdes 4 olika nuget-paket:

- Serilog
- Serilog.AspNetCore
- Serilog.Sinks.Debug
- Serilogs.Sink.MSSqlServer



De två första paketen används för att få in serilog i en standard ASP.Net Core applikation och de två andra är vad man kallar för "sinks". Ett sink är en benämning för vart man vill att outputen för ens logg ska hamna.

I koden sedan använda vi oss av dessa rader för implementera allt:

*Startup konfigurering*

![SerilogStartup](C:\Users\hampe\Repos\Skola\Molntjänster\Projekt\spacepark-grupp-1-spacepark\Documentation\Solution\img\SerilogStartup.PNG)

*Program konfigurering*

![SerilogProgram](C:\Users\hampe\Repos\Skola\Molntjänster\Projekt\spacepark-grupp-1-spacepark\Documentation\Solution\img\SerilogProgram.PNG)



Loggern injinceras sedan i repositorysarna via dependency injection. I våran lösning har vi valt att lägga ett repositorys grundfunktionalitet i en basklass som sedan alla andra repositorys ärver av.

*Exempel grundrepository*

![LoggerRepo](C:\Users\hampe\Repos\Skola\Molntjänster\Projekt\spacepark-grupp-1-spacepark\Documentation\Solution\img\LoggerRepo.PNG)



## Tester

Eftersom vi visste att tiden kunde bli knapp och vi samtidigt ville ha så mycket tid som möjligt för Azure Devops och  Azure Portal gjorde vi endast ett fåtal tester för att få med dem i vår pipeline. Vi använde oss av Xunit samt Mock(Moq nuget). Testerna skedde främst för våra Controllers där vi bland annat testade att vi fick tillbaka svarskod *Status200OK*, och objektet vi eftersökte.



## Utmaningar

Till en början gick det relativt långsamt eftersom vi var tvungna att få upp vår databas innan vi separerade och börja arbeta på våra egna issues. Vi hade lite även lite problem med vår connectionstring, vilket tog lite extra tid. Vid denna tid hade vi inte heller satt igång med vår Frontend och för att inte krocka och orsaka mergeconflicts blev resultatet vissa medlemmar stundvis fick avvakta i väntan på att olika issues (gjorda av andra teammedlemmar), skulle bli klara. En anledning är att vårt Backend API var såpass litet.

