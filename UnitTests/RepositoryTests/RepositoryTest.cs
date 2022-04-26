using Core.Infastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Mocks;
using Xunit;

namespace UnitTests.RepositoryTests
{
  public class RepositoryTest
  {
    private readonly Data.Products.Context.ProductsDBContext _productsDBContext;
    private readonly ProductsDBMockContext _productsDBMockContext;

    public RepositoryTest()
    {
      _productsDBContext = new Data.Products.Context.ProductsDBContext();
      _productsDBMockContext = new ProductsDBMockContext();

    }


    [Fact]
    public void TestCreateDBOrders()
    {
      var dbContext = _productsDBMockContext.GetDbContext();
      var repo = new Repository<Data.Products.Context.Order>(dbContext);

      var preOrderCreationCount = dbContext.Orders.Count();

      var newOrder = new Data.Products.Context.Order
      {
        OrderId = 11,
        ProductId = 10,      
        Name = "My Order",
        Quantity = 45,
        CreatedDateUtc = DateTime.UtcNow,
        OrderStateId = 1
      };

      repo.Add(newOrder);
      repo.Save();

      var postOrderCreationCount = dbContext.Orders.Count();

      Assert.NotEqual(preOrderCreationCount, postOrderCreationCount);
      Assert.Equal(preOrderCreationCount + 1, postOrderCreationCount);

      var order = repo.GetByKey(newOrder.OrderId);

      Assert.NotNull(order);  
      Assert.Equal(newOrder.OrderId, order.OrderId);

      var orders = repo.Find(x => x.OrderId == newOrder.OrderId);

      Assert.NotNull(orders);
    }

    [Fact]
    public void TestUpdateDBOrders()
    {
      var dbContext = _productsDBMockContext.GetDbContext();
      var repo = new Repository<Data.Products.Context.Order>(dbContext);

      var existingOrder = dbContext.Orders.FirstOrDefault();
      Assert.NotNull(existingOrder);

      string updatedOrderName = "Order updated";
      int updateOrderQuantity = 15;

      Assert.NotEqual(updatedOrderName, existingOrder.Name);
      Assert.NotEqual(updateOrderQuantity, existingOrder.Quantity);

      existingOrder.Name = updatedOrderName;
      existingOrder.Quantity = updateOrderQuantity;     

      repo.Update(existingOrder);
      repo.Save();

      var updateOrder = repo.GetByKey(existingOrder.OrderId);

      Assert.Equal(updatedOrderName, updateOrder.Name);
      Assert.Equal(updateOrderQuantity, updateOrder.Quantity);

    }

    [Fact]
    public void TestDeleteDBOrders()
    {
      var dbContext = _productsDBMockContext.GetDbContext();
      var repo = new Repository<Data.Products.Context.Order>(dbContext);

      var existingOrder = dbContext.Orders.FirstOrDefault();
      Assert.NotNull(existingOrder);

      repo.Delete(existingOrder);
      repo.Save();

      var deletedOrder = repo.GetByKey(existingOrder.OrderId);

      Assert.True(deletedOrder is null);

    }


    [Fact]
    public void TestCreateDBProducts()
    {
      var dbContext = _productsDBMockContext.GetDbContext();
      var repo = new Repository<Data.Products.Context.Product>(dbContext);

      var preProductCreationCount = dbContext.Products.Count();

      var product = new Data.Products.Context.Product
      {
        ProductId = 10,
        Description = "Microservices",
        Name = "Microservices",
        Price = 45
      };

      repo.Add(product);
      repo.Save();

      var postProductCreationCount = dbContext.Products.Count();

      Assert.NotEqual(preProductCreationCount, postProductCreationCount);
      Assert.Equal(preProductCreationCount + 1, postProductCreationCount);
    }
  }
}
