using Dme.Services.UsersStoreClient.Models;
using Polly;
using RestSharp;
using Serilog;
using System.Net;
using AutoMapper;
using Dme.Interaction.Models.Users;
using Dme.Interaction.ServiceContracts;
using Dme.Services.UsersStoreClient.Models.Mappers;

namespace Dme.Services.UsersStoreClient
{

	internal class UsersStoreClient : IUsersStoreClient
	{
		private readonly IRestClient _restClient;
		private readonly int _retryCount;
		private readonly ILogger _logger;

		public UsersStoreClient(IRestClient restClient, int? retryCount, ILogger logger)
		{
			_restClient = restClient ??
						  throw new ArgumentNullException(nameof(restClient));
			_retryCount = retryCount ?? 3;
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<IReadOnlyCollection<User>> GetUsersAsync(CancellationToken cancellationToken = default)
		{
			var retryPolicy = Policy.Handle<HttpRequestException>(ex => ex.StatusCode == HttpStatusCode.TooManyRequests)
				.WaitAndRetryAsync(
				   retryCount: _retryCount,
				   sleepDurationProvider: attemptNumber => TimeSpan.FromSeconds(attemptNumber),
				   onRetry: (exception, sleepDuration, attemptNumber, context) =>
				   {
					   _logger.Debug("Too many requests. Retrying in {sleepDuration}. " +
						   "{attemptNumber} / {_retryCount}", sleepDuration, attemptNumber, _retryCount);
				   });

			var captureResult = await retryPolicy.ExecuteAndCaptureAsync(async () =>
			{
				var response = await _restClient.ExecuteAsync<StoreResponse>(new RestRequest("/"), cancellationToken: cancellationToken);
				return (int)response.StatusCode switch
				{
					< 300 => response.Data,
					(int)HttpStatusCode.BadRequest => throw new Exception("Bad request to users store"),
					_ => throw new HttpRequestException($"Request to users store got status code {response.StatusCode}")
				};
			});

			switch (captureResult.Outcome)
			{
				case OutcomeType.Successful when captureResult.Result?.Results == null:
					throw new Exception("Unabled to download users from store: null reference", captureResult.FinalException);
				case OutcomeType.Successful:
					return captureResult.Result.Results.Select(user => user.ToInteraction()).ToList();
				default:
					throw new Exception("Unabled to download users from store", captureResult.FinalException);
			}
		}
	}
}