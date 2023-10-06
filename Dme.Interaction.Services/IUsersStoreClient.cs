using Dme.Services.UsersStoreClient.Models;

namespace Dme.Services.UsersStoreClient
{
	public interface IUsersStoreClient
	{
		Task<IReadOnlyCollection<Result>> GetUsersAsync(CancellationToken cancellationToken = default);
	}
}