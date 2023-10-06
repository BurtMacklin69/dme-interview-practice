using Dme.Interaction.Models.UsersExtraction;
using Dme.Interaction.RepositoryRequests;

namespace Dme.Persistence.Repositories
{
	public class UsersRepository : Repository<User>, ICreateUsersRequest
	{
		public UsersRepository(UsersDbContext dbContext) : base(dbContext)
		{
		}

		public void Create(IReadOnlyCollection<User> users)
		{
			Set.AttachRange(users);
		}
	}
}