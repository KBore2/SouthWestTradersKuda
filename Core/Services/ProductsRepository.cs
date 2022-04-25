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
  /// <summary>
  /// Provides operations to manage products
  /// </summary>
  public class ProductsRepository: Repository<Data.Products.Context.Product>, IProductsRepository
  {
    private readonly Data.Products.Context.ProductsDBContext _productsDBContext;

    /// <summary>
    /// Initialises the constructor
    /// </summary>
    /// <param name="productsDBContext"></param>
    public ProductsRepository(Data.Products.Context.ProductsDBContext productsDBContext): base(productsDBContext)
    {
      _productsDBContext = productsDBContext;
    } 
  }
}
