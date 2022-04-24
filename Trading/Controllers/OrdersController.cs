using AutoMapper;
using Core.Models;
using Core.Repositories;
using Core.Transactions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Controllers
{
  [Route("[controller]")]
  public class OrdersController : ControllerBase
  {
    private readonly IOrdersRepository _ordersRepository;
    private readonly IProductsRepository _productsRepository;
    private readonly IOrderStatesRepository _orderStatesRepository;
    private readonly IStockRepository _stockRepository;
    private readonly IDatabaseTransaction<Data.Products.Context.ProductsDBContext> _databaseTransaction;
    private readonly IMapper _mapper;

    public OrdersController(IOrdersRepository ordersRepository, IProductsRepository productsRepository, IOrderStatesRepository orderStatesRepository, IStockRepository stockRepository, IDatabaseTransaction<Data.Products.Context.ProductsDBContext> databaseTransaction, IMapper mapper)
    {
      _ordersRepository = ordersRepository;
      _productsRepository = productsRepository;
      _orderStatesRepository = orderStatesRepository;
      _stockRepository = stockRepository;
      _databaseTransaction = databaseTransaction;
      _mapper = mapper;
    }

    [HttpGet]
    public IEnumerable<Order> Get()
    {
      var orders = _ordersRepository.GetAll();
      return _mapper.Map<IEnumerable<Order>>(orders);
    }

    [HttpGet("{orderId}")]
    public Order Get(long orderId)
    {
      var orders = _ordersRepository.GetAll();
      return _mapper.Map<Order>(orders);
    }

    /// <summary>
    /// Places an order
    /// </summary>
    /// <returns></returns>    
    [HttpPost("PlaceOrder")]
    public async Task<IActionResult> PlaceOrder([FromBody] OrderRequest model)
    {
      if (model.Quantity <= 0)
      {
        return BadRequest("Business rule violation: Invalid quantity, an order must be placed with a quantity greater than zero");
      }

      var product = _productsRepository.GetByKey(model.ProductId);  

      if (product is null)
      {
        return BadRequest("Invalid product supplied");
      }

      var orderStates = _orderStatesRepository.Find(x => x.OrderStateId == (int)OrderState.Reserved).FirstOrDefault();

      if (orderStates is null)
      {
        throw new InvalidOperationException("Invalid order state");
      }
      
      var availableStock = _stockRepository.GetAvailableStock(product.ProductId);

      if (availableStock != null)
      {
        if (availableStock.AvailableStock < model.Quantity)
        {
          return BadRequest("Business rule violation: Requested quantity is more than the available stock");
        }

        if (availableStock.AvailableStock == 0)
        {
          return BadRequest("Business rule violation: Product is out of stock, order cannot be completed");
        }
      }

      var transaction = await _databaseTransaction.BeginTransactionAsync();
     
      try
      {
        var order = new Data.Products.Context.Order
        {
          ProductId = model.ProductId,
          Name = $"{product.Name}-{product.Price}-{model.Quantity}",
          OrderStateId = orderStates.OrderStateId,
          CreatedDateUtc = DateTime.UtcNow,
          Quantity = model.Quantity
        };

        _ordersRepository.Add(order);
        _ordersRepository.Save();  

        availableStock.AvailableStock = availableStock.AvailableStock - model.Quantity;

        _stockRepository.Update(availableStock);
        _stockRepository.Save();

        await _databaseTransaction.CommitTransactionAsync();
      }

      catch (Exception)
      {
        await transaction.RollbackAsync();

        return BadRequest("Transaction failed, operation rolled back");
      }    

      return Ok();
    }

    /// <summary>
    /// Completes an order
    /// </summary>
    /// <returns></returns>    
    [HttpPost("CompleteOrder/{orderId}")]
    public IActionResult CompleteOrder(long orderId)
    {
      var order = _ordersRepository.GetByKey(orderId);

      if (order is null)
      {
        return BadRequest("Invalid order supplied");
      }

      if (order.OrderStateId == (int)OrderState.Cancelled)
      {
        return BadRequest("Business rule violation: Order is in a cancelled state");
      }

      var orderStates = _orderStatesRepository.Find(x => x.OrderStateId == (int)OrderState.Completed).FirstOrDefault();

      if (orderStates is null)
      {
        throw new InvalidOperationException("Invalid order state");
      }

      order.OrderStateId = orderStates.OrderStateId;

      _ordersRepository.Update(order);
      _ordersRepository.Save();

      return Ok();
    }

    /// <summary>
    /// Completes an order
    /// </summary>
    /// <returns></returns>    
    [HttpPost("CancelOrder/{orderId}")]
    public async Task<IActionResult> CancelOrder(long orderId)
    {
      var order = _ordersRepository.GetByKey(orderId);

      if (order is null)
      {
        return BadRequest("Invalid order supplied");
      }

      //Order is already cancelled.
      if (order.OrderStateId == (int)OrderState.Cancelled)
      {
        return Ok();
      }

      if (order.OrderStateId == (int)OrderState.Completed)
      {
        return BadRequest("Business rule violation: An order in a completed state cannot be cancelled");
      }

      var orderStates = _orderStatesRepository.Find(x => x.OrderStateId == (int)OrderState.Cancelled).FirstOrDefault();

      if (orderStates is null)
      {
        throw new InvalidOperationException("Invalid order state");
      }

      var transaction = await _databaseTransaction.BeginTransactionAsync();

      try
      {
        order.OrderStateId = orderStates.OrderStateId;

        _ordersRepository.Update(order);
        _ordersRepository.Save();

        var availableStock = _stockRepository.GetAvailableStock(order.ProductId);

        //Restore the product stock previously reserved.
        if (availableStock != null)
        {
          availableStock.AvailableStock = availableStock.AvailableStock + order.Quantity;

          _stockRepository.Update(availableStock);
          _stockRepository.Save();
        }

        await _databaseTransaction.CommitTransactionAsync();

      }
      catch (Exception)
      {
        await transaction.RollbackAsync();
        return BadRequest("Transaction failed, operation rolled back");
      }
     
      return Ok();
    }

    [HttpGet("SearchByName/{orderName}")]
    public IEnumerable<Order> SearchByName(string orderName)
    {
      var products = _ordersRepository.Find(x => x.Name.Contains(orderName));
      return _mapper.Map<IEnumerable<Order>>(products);
    }

    [HttpGet("SearchByDate/{orderDate}")]
    public IEnumerable<Order> SearchByDate(DateTime orderDate)
    {
      var products = _ordersRepository.Find(x => x.CreatedDateUtc.Date == orderDate.Date);
      return _mapper.Map<IEnumerable<Order>>(products);
    }

    /// <summary>
    /// Represents order states
    /// </summary>
    public enum OrderState
    {
      /// <summary>
      /// Reserved
      /// </summary>
      Reserved = 1,

      /// <summary>
      /// Cancelled
      /// </summary>
      Cancelled,

      /// <summary>
      /// Completed
      /// </summary>
      Completed
    }

  }
}
