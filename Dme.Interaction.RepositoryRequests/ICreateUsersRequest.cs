using Dme.Interaction.Models.Users;

namespace Dme.Interaction.RepositoryRequests;

public interface ICreateUsersRequest 
{
	void Create(IReadOnlyCollection<User> users);
}