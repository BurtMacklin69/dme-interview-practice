using Dme.Interaction.Models.Users;

namespace Dme.Interaction.RepositoryRequests;

public interface IGetUserRequest
{
	User? GetUser(int id);
}