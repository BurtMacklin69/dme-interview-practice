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
		var logger = new LoggerConfiguration()
			.MinimumLevel.Debug()
			.WriteTo.Console()
			.CreateLogger();

		InitDatabase(logger);

		builder.Register(ctx => new UsersDbContext(BuildDbContext)).As<DbContext>().InstancePerLifetimeScope();
		builder.Register(ctx => logger).As<ILogger>().SingleInstance();
		builder.RegisterModule(new InteractionModule());
		builder.RegisterModule(new PersistenceModule());
		builder.RegisterModule(new UsersStoreClientModule(_settings.UsersStoreClient));
	}

	private void InitDatabase(ILogger logger)
	{
		logger.Debug("Ensuring LocalDB-database with name {database} created...", UsersDbContext.DatabaseName);
		DatabaseInitializer.EnsureDatabaseCreated(out var created);
		logger.Debug("Database {database} {created}", UsersDbContext.DatabaseName, created ? "created" : "already existed");

		using var dbContext = new UsersDbContext(BuildDbContext);
		logger.Debug("Ensuring database is synchronized with data model...");
		dbContext.Database.Migrate();
		logger.Debug("Database is synchronized with data model");
	}

	private void BuildDbContext(DbContextOptionsBuilder builder) =>
		builder.UseSqlServer(_settings.ConnectionString.Replace("{database}", UsersDbContext.DatabaseName));
}