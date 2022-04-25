using Core.Infastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
  /// <summary>
  /// Provides operations to manage stock
  /// </summary>
  public interface IStockRepository: IRepository<Data.Products.Context.Stock>
  {
    /// <summary>
    /// Retrieves the available stock
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public Data.Products.Context.Stock GetAvailableStock(long productId);
  }
}
