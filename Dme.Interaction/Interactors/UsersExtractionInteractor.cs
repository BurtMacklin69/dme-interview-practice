﻿using Dme.Interaction.RepositoryRequests;
using Dme.Interaction.ServiceContracts;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Dme.Interaction.Interactors;

public interface IUsersExtractionInteractor
{
	Task ExtractUsersAsync(int count, CancellationToken cancellationToken = default);
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

	public async Task ExtractUsersAsync(int count, CancellationToken cancellationToken = default)
	{
		if (count == 0) throw new ArgumentException("Users count must be positive", nameof(count));

		_logger.Debug("Extracting {count} users from remote users store...", count);

		var users = await _usersStoreClient.GetUsersAsync(count, cancellationToken);

		_logger.Debug("{count} users extracted from remote users store", users.Count);

		_logger.Debug("Saving users to local database...");
		_createUsersRequest.Create(users);
		await DbContext.SaveChangesAsync(cancellationToken);
		_logger.Debug("{count} users successfully persisted", users.Count);
	}
}