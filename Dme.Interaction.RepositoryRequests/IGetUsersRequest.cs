using Dme.Interaction.Models.Users;
using Dme.Interaction.RepositoryRequests.Models;

namespace Dme.Interaction.RepositoryRequests;

public interface IGetUsersRequest
{
	IReadOnlyCollection<User> GetUsers(QueryOptions queryOptions, bool? orderByName = null, bool? orderByBirthDate = null);
}