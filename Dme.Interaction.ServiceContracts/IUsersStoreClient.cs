using Dme.Interaction.Models.Users;

namespace Dme.Interaction.ServiceContracts
{
    public interface IUsersStoreClient
	{
		Task<IReadOnlyCollection<User>> GetUsersAsync(CancellationToken cancellationToken = default);
	}
}