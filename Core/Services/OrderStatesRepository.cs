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
  /// Provides operations to manage order states
  /// </summary>
  public class OrderStatesRepository: Repository<Data.Products.Context.OrderState>, IOrderStatesRepository
  {
    /// <summary>
    /// Initialises the order states
    /// </summary>
    /// <param name="productsDBContext"></param>
    public OrderStatesRepository(Data.Products.Context.ProductsDBContext productsDBContext): base(productsDBContext)
    {
    }
  }
}
