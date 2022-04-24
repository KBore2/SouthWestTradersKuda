using Core.Infastructure;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
  public class OrdersRepository: Repository<Data.Products.Context.Order>, IOrdersRepository
  {
    public OrdersRepository(Data.Products.Context.ProductsDBContext productsDBContext): base(productsDBContext)
    {
    }
  }
}
