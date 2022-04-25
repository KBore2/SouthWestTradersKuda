using Core.Models.Examples;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
  [SwaggerSchemaFilter(typeof(StockExampleProvider))]
  public class Stock
  {
    public long StockId { get; set; }
    public long ProductId { get; set; }
    public int AvailableStock { get; set; }
  }
}
