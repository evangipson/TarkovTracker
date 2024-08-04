using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using TarkovTracker.Base.DependencyInjection;
using TarkovTracker.Logic.Services.Interfaces;

namespace TarkovTracker.Logic.Services
{
	/// <inheritdoc cref="ICacheService"/>
	[Service(typeof(ICacheService))]
	public class CacheService : ICacheService
	{
		private readonly ILogger<CacheService> _logger;
		private readonly IMemoryCache _memoryCache;
		private const int _defaultCacheExpirationInMinutes = 5;

		public CacheService(ILogger<CacheService> logger, IMemoryCache memoryCache)
		{
			_logger = logger;
			_memoryCache = memoryCache;
		}

		public bool Exists(string cacheKey) => _memoryCache.Get(cacheKey) != null;

		public CacheValueType? Get<CacheValueType>(string cacheKey)
		{
			_logger.LogInformation($"{nameof(Get)}: Attempting to retrieve cached value for the key \"{cacheKey}\".");

			try
			{
				if (_memoryCache.TryGetValue(cacheKey, out CacheValueType? cachedValue))
				{
					_logger.LogInformation($"{nameof(Get)}: Cached value retrieved successfully for the key \"{cacheKey}\".");
					return cachedValue;
				}
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, $"{nameof(Get)}: Encountered an error getting cached value for key \"{cacheKey}\".");
			}

			_logger.LogWarning($"{nameof(Get)}: Cached value was not retrieved successfully for the key \"{cacheKey}\".");
			return default;
		}

		public bool Set<CacheValueType>(string cacheKey, CacheValueType cacheValue, DateTime expirationDate = default)
		{
			_logger.LogInformation($"{nameof(Set)}: Attempting to set cached value with the key \"{cacheKey}\".");

			if (expirationDate <= DateTime.UtcNow)
			{
				_logger.LogInformation($"{nameof(Set)}: Expiration date was not provided or in the past, using the default cache expiration time of \"{_defaultCacheExpirationInMinutes}\" minutes.");
				expirationDate = DateTime.UtcNow.AddMinutes(_defaultCacheExpirationInMinutes);
			}

			CacheValueType? cacheSetValue = default;
			try
			{
				cacheSetValue = _memoryCache.Set(cacheKey, cacheValue, expirationDate);
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, $"{nameof(Set)}: Encountered an error setting cache for key \"{cacheKey}\".");
			}

			return cacheSetValue != null;
		}
	}
}
