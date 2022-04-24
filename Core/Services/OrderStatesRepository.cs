using Core.Infastructure;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
  public class OrderStatesRepository: Repository<Data.Products.Context.OrderState>, IOrderStatesRepository
  {
    public OrderStatesRepository(Data.Products.Context.ProductsDBContext productsDBContext): base(productsDBContext)
    {
    }
  }
}
