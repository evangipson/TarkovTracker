namespace TarkovTracker.Logic.Services.Interfaces
{
	/// <summary>
	/// The service repsonsible for searching
	/// and providing results.
	/// </summary>
	public interface ISearchService
	{
		/// <summary>
		/// Searches based on <paramref name="query"/>, and
		/// provides the result.
		/// </summary>
		/// <typeparam name="ResultType">
		/// The type to search for.
		/// </typeparam>
		/// <param name="query">
		/// The query to filter the results.
		/// </param>
		/// <returns>
		/// A collection of <typeparamref name="ResultType"/>.
		/// Defaults to an empty <see cref="List{T}"/>.
		/// </returns>
		List<ResultType> GetSearchResult<ResultType>(string query);
	}
}
