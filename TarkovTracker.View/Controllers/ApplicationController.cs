using Microsoft.Extensions.Logging;

using TarkovTracker.Base.DependencyInjection;
using TarkovTracker.Domain.Models;
using TarkovTracker.Logic.Services.Interfaces;
using TarkovTracker.View.Controllers.Interfaces;

namespace TarkovTracker.View.Controllers
{
	/// <inheritdoc cref="IApplicationController" />
	[Service(typeof(IApplicationController))]
	public class ApplicationController : IApplicationController
	{
		private readonly ILogger<ApplicationController> _logger;
		private readonly IQueryService _queryService;
		private readonly ISearchService _searchService;

		public ApplicationController(ILogger<ApplicationController> logger, IQueryService queryService, ISearchService searchService)
		{
			_logger = logger;
			_queryService = queryService;
			_searchService = searchService;
		}

		public void Run()
		{
			// Run the application logic here
			_logger.LogInformation("Started the application.");

			// run a query
			//_queryService.GetResult(string.Empty);
			
			// run a search
			//var result = _searchService.GetSearchResult<Map>("interchange");
		}
	}
}
