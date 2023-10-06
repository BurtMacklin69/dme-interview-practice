using Autofac;
using Dme.ExtractorApp.Settings;
using Dme.Interaction.Infrastructure;
using Dme.Persistence.Infrastructure;
using Dme.Services.UsersStoreClient.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Extensions.Autofac.DependencyInjection;

namespace Dme.ExtractorApp.Infrastructure
{
	internal class AppModule : Module
	{
		private readonly AppSettings _settings;

		public AppModule(AppSettings settings) =>
			_settings = settings ?? 
			            throw new ArgumentNullException(nameof(settings));

		protected override void Load(ContainerBuilder builder)
		{
			builder.Register(ctx => new LoggerConfiguration().WriteTo.Console().CreateLogger()).As<ILogger>();
			builder.RegisterModule(new InteractionModule());
			builder.RegisterModule(new PersistenceModule(ctxBuilder => ctxBuilder.UseSqlServer(_settings.ConnectionString)));
			builder.RegisterModule(new UsersStoreClientModule(_settings.UsersStoreClient));
		}
	}
}
