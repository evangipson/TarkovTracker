using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

using TarkovTracker.Base.DependencyInjection;
using TarkovTracker.Logic.Builders.Interfaces;
using TarkovTracker.Logic.Services.Interfaces;

namespace TarkovTracker.Logic.Services
{
	/// <inheritdoc cref="IQueryService"/>
	[Service(typeof(IQueryService))]
	public class QueryService : IQueryService
	{
		private readonly ILogger<QueryService> _logger;
		private readonly HttpClient _httpClient;
		private readonly IQueryBuilder _queryBuilder;
		private readonly ICacheService _cacheService;

		public QueryService(ILogger<QueryService> logger, IHttpClientFactory httpClientFactory, IQueryBuilder queryBuilder, ICacheService cacheService)
		{
			_logger = logger;
			_httpClient = httpClientFactory.CreateClient();
			_queryBuilder = queryBuilder;
			_cacheService = cacheService;
		}

		public string GetResult(string query)
		{
			var fiveTaskIdsQuery = _queryBuilder.Create()
				.Add(["tasks(limit:5, offset:105)", "id"])
				.Build();

			if (_cacheService.Exists(fiveTaskIdsQuery))
			{
				var cachedQueryResult = _cacheService.Get<string>(fiveTaskIdsQuery);
				_logger.LogInformation($"{nameof(GetResult)}: had the query result in cache, so returning the stored value:");
				_logger.LogInformation($"{cachedQueryResult}");
				return cachedQueryResult ?? string.Empty;
			}

			_logger.LogInformation($"{nameof(GetResult)}: No query found in cache, so instead running an actual query.");
			RunQuery(fiveTaskIdsQuery);

			return string.Empty;
		}

		/// <summary>
		/// Runs a query to get the result, then stores that
		/// result in the cache for fast retrieval later.
		/// </summary>
		/// <param name="query">
		/// The query to get the result of.
		/// </param>
		private void RunQuery(string query)
		{
			var postQuery = new Dictionary<string, string>
			{
				["query"] = query
			};

			_logger.LogInformation($"{nameof(RunQuery)}: POSTing to tarkov API.");
			var httpResponse = Task.Run(() => _httpClient.PostAsJsonAsync("https://api.tarkov.dev/graphql", postQuery))
				.GetAwaiter()
				.GetResult();

			_logger.LogInformation($"{nameof(RunQuery)}: Posted and am now awaiting the content.");
			var responseContent = Task.Run(() => httpResponse.Content.ReadAsStringAsync())
				.GetAwaiter()
				.GetResult();

			_logger.LogInformation($"{nameof(RunQuery)}: Got the content, storing in cache for later:");
			_logger.LogInformation($"{responseContent}");
			_cacheService.Set(query, responseContent);
		}
	}
}
