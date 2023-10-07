using Dme.Services.UsersStoreClient.Settings;

namespace Dme.ExtractorApp.Settings;

internal class AppSettings
{
	public string ConnectionString { get; set; }
	public UsersStoreClientSettings UsersStoreClient { get; set; }
	public int ExtractUsersCount { get; set; }
}