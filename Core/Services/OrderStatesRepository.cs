using Core.Cache;
using Core.Infastructure;
using Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
  /// <summary>
  /// Provides operations to manage order states
  /// </summary>
  public class OrderStatesRepository: Repository<Data.Products.Context.OrderState>, IOrderStatesRepository
  {
    private readonly IDistributedCacheRepository _distributedCacheRepository;
    private readonly string _cacheKey;
    private readonly int _absoluteExpiration;

    /// <summary>
    ///  Initialises the constructor
    /// </summary>
    /// <param name="productsDBContext"></param>
    /// <param name="distributedCacheRepository"></param>
    public OrderStatesRepository(Data.Products.Context.ProductsDBContext productsDBContext, IDistributedCacheRepository distributedCacheRepository) : base(productsDBContext)
    {
      _distributedCacheRepository = distributedCacheRepository;
      _cacheKey = "OrderStates";
      _absoluteExpiration = 5; // hours
    }

    /// <summary>
    /// Retrieves cached order states
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IEnumerable<Data.Products.Context.OrderState>> GetCachedOrderStates()
    {
      return await GetCache();
    }

    public async Task<IEnumerable<Data.Products.Context.OrderState>> GetCache()
    {
      var cachedOrderStates = await _distributedCacheRepository.GetAsync(_cacheKey);
      if (!string.IsNullOrEmpty(cachedOrderStates))
      {
        var orderStates = JsonConvert.DeserializeObject<List<Data.Products.Context.OrderState>>(cachedOrderStates);
        return orderStates;
      }
      else
      {
        var dbOrderStates = this.GetAll().AsQueryable();

        var jsonOrderStates = JsonConvert.SerializeObject(dbOrderStates);

        var options = new DistributedCacheEntryOptions()
                          .SetAbsoluteExpiration(TimeSpan.FromHours(_absoluteExpiration));

        await _distributedCacheRepository.SetAsync(_cacheKey, jsonOrderStates, options);

        return dbOrderStates;
      }
    }

    /// <summary>
    /// Retrieves the specified order state
    /// </summary>
    /// <param name="orderStateId"></param>
    /// <returns></returns>
    public async Task<Data.Products.Context.OrderState> GetCachedOrderStatesByKey(int orderStateId)
    {
      var orderState = await GetCache();
      return orderState.FirstOrDefault(x => x.OrderStateId == orderStateId);
    }
  }
}
