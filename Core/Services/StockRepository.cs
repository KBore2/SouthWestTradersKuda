using Core.Infastructure;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
  public class StockRepository: Repository<Data.Products.Context.Stock>, IStockRepository
  {
    public StockRepository(Data.Products.Context.ProductsDBContext productsDBContext): base(productsDBContext)
    {        
    }
  }
}
