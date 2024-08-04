using TarkovTracker.Base.DependencyInjection;
using TarkovTracker.Logic.Services.Interfaces;

namespace TarkovTracker.Logic.Services
{
	/// <inheritdoc cref="ISearchService"/>
	[Service(typeof(ISearchService))]
	public class SearchService : ISearchService
	{
		public List<ResultType> GetSearchResult<ResultType>(List<ResultType> results, Func<ResultType, bool> filter)
		{
            return results.Where(result => filter(result)).ToList();
		}

	}
}
