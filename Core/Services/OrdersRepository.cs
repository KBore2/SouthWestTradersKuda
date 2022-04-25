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
  /// Provides operations to manage orders
  /// </summary>
  public class OrdersRepository: Repository<Data.Products.Context.Order>, IOrdersRepository
  {
    /// <summary>
    /// Initialises the constructor
    /// </summary>
    /// <param name="productsDBContext"></param>
    public OrdersRepository(Data.Products.Context.ProductsDBContext productsDBContext): base(productsDBContext)
    {
    }
  }
}
