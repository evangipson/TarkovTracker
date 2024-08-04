using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Threading.Tasks;
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

		public QueryService(ILogger<QueryService> logger, IHttpClientFactory httpClientFactory, IQueryBuilder queryBuilder)
		{
			_logger = logger;
			_httpClient = httpClientFactory.CreateClient();
			_queryBuilder = queryBuilder;
		}

		public string GetResult(string query)
		{
			var fiveTaskIdsQuery = _queryBuilder.Create()
				.Add(["tasks(limit:5, offset:105)", "id"])
				.Build();

			_logger.LogInformation($"{nameof(GetResult)}: query built: {fiveTaskIdsQuery}");

			RunQuery(fiveTaskIdsQuery);

			return string.Empty;
		}

		private void RunQuery(string query)
		{
			var postQuery = new Dictionary<string, string>
			{
				["query"] = query
			};

			_logger.LogInformation("I'm about to POST to tarkov API.");
			var httpResponse = Task.Run(() => _httpClient.PostAsJsonAsync("https://api.tarkov.dev/graphql", postQuery))
				.GetAwaiter()
				.GetResult();

			_logger.LogInformation("I posted and am now awaiting the content.");
			var responseContent = Task.Run(() => httpResponse.Content.ReadAsStringAsync())
				.GetAwaiter()
				.GetResult();

			_logger.LogInformation("I got the content:");
			_logger.LogInformation(responseContent);
		}
	}
}
