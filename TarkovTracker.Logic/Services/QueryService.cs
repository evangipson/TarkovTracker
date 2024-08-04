using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Threading.Tasks;
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
                ["query"] = @"{
                    task(id: ""5b478eca86f7744642012254"")
                    {
                        id
                        name
                        wikiLink
                        trader {
                            name
                        }
                        objectives{
                           maps {
                               id
                               name
                           }
                           type
                           description
                        }
                    }
                }"
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
