using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Cache
{
  /// <summary>
  ///  Provides operations to manage a distributed remote cache
  /// </summary>
  public interface IDistributedCacheRepository
  {
    /// <summary>
    /// Retrieves a value from the cache based on the key
    /// </summary>
    /// <param name="cacheKey"></param>
    /// <returns></returns>
    Task<string> GetAsync(string cacheKey);

    /// <summary>
    /// Persists a value to the cache and sets the cache options
    /// </summary>
    /// <param name="cacheKey"></param>
    /// <param name="cacheValue"></param>
    /// <param name="cacheEntryOptions"></param>
    /// <returns></returns>
    Task SetAsync(string cacheKey, string cacheValue, DistributedCacheEntryOptions cacheEntryOptions);

    /// <summary>
    /// Purges the cache values based on the key
    /// </summary>
    /// <param name="cacheKey"></param>
    /// <returns></returns>
    Task PurgeAsync(string cacheKey);
  }
}
