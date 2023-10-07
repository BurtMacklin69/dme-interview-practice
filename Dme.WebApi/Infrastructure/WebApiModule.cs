using Autofac;
using Dme.Persistence;
using Dme.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Dme.WebApi.Infrastructure;

internal class WebApiModule : Module
{
	private readonly string _connectionString;

	public WebApiModule(string connectionString) =>
		_connectionString = connectionString;

	protected override void Load(ContainerBuilder builder)
	{
		builder.Register(_ => new UsersDbContext(BuildDbContext)).As<DbContext>().InstancePerLifetimeScope();
		builder.Register(_ => new LoggerConfiguration()
			.MinimumLevel.Debug()
			.WriteTo.Console()
			.CreateLogger()).As<Serilog.ILogger>();
		builder.RegisterModule(new PersistenceModule());
	}

	private void BuildDbContext(DbContextOptionsBuilder builder) =>
		builder.UseSqlServer(_connectionString.Replace("{database}", UsersDbContext.DatabaseName));
}