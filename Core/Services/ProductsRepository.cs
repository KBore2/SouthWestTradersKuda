using Core.Infastructure;
using Core.Repositories;
using Data.Products.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
  public class ProductsRepository: Repository<Data.Products.Context.Product>, IProductsRepository
  {
    private readonly Data.Products.Context.ProductsDBContext _productsDBContext;
    public ProductsRepository(Data.Products.Context.ProductsDBContext productsDBContext): base(productsDBContext)
    {
      _productsDBContext = productsDBContext;
    }

    //public IEnumerable<Product> GetAvailableStock(long productId)
    //{
    //  var stock = from p in _productsDBContext.Products
    //              join s in _productsDBContext.Stocks on p.ProductId equals s.ProductId
    //              where s.ProductId == productId
    //              select s;



    //}
  }
}
