using Dme.Interaction.Models.Users;
using Dme.Interaction.RepositoryRequests;
using Dme.Persistence.Mappers;
using Dme.Persistence.Models.Models;
using Dme.Persistence.Pattern;
using Microsoft.EntityFrameworkCore;

namespace Dme.Persistence.Repositories;

internal class UsersRepository : Repository<UserEntity>, ICreateUsersRequest
{
	public UsersRepository(DbContext dbContext) : base(dbContext)
	{
	}

	void ICreateUsersRequest.Create(IReadOnlyCollection<User> users)
	{
		var userEntities = users.Select(user => user.ToPersistence()).ToList();
		Set.AttachRange(userEntities);
	}
}