namespace TarkovTracker.Logic.Services.Interfaces
{
	/// <summary>
	/// A service which will allow both getting and
	/// setting objects from and to the cache, to
	/// avoid repetitive long calls to other services.
	/// </summary>
	public interface ICacheService
	{
		/// <summary>
		/// Checks if <paramref name="cacheKey"/> exists
		/// in cache, then returns the result.
		/// </summary>
		/// <param name="cacheKey">
		/// The key to check.
		/// </param>
		/// <returns>
		/// <c>true</c> if <paramref name="cacheKey"/> exists
		/// in the cache, <c>false</c> otherwise.
		/// </returns>
		bool Exists(string cacheKey);

		/// <summary>
		/// Tries to get the <typeparamref name="CacheValueType"/>
		/// value from the cache, then returns that value if the
		/// attempt was successful.
		/// <para>
		/// Can return <c>null</c> if the cache retrieval was
		/// unsuccessful.
		/// </para>
		/// </summary>
		/// <typeparam name="CacheValueType">
		/// The type of value to get from the cache.
		/// </typeparam>
		/// <param name="cacheKey">
		/// The key of the stored cache value.
		/// </param>
		/// <returns>
		/// The cached value if cached value was successfully
		/// retrieved, <c>null</c> otherwise.
		/// </returns>
		CacheValueType? Get<CacheValueType>(string cacheKey);

		/// <summary>
		/// Tries to set a value in the cache, and returns the
		/// success of that attempt.
		/// </summary>
		/// <typeparam name="CacheValueType">
		/// The type of value to store in the cache.
		/// </typeparam>
		/// <param name="cacheKey">
		/// The key of the stored cache value.
		/// </param>
		/// <param name="cacheValue">
		/// The value to store in the cache.
		/// </param>
		/// <param name="expirationDate">
		/// An optional expiration date for the cached value.
		/// Defaults to 5 minutes.
		/// </param>
		/// <returns>
		/// <c>true</c> if the cache was successfully set,
		/// <c>false</c> otherwise.
		/// </returns>
		bool Set<CacheValueType>(string cacheKey, CacheValueType cacheValue, DateTime expirationDate = default);
	}
}
