namespace TarkovTracker.Logic.Services.Interfaces
{
	/// <summary>
	/// The service responsible for querying and returning data.
	/// </summary>
	public interface IQueryService
	{
		/// <summary>
		/// Returns the result of a query.
		/// </summary>
		/// <returns>
		/// The result of the query.
		/// </returns>
		string GetResult(string query);
	}
}
