using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.Logging;
using TarkovTracker.Base.DependencyInjection;
using TarkovTracker.Domain.Models;
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
            _httpClient = httpClientFactory.CreateClient("QueryClient");
        }
        public string GetResult(string query)
        {
            RunTestQuery();
            return string.Empty;
        }

        private async void RunTestQuery()
        {
            _logger.LogInformation("I'm in RunTestQuery");
            var data = JsonContent.Create(new {query = "{items(name: \"m855a1\") { id name shortName }}"});  

            _logger.LogInformation("I'm about to connect");
            //Http response message
            var httpResponse = await _httpClient.PostAsync("https://api.tarkov.dev/graphql", data);
            _logger.LogInformation("I'm looking for content");
            //Response content
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            _logger.LogInformation("I'm printing a response");
            //Print response
            _logger.LogInformation(responseContent);

        }

    }
}
