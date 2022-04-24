using AutoMapper;
using Core.Models;
using Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ProductsController: ControllerBase
  {
    private readonly IProductsRepository _productsRepository;
    private readonly IStockRepository _stockRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initialises the constructor
    /// </summary>
    /// <param name="productsRepository"></param>
    /// <param name="stockRepository"></param>
    /// <param name="mapper"></param>
    public ProductsController(IProductsRepository productsRepository, IStockRepository stockRepository, IMapper mapper)
    {
      _productsRepository = productsRepository;
      _stockRepository = stockRepository;
      _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a set of products
    /// </summary>
    /// <returns></returns>

    [HttpGet]
    public IEnumerable<Product> Get()
    {     
      var products = _productsRepository.GetAll();
      return _mapper.Map<IEnumerable<Product>>(products);
    }

    [HttpGet("{productId}")]
    public Product Get(long productId)
    {
      var products = _productsRepository.GetAll();
      return _mapper.Map<Product>(products);
    }

    [HttpPost]
    public IActionResult Product([FromBody] Product model)
    {
      var products = _productsRepository.GetAll();

      if (products.Any(x => x.Name == model.Name))
      {
        //product with same name already exists
        return Conflict();
      }

      //inject dependency - factory pattern
      var productEntity = new Data.Products.Context.Product();

      var product = _mapper.Map(model, productEntity);

      _productsRepository.Add(product);
      _productsRepository.Save();

      return Ok(product);
    }

    [HttpDelete("{productId}")]
    public IActionResult Delete(long productId)
    {
      var product = _productsRepository.GetByKey(productId);

      if (product is null)
      {
        //product does not exist
        return NotFound();
      }
     
      _productsRepository.Delete(product);
      _productsRepository.Save();

      return Ok();
    }

    [HttpGet("SearchByName/{productName}")]
    public IEnumerable<Product> SearchByName(string productName)
    {
      var products = _productsRepository.Find(x => x.Name.Contains(productName));
      return _mapper.Map<IEnumerable<Product>>(products);    
    }

    [HttpGet("AvailableStock/{productId}")]
    public Stock AvailableStock(long productId)
    {
      var products = _stockRepository.GetAvailableStock(productId);
      return _mapper.Map<Stock>(products);
    }

    [HttpPost("AddStock/{productId}/{quantity}")]
    public IActionResult AddStock(long productId, int quantity)
    {
      var availableStock = _stockRepository.GetAvailableStock(productId);

      //no stock available
      if (availableStock is null)
      {
        var stock = new Data.Products.Context.Stock
        {
          AvailableStock = quantity,
          ProductId = productId
        };        

        _stockRepository.Add(stock);
        _stockRepository.Save();

        return Ok();
      }

      availableStock.AvailableStock = availableStock.AvailableStock + quantity;

      _stockRepository.Update(availableStock);
      _stockRepository.Save();

      return Ok();
    }
  }
}
