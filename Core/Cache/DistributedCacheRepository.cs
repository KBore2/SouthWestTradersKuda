using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Cache
{
  /// <summary>
  /// Provides operations to manage a distributed remote cache
  /// </summary>
  public class DistributedCacheRepository : IDistributedCacheRepository
  {
    private readonly IDistributedCache _distributedCache;  

    /// <summary>
    /// Initialise the constructor
    /// </summary>
    public DistributedCacheRepository(IDistributedCache distributedCache)
    {
      _distributedCache = distributedCache;     
    }

    /// <summary>
    /// Retrieves a value from the cache based on the key
    /// </summary>
    /// <param name="cacheKey"></param>
    /// <returns></returns>
    public async Task<string> GetAsync(string cacheKey)
    {
      try
      {
        var cachedValue = await _distributedCache.GetStringAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedValue))
        {
          return cachedValue;
        }

        return string.Empty;

      }
      catch (Exception)
      {
        return string.Empty;
      }
    }

    /// <summary>
    /// Purges the contents of the cache value
    /// </summary>
    /// <param name="cacheKey"></param>
    /// <returns></returns>
    public async Task PurgeAsync(string cacheKey)
    {
      try
      {
        await _distributedCache.RemoveAsync(cacheKey);
      }
      catch (Exception)
      {
        // do nothing
      }
    }

    /// <summary>
    /// Persists the value to the cache and sets the cache options
    /// </summary>
    /// <param name="cacheKey"></param>
    /// <param name="cacheValue"></param>
    /// <param name="cacheEntryOptions"></param>
    /// <returns></returns>
    public async Task SetAsync(string cacheKey, string cacheValue, DistributedCacheEntryOptions cacheEntryOptions)
    {
      try
      {
        await _distributedCache.SetStringAsync(cacheKey, cacheValue, cacheEntryOptions);
      }
      catch (Exception)
      {
        // do nothing
      }
    }
  }
}
