using Dme.Interaction.Models.Users;
using Dme.Interaction.RepositoryRequests;
using Dme.Interaction.RepositoryRequests.Models;
using Dme.Persistence.Mappers;
using Dme.Persistence.Models.Models;
using Dme.Persistence.Pattern;
using Microsoft.EntityFrameworkCore;

namespace Dme.Persistence.Repositories;

internal class UsersRepository : Repository<UserEntity>,
	ICreateUsersRequest,
	IGetUserRequest, 
	IGetUsersRequest
{
	public UsersRepository(DbContext dbContext) : base(dbContext)
	{
	}

	void ICreateUsersRequest.Create(IReadOnlyCollection<User> users)
	{
		var userEntities = users.Select(user => user.ToPersistence()).ToList();
		Set.AttachRange(userEntities);
	}

	public User? GetUser(int id)
	{
		var entity = Read().SingleOrDefault(user => user.Id == id);
		return entity?.ToInteraction();
	}

	public IReadOnlyCollection<User> GetUsers(QueryOptions queryOptions,
		bool? orderByName = null,
		bool? orderByBirthDate = null) => Read(queryOptions, orderByName, orderByBirthDate).Select(user => user.ToInteraction()).ToList();

	private IQueryable<UserEntity> Read(QueryOptions? queryOptions = null,
		bool? orderByName = null,
		bool? orderByBirthDate = null)
	{
		queryOptions ??= new QueryOptions(null, null);

		var query = Set
			.Include(e => e.Documents)
			.Include(e => e.Name)
			.Include(e => e.Pictures)
			.AsQueryable();

		if (orderByName == true)
			return queryOptions.Apply(query, user => user.Name.Last, user => user.Name.First);

		if (orderByBirthDate == true)
			return queryOptions.Apply(query, user => user.BirthDate);

		return queryOptions.Apply(query, user => user.Id);
	}
}