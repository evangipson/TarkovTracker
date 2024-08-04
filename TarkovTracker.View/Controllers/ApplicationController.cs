using Microsoft.Extensions.Logging;

using TarkovTracker.Base.DependencyInjection;
using TarkovTracker.Logic.Builders.Interfaces;
using TarkovTracker.Logic.Services.Interfaces;
using TarkovTracker.View.Controllers.Interfaces;

namespace TarkovTracker.View.Controllers
{
	/// <inheritdoc cref="IApplicationController" />
	[Service(typeof(IApplicationController))]
	public class ApplicationController : IApplicationController
	{
		private readonly ILogger<ApplicationController> _logger;
		private readonly IQueryBuilder _queryBuilder;
		private readonly IQueryService _queryService;
		private readonly ISearchService _searchService;

		public ApplicationController(ILogger<ApplicationController> logger, IQueryBuilder queryBuilder, IQueryService queryService, ISearchService searchService)
		{
			_logger = logger;
			_queryBuilder = queryBuilder;
			_queryService = queryService;
			_searchService = searchService;
		}

		public void Run()
		{
			// Run the application logic here
			_logger.LogInformation("Started the application.");

			// build a query
			var fiveTaskIdsQuery = _queryBuilder.Create()
				.Add(["tasks(limit:5, offset:105)", "id"])
				.Build();

			// hit the API with a query to get data back
			_queryService.GetResult(fiveTaskIdsQuery);

			// run the same query, but use cache to retrieve it.
			_queryService.GetResult(fiveTaskIdsQuery);

			// run a search
			//var result = _searchService.GetSearchResult<Map>("interchange");
		}
	}
}
