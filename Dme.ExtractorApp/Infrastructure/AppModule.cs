using Autofac;
using Dme.ExtractorApp.Helpers;
using Dme.ExtractorApp.Settings;
using Dme.Interaction.Infrastructure;
using Dme.Persistence;
using Dme.Persistence.Infrastructure;
using Dme.Services.UsersStoreClient.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Dme.ExtractorApp.Infrastructure;

internal class AppModule : Module
{
	private readonly AppSettings _settings;

	public AppModule(AppSettings settings) =>
		_settings = settings ?? 
		            throw new ArgumentNullException(nameof(settings));

	protected override void Load(ContainerBuilder builder)
	{
		InitDatabase();

		builder.Register(ctx => new UsersDbContext(BuildDbContext)).As<DbContext>().InstancePerLifetimeScope();
		builder.Register(ctx => new LoggerConfiguration()
			.MinimumLevel.Debug()
			.WriteTo.Console()
			.CreateLogger()).As<ILogger>();
		builder.RegisterModule(new InteractionModule());
		builder.RegisterModule(new PersistenceModule());
		builder.RegisterModule(new UsersStoreClientModule(_settings.UsersStoreClient));
	}

	private void InitDatabase()
	{
		DatabaseInitializer.EnsureDatabaseCreated();
		using var dbContext = new UsersDbContext(BuildDbContext);
		dbContext.Database.Migrate();
	}

	private void BuildDbContext(DbContextOptionsBuilder builder) =>
		builder.UseSqlServer(_settings.ConnectionString.Replace("{database}", DatabaseInitializer.DatabaseName));
}