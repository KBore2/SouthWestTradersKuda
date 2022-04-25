using AutoMapper;
using Core.Models;
using Core.Repositories;
using Core.Transactions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Trading.Controllers;
using UnitTests.Mocks;
using Xunit;

namespace UnitTests.ControllerTests
{
  public class OrdersControllerTest
  {
    private readonly Mock<IProductsRepository> _productsRepositoryMock;
    private readonly Mock<IStockRepository> _stockRepositoryMock;
    private readonly Mock<IOrdersRepository> _ordersRepositoryMock;
    private readonly Mock<IOrderStatesRepository> _orderStatesRepositoryMock;
    private readonly Mock<IDatabaseTransaction<Data.Products.Context.ProductsDBContext>> _databaseTransaction;
    private readonly IMapper _mapper;
    private readonly MapperMock _mapperMock;
    private readonly ProductsDbEntitiesMock _productsDbEntitiesMock;

    public OrdersControllerTest()
    {
      //Arange
      _productsRepositoryMock = new Mock<IProductsRepository>();
      _stockRepositoryMock = new Mock<IStockRepository>();
      _databaseTransaction = new Mock<Core.Transactions.IDatabaseTransaction<Data.Products.Context.ProductsDBContext>>();
      _ordersRepositoryMock = new Mock<IOrdersRepository>();
      _orderStatesRepositoryMock = new Mock<IOrderStatesRepository>();
      _mapperMock = new MapperMock();
      _productsDbEntitiesMock = new ProductsDbEntitiesMock();

      _mapper = _mapperMock.GetMapper();

      _productsRepositoryMock.Setup(x => x.GetAll()).Returns(_productsDbEntitiesMock.GetTestProducts());
      _productsRepositoryMock.Setup(x => x.Save());
      _productsRepositoryMock.Setup(x => x.GetByKey(It.IsAny<long>())).Returns(_productsDbEntitiesMock.GetTestProducts().FirstOrDefault());

      _ordersRepositoryMock.Setup(x => x.GetAll()).Returns(_productsDbEntitiesMock.GetTestOrders());
      _ordersRepositoryMock.Setup(x => x.Save());
      _ordersRepositoryMock.Setup(x => x.GetByKey(It.IsAny<long>())).Returns(_productsDbEntitiesMock.GetTestOrders().FirstOrDefault());
      _ordersRepositoryMock.Setup(x => x.Find(It.IsAny<Expression<Func<Data.Products.Context.Order, bool>>>())).Returns(_productsDbEntitiesMock.GetTestOrders());

      _orderStatesRepositoryMock.Setup(x => x.GetAll()).Returns(_productsDbEntitiesMock.GetTestOrderStates());   
      _orderStatesRepositoryMock.Setup(x => x.GetByKey(It.IsAny<long>())).Returns(_productsDbEntitiesMock.GetTestOrderStates().FirstOrDefault());
      _orderStatesRepositoryMock.Setup(x => x.Find(It.IsAny<Expression<Func<Data.Products.Context.OrderState, bool>>>())).Returns(_productsDbEntitiesMock.GetTestOrderStates());

      _stockRepositoryMock.Setup(x => x.GetAll()).Returns(_productsDbEntitiesMock.GetTestStock());
      _stockRepositoryMock.Setup(x => x.GetAvailableStock(It.IsAny<long>())).Returns(_productsDbEntitiesMock.GetTestStock().FirstOrDefault());
      _stockRepositoryMock.Setup(x => x.Find((It.IsAny<Expression<Func<Data.Products.Context.Stock, bool>>>()))).Returns(_productsDbEntitiesMock.GetTestStock());

      _databaseTransaction.Setup(x => x.BeginTransactionAsync());
      _databaseTransaction.Setup(x => x.CommitTransactionAsync());
    }

    [Fact]
    public void TestGetOrders()
    {
      var controller = new OrdersController(_ordersRepositoryMock.Object, _productsRepositoryMock.Object, _orderStatesRepositoryMock.Object, _stockRepositoryMock.Object, _databaseTransaction.Object, _mapper);

      //Act
      var results = controller.Get();

      //Assert
      _ordersRepositoryMock.Verify(r => r.GetAll());
      Assert.NotNull(results);
      Assert.Equal(_productsDbEntitiesMock.GetTestOrders().Count, results.Count());
    }


    [Fact]
    public async Task TestPlaceOrder()
    {
      var controller = new OrdersController(_ordersRepositoryMock.Object, _productsRepositoryMock.Object, _orderStatesRepositoryMock.Object, _stockRepositoryMock.Object, _databaseTransaction.Object, _mapper);

      var order = new OrderRequest
      {
       ProductId = 1,
       Quantity = 5
      };

      //Act
      var results = await controller.PlaceOrder(order);

      //Assert
      _productsRepositoryMock.Verify(r => r.GetByKey(order.ProductId));
      _ordersRepositoryMock.Verify(r => r.Save());
      _databaseTransaction.Verify(x => x.BeginTransactionAsync());
      _databaseTransaction.Verify(x => x.CommitTransactionAsync(), Times.Once);

      Assert.NotNull(results);
      Assert.IsAssignableFrom<OkResult>(results);
    }

    [Fact]
    public void TestCompleteOrder()
    {
      var controller = new OrdersController(_ordersRepositoryMock.Object, _productsRepositoryMock.Object, _orderStatesRepositoryMock.Object, _stockRepositoryMock.Object, _databaseTransaction.Object, _mapper);

      long orderId = 1;
      long productId = 1;

      //Act
      var results = controller.CompleteOrder(orderId);

      //Assert
      _ordersRepositoryMock.Verify(r => r.GetByKey(orderId));
      _ordersRepositoryMock.Verify(r => r.Save());
      
      Assert.NotNull(results);
      Assert.IsAssignableFrom<OkResult>(results);
    }

    [Fact]
    public async Task TestCancelOrder()
    {
      var controller = new OrdersController(_ordersRepositoryMock.Object, _productsRepositoryMock.Object, _orderStatesRepositoryMock.Object, _stockRepositoryMock.Object, _databaseTransaction.Object, _mapper);

      long orderId = 1;
      long productId = 1;

      //Act
      var results = await controller.CancelOrder(orderId);

      //Assert
      _ordersRepositoryMock.Verify(r => r.GetByKey(orderId));
      _ordersRepositoryMock.Verify(r => r.Save());
      _databaseTransaction.Verify(x => x.BeginTransactionAsync());
      _databaseTransaction.Verify(x => x.CommitTransactionAsync(), Times.Once);

      Assert.NotNull(results);
      Assert.IsAssignableFrom<OkResult>(results);
    }

    [Fact]
    public void TestSearchByName()
    {
      var controller = new OrdersController(_ordersRepositoryMock.Object, _productsRepositoryMock.Object, _orderStatesRepositoryMock.Object, _stockRepositoryMock.Object, _databaseTransaction.Object, _mapper);

      string name = "Azure";

      //Act
      var results = controller.SearchByName(name);

      //Assert     
      Assert.NotNull(results);
      Assert.Equal(_productsDbEntitiesMock.GetTestOrders().Count, results.Count());


    }
  }
}
