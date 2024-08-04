using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

using TarkovTracker.Base.DependencyInjection;
using TarkovTracker.Logic.Services.Interfaces;

namespace TarkovTracker.Logic.Services
{
	/// <inheritdoc cref="IQueryService"/>
	[Service(typeof(IQueryService), Lifetime = ServiceLifetime.Transient)]
	public class QueryService : IQueryService
	{
		private readonly ILogger<QueryService> _logger;
		private readonly HttpClient _httpClient;
		private readonly ICacheService _cacheService;

		public QueryService(ILogger<QueryService> logger, IHttpClientFactory httpClientFactory, ICacheService cacheService)
		{
			_logger = logger;
			_httpClient = httpClientFactory.CreateClient();
			_cacheService = cacheService;
		}

		public string GetResult(string query)
		{
			if (string.IsNullOrEmpty(query))
			{
				_logger.LogInformation($"{nameof(GetResult)}: Attempted to get a query result, but no query was provided.");
				return string.Empty;
			}

			if (_cacheService.Exists(query))
			{
				var cachedQueryResult = _cacheService.Get<string>(query);
				_logger.LogInformation($"{nameof(GetResult)}: had the query result in cache, so returning the stored value:\n{cachedQueryResult}");
				return cachedQueryResult ?? string.Empty;
			}

			_logger.LogInformation($"{nameof(GetResult)}: No query found in cache, so instead running an actual query.");
			return GetResultFromQuery(query);
		}

		/// <summary>
		/// Runs a query to get the result, then stores that
		/// result in the cache for fast retrieval later.
		/// </summary>
		/// <param name="query">
		/// The query to get the result of.
		/// </param>
		private string GetResultFromQuery(string query)
		{
			var postQuery = new Dictionary<string, string>{ ["query"] = query };
			var httpResponse = Task.Run(() => _httpClient.PostAsJsonAsync("https://api.tarkov.dev/graphql", postQuery))
				.GetAwaiter()
				.GetResult();

			var responseContent = Task.Run(() => httpResponse.Content.ReadAsStringAsync())
				.GetAwaiter()
				.GetResult();

			_logger.LogInformation($"{nameof(GetResultFromQuery)}: Got content, attemping to store in cache for later:\n{responseContent}");
			if(!_cacheService.Set(query, responseContent))
			{
				_logger.LogWarning($"{nameof(GetResultFromQuery)}: Couldn't store response in cache.");
			}

			return responseContent;
		}
	}
}
