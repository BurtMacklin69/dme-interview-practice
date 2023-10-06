using Dme.Interaction.RepositoryRequests;
using Dme.Interaction.ServiceContracts;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Dme.Interaction.Interactors
{
	public interface IUsersExtractionInteractor
	{
		Task ExtractUsersAsync(CancellationToken cancellationToken = default);
	}

	internal class UsersExtractionInteractor : Interactor, IUsersExtractionInteractor
	{
		private readonly IUsersStoreClient _usersStoreClient;
		private readonly ICreateUsersRequest _createUsersRequest;
		private readonly ILogger _logger;

		public UsersExtractionInteractor(IUsersStoreClient usersStoreClient, DbContext dbContext, ICreateUsersRequest createUsersRequest, ILogger logger) : base(dbContext)
		{
			_usersStoreClient = usersStoreClient ?? 
			                    throw new ArgumentNullException(nameof(usersStoreClient));
			_createUsersRequest = createUsersRequest ?? 
			                      throw new ArgumentNullException(nameof(createUsersRequest));
			_logger = logger ?? 
			          throw new ArgumentNullException(nameof(logger));
		}

		public async Task ExtractUsersAsync(CancellationToken cancellationToken = default)
		{
			var users = await _usersStoreClient.GetUsersAsync(cancellationToken);

			_logger.Debug("{count} users extracted from remote users store", users.Count);

			_createUsersRequest.Create(users);
			await DbContext.SaveChangesAsync(cancellationToken);

			_logger.Debug("{count} users persisted", users.Count);
		}
	}
}
