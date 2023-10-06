using System.Reflection;
using Autofac;
using Dme.ExtractorApp.Infrastructure;
using Dme.ExtractorApp.Settings;
using Dme.Interaction.Interactors;
using Newtonsoft.Json;

var programPath = Assembly.GetExecutingAssembly().Location;
var uri = new UriBuilder(programPath);
var path = Uri.UnescapeDataString(uri.Path);
var fullPath = Path.GetDirectoryName(path);
var appSettingsFile = await File.ReadAllTextAsync(Path.Combine(fullPath!, "appsettings.json"));
var settings = JsonConvert.DeserializeObject<AppSettings>(appSettingsFile);

var containerBuilder = new ContainerBuilder();
containerBuilder.RegisterModule(new AppModule(settings));
var container = containerBuilder.Build();

var interactor = container.Resolve<IUsersExtractionInteractor>();
await interactor.ExtractUsersAsync();

Console.WriteLine("Finished");
Console.ReadLine();