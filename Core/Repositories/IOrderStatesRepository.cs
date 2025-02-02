﻿using Core.Infastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
  /// <summary>
  /// Provides operations to manage order state
  /// </summary>
  public interface IOrderStatesRepository: IRepository<Data.Products.Context.OrderState>
  {
    /// <summary>
    /// Retrieves values from the cache
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<Data.Products.Context.OrderState>> GetCachedOrderStates();

    /// <summary>
    /// Retrieves the specified order state
    /// </summary>
    /// <returns></returns>
    public Task<Data.Products.Context.OrderState> GetCachedOrderStatesByKey(int orderStateId);
  }
}
