using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
  /// <summary>
  /// Represents the order request
  /// </summary>
  public class OrderRequest
  {
    public long ProductId { get; set; }
    public int Quantity { get; set; }
  }
}
