using Autofac;
using Dme.ExtractorApp.Helpers;
using Dme.ExtractorApp.Infrastructure;
using Dme.ExtractorApp.Settings;
using Dme.Interaction.Interactors;
using Newtonsoft.Json;
using Serilog;

var fullPath = PathHelper.GetCurrentPath();
var appSettingsFile = await File.ReadAllTextAsync(Path.Combine(fullPath!, "appsettings.json"));
var settings = JsonConvert.DeserializeObject<AppSettings>(appSettingsFile);

var containerBuilder = new ContainerBuilder();
containerBuilder.RegisterModule(new AppModule(settings));
var container = containerBuilder.Build();

var logger = container.Resolve<ILogger>();

var interactor = container.Resolve<IUsersExtractionInteractor>();
await interactor.ExtractUsersAsync(settings.ExtractUsersCount);

logger.Information("{count} пользователей загружены в БД {database}",
	settings.ExtractUsersCount, DatabaseInitializer.DatabaseName);