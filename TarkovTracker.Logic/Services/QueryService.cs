using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using TarkovTracker.Base.DependencyInjection;
using TarkovTracker.Logic.Services.Interfaces;

namespace TarkovTracker.Logic.Services
{
    /// <inheritdoc cref="IQueryService"/>
    [Service(typeof(IQueryService))]
    public class QueryService : IQueryService
    {
        private readonly ILogger<QueryService> _logger;
        private readonly HttpClient _httpClient;

        public QueryService(ILogger<QueryService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public string GetResult(string query)
        {
            RunTestQuery();
            return string.Empty;
        }

        private void RunTestQuery()
        {
            _logger.LogInformation("I'm in RunTestQuery");
            var data = new Dictionary<string, string>
            {
                ["query"] = "{items(name: \"m855a1\") { id name shortName }}"
			};

            _logger.LogInformation("I'm about to POST to tarkov API.");
            var httpResponse = Task.Run(() => _httpClient.PostAsJsonAsync("https://api.tarkov.dev/graphql", data)).GetAwaiter().GetResult();
            
            _logger.LogInformation("I posted and am now awaiting the content.");
            var responseContent = Task.Run(() => httpResponse.Content.ReadAsStringAsync()).GetAwaiter().GetResult();

            _logger.LogInformation("I got the content:");
            _logger.LogInformation(responseContent);
        }
    }
}
