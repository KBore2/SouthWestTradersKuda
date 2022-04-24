using Core.Infastructure;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
  internal class ProductRepository: Repository<Data.Products.Context.Product>, IProductsRepository
  {
    public ProductRepository(Data.Products.Context.ProductsDBContext productsDBContext): base(productsDBContext)
    {
    }
  }
}
