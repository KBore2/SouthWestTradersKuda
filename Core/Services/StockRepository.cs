using Core.Infastructure;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
  /// <summary>
  /// Provides operations to manage stocks
  /// </summary>
  public class StockRepository: Repository<Data.Products.Context.Stock>, IStockRepository
  {
    private readonly Data.Products.Context.ProductsDBContext _productsDBContext;

    /// <summary>
    /// Initialises the constructort
    /// </summary>
    /// <param name="productsDBContext"></param>
    public StockRepository(Data.Products.Context.ProductsDBContext productsDBContext): base(productsDBContext)
    {        
      _productsDBContext = productsDBContext;
    }

    /// <summary>
    /// Retrieves the available stock for a given product
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public Data.Products.Context.Stock GetAvailableStock(long productId)
    {
      var availableStock = _productsDBContext.Stocks.Where(x => x.ProductId == productId).FirstOrDefault();
      return availableStock;
    }
  }
}
