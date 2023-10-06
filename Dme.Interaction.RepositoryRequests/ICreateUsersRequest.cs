using Dme.Interaction.Models.UsersExtraction;

namespace Dme.Interaction.RepositoryRequests
{
	public interface ICreateUsersRequest 
	{
		void Create(IReadOnlyCollection<User> users);
	}
}