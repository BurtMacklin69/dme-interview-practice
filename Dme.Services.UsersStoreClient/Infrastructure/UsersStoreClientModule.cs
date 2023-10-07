using Autofac;
using Dme.Interaction.ServiceContracts;
using Dme.Services.UsersStoreClient.Settings;
using RestSharp;
using Serilog;

namespace Dme.Services.UsersStoreClient.Infrastructure;

public class UsersStoreClientModule : Module
{
	private readonly UsersStoreClientSettings _settings;

	public UsersStoreClientModule(UsersStoreClientSettings settings)
	{
		_settings = settings ?? throw new ArgumentNullException(nameof(settings));
		if (string.IsNullOrWhiteSpace(settings.UsersStoreBaseUrl))
			throw new ArgumentException("Expecting users store base path in settings", nameof(settings));
	}	

	protected override void Load(ContainerBuilder builder)
	{
		builder.Register(ctx =>
		{
			var restClient = new RestClient(new RestClientOptions
			{
				BaseUrl = new Uri(_settings.UsersStoreBaseUrl)
			});
			return new UsersStoreClient(restClient, _settings.MaxRetryCount, ctx.Resolve<ILogger>());
		}).As<IUsersStoreClient>();
	}
}