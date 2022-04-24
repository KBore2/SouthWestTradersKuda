using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
  public class Order
  {
    public long OrderId { get; set; }
    public long ProductId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedDateUtc { get; set; }
    public int Quantity { get; set; }
    public int OrderStateId { get; set; }
  }
}
