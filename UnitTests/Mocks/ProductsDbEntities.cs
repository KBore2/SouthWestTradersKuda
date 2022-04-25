using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Mocks
{
  internal class ProductsDbEntities
  {
    public ICollection<Data.Products.Context.Product> GetTestProducts()
    {
      return new List<Data.Products.Context.Product>
      {
        new Data.Products.Context.Product
        {
         ProductId = 1,
         Name = "Azure fundamentals",
         Description = "Azure fundamentals",
         Price = 250
        },
        new Data.Products.Context.Product
        {
         ProductId = 1,
         Name = "Entity framework",
         Description = "Entity framework",
         Price = 250
        },
      };
    }

    public ICollection<Data.Products.Context.Order> GetTestOrders()
    {
      return new List<Data.Products.Context.Order>
      {
        new Data.Products.Context.Order
        {
         OrderId = 1,
         ProductId = 1,
         Name = "Azure fundamentals-",
         CreatedDateUtc = DateTime.UtcNow,
         Quantity = 10
        },
        new Data.Products.Context.Order
        {
         OrderId = 2,
         ProductId = 1,
         Name = "Azure fundamentals",
         CreatedDateUtc = DateTime.UtcNow,
         Quantity = 22
        },
      };
    }
    public ICollection<Data.Products.Context.Stock> GetTestStock()
    {
      return new List<Data.Products.Context.Stock>
      {
        new Data.Products.Context.Stock
        {
         StockId = 1,
         ProductId = 1,
         AvailableStock = 100
        }      
      };
    }
    public ICollection<Data.Products.Context.OrderState> GetTestOrderStates()
    {
      return new List<Data.Products.Context.OrderState>
      {
        new Data.Products.Context.OrderState
        {
         OrderStateId = 1,
         State = "Reserved"
        },
       new Data.Products.Context.OrderState
        {
         OrderStateId = 2,
         State = "Cancelled"
        },
       new Data.Products.Context.OrderState
        {
         OrderStateId = 3,
         State = "Completed"
        },
      };
    }

  }
}
