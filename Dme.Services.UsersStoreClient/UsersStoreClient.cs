using Dme.Services.UsersStoreClient.Models;
using Polly;
using RestSharp;
using Serilog;
using System.Net;
using Dme.Interaction.Models.Users;
using Dme.Interaction.ServiceContracts;
using Dme.Services.UsersStoreClient.Models.Mappers;

namespace Dme.Services.UsersStoreClient;

internal class UsersStoreClient : IUsersStoreClient
{
	private readonly IRestClient _restClient;
	private readonly int _retryCount;
	private readonly ILogger _logger;

	private const string Seed = "qwe";

	public UsersStoreClient(IRestClient restClient, int? retryCount, ILogger logger)
	{
		_restClient = restClient ??
		              throw new ArgumentNullException(nameof(restClient));
		_retryCount = retryCount ?? 3;
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	public async Task<IReadOnlyCollection<User>> GetUsersAsync(int count,CancellationToken cancellationToken = default)
	{
		if (count == 0) throw new ArgumentException("Users count must be positive", nameof(count));

		var retryPolicy = Policy.Handle<HttpRequestException>(ex => ex.StatusCode == HttpStatusCode.TooManyRequests)
			.WaitAndRetryAsync(
				retryCount: _retryCount,
				sleepDurationProvider: attemptNumber => TimeSpan.FromSeconds(attemptNumber),
				onRetry: (_, sleepDuration, attemptNumber, _) =>
				{
					_logger.Debug("Too many requests. Retrying in {sleepDuration}. " +
					              "{attemptNumber} / {_retryCount}", sleepDuration, attemptNumber, _retryCount);
				});

		var captureResult = await retryPolicy.ExecuteAndCaptureAsync(async () =>
		{
			var request = new RestRequest("/api/");
			request.AddQueryParameter("seed", Seed);
			request.AddQueryParameter("page", 1);
			request.AddQueryParameter("results", count);
			request.AddQueryParameter("format", "json");
			var response = await _restClient.ExecuteAsync<StoreResponse>(request, cancellationToken: cancellationToken);
			return (int)response.StatusCode switch
			{
				< 300 => response.Data,
				(int)HttpStatusCode.BadRequest => throw new Exception("Bad request to users store"),
				_ => throw new HttpRequestException($"Request to users store got status code {response.StatusCode}")
			};
		});

		return captureResult.Outcome switch
		{
			OutcomeType.Successful when captureResult.Result?.Results == null => throw new Exception(
				"Unabled to download users from store: null reference", captureResult.FinalException),
			OutcomeType.Successful => captureResult.Result.Results.Select(user => user.ToInteraction()).ToList(),
			OutcomeType.Failure => throw new Exception("Unabled to download users from store", captureResult.FinalException),
			_ => throw new IndexOutOfRangeException($"{nameof(captureResult.Outcome)} is not supported")
		};
	}
}