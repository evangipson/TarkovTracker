using TarkovTracker.Base.DependencyInjection;
using TarkovTracker.Logic.Services.Interfaces;

namespace TarkovTracker.Logic.Services
{
	/// <inheritdoc cref="IQueryService"/>
	[Service(typeof(IQueryService))]
	public class QueryService : IQueryService
	{
		public string GetResult(string query)
		{
			return string.Empty;
		}
	}
}
