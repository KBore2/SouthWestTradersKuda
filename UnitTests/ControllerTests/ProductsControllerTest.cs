using AutoMapper;
using Core.Mapper;
using Core.Models;
using Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Controllers;
using UnitTests.Mocks;
using Xunit;

namespace UnitTests.ControllerTests
{
  public class ProductsControllerTest
  {
    private readonly Mock<IProductsRepository> _productsRepositoryMock;
    private readonly Mock<IStockRepository> _stockRepositoryMock;
    private readonly IMapper _mapper;    
    private readonly MapperMock _mapperMock;    
    private readonly ProductsDbEntities _productsDbEntities;
   
    public ProductsControllerTest()
    {
      //Arange
      _productsRepositoryMock = new Mock<IProductsRepository>();
      _stockRepositoryMock = new Mock<IStockRepository>();
      _mapperMock = new MapperMock();
      _productsDbEntities = new ProductsDbEntities();      

      _mapper = _mapperMock.GetMapper();

      _productsRepositoryMock.Setup(x => x.GetAll()).Returns(_productsDbEntities.GetTestProducts());
      _productsRepositoryMock.Setup(x => x.Save());
      _productsRepositoryMock.Setup(x => x.GetByKey(It.IsAny<long>())).Returns(_productsDbEntities.GetTestProducts().FirstOrDefault());

      _stockRepositoryMock.Setup(x => x.GetAll()).Returns(_productsDbEntities.GetTestStock());
      _stockRepositoryMock.Setup(x => x.GetAvailableStock(It.IsAny<long>())).Returns(_productsDbEntities.GetTestStock().FirstOrDefault());
    }

    [Fact]
    public async void TestGetProducts()
    {     
      var controller = new ProductsController(_productsRepositoryMock.Object, _stockRepositoryMock.Object, _mapper);

      //Act
      var results = controller.Get();

      //Assert
      _productsRepositoryMock.Verify(r => r.GetAll());
      Assert.NotNull(results);
      Assert.Equal(_productsDbEntities.GetTestProducts().Count, results.Count());    
    }


    [Fact]
    public async void TestCreateProduct()
    {
      var controller = new ProductsController(_productsRepositoryMock.Object, _stockRepositoryMock.Object, _mapper);

      var product = new Product
      {
        Name = "C# Basics",
        Description = "C# Basics",
        Price = 500
      };

      //Act
      var results = controller.Post(product);

      //Assert
      _productsRepositoryMock.Verify(r => r.GetAll());     
      _productsRepositoryMock.Verify(r => r.Save());

      Assert.NotNull(results);
      Assert.IsAssignableFrom<OkObjectResult>(results);
    }

    [Fact]
    public async void TestDeleteProduct()
    {
      var controller = new ProductsController(_productsRepositoryMock.Object, _stockRepositoryMock.Object, _mapper);

      long productId = 1;

      //Act
      var results = controller.Delete(productId);

      //Assert
      _productsRepositoryMock.Verify(r => r.GetByKey(productId));
      _productsRepositoryMock.Verify(r => r.Save());

      Assert.NotNull(results);
      Assert.IsAssignableFrom<OkResult>(results);
    }

    [Fact]
    public async void TestAddStock()
    {
      var controller = new ProductsController(_productsRepositoryMock.Object, _stockRepositoryMock.Object, _mapper);

      long productId = 1;
      int quantity = 4;

      //Act
      var results = controller.AddStock(productId, quantity);

      //Assert
      _stockRepositoryMock.Verify(r => r.GetAvailableStock(productId));
      _stockRepositoryMock.Verify(r => r.Save());

      Assert.NotNull(results);
      Assert.IsAssignableFrom<OkResult>(results);
    }


  }
}
