using Core.Infastructure;
using Core.Repositories;
using Core.Transactions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Mocks
{
  public class RepositoryMocks<TEntity> where TEntity : class
  {
    private readonly Mock<IProductsRepository> _productsRepositoryMock;
    private readonly Mock<IStockRepository> _stockRepositoryMock;
    private readonly Mock<IOrdersRepository> _ordersRepositoryMock;
    private readonly Mock<IOrderStatesRepository> _orderStatesRepositoryMock;
    private readonly Mock<IDatabaseTransaction<Data.Products.Context.ProductsDBContext>> _databaseTransaction;
    private readonly ProductsDbEntitiesMock _productsDbEntitiesMock;

    public RepositoryMocks()
    {
      _productsRepositoryMock = new Mock<IProductsRepository>();
      _stockRepositoryMock = new Mock<IStockRepository>();
      _databaseTransaction = new Mock<Core.Transactions.IDatabaseTransaction<Data.Products.Context.ProductsDBContext>>();
      _ordersRepositoryMock = new Mock<IOrdersRepository>();
      _orderStatesRepositoryMock = new Mock<IOrderStatesRepository>();
      _productsDbEntitiesMock = new ProductsDbEntitiesMock(); 
    }
   
  }

  public enum RepositoryType
  {
    Products,
    Orders,
    Stock,
    OrderStates
  }
}
