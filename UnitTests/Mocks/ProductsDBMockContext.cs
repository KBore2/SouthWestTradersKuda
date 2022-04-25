
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Mocks
{
  public class ProductsDBMockContext : DbContextMockBase<Data.Products.Context.ProductsDBContext>
  {
    private readonly ProductsDbEntities _productsDbEntities;
    public ProductsDBMockContext()
    {
      _productsDbEntities = new ProductsDbEntities();
    }
    public override Data.Products.Context.ProductsDBContext GetDbContext()
    {
      var mockDb = InitializeDb();  
      PopulateEntities(mockDb);
      return mockDb;
    }

    public override void PopulateEntities(Data.Products.Context.ProductsDBContext productsDBContextMock)
    {
      productsDBContextMock.AddRange(_productsDbEntities.GetTestProducts());
      productsDBContextMock.AddRange(_productsDbEntities.GetTestOrders());
      productsDBContextMock.AddRange(_productsDbEntities.GetTestStock());
      productsDBContextMock.AddRange(_productsDbEntities.GetTestOrderStates());
    }

    private Data.Products.Context.ProductsDBContext InitializeDb()
    {
      var mockDb = new DbContextOptionsBuilder<Data.Products.Context.ProductsDBContext>()
       .UseInMemoryDatabase("ProductsMockDB")
       .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
       .Options;

      using var context = new Data.Products.Context.ProductsDBContext(mockDb);

    //  context.Database.EnsureDeleted();
      context.Database.EnsureCreated();

      return context;
    }
  }
}
