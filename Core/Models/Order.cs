using Core.Models.Examples;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
  /// <summary>
  /// Represents an order
  /// </summary>
  [SwaggerSchemaFilter(typeof(OrderExampleProvider))]
  public class OrderDTO
  {
    /// <summary>
    /// Gets or sets the order identifier
    /// </summary>
    public long OrderId { get; set; }

    /// <summary>
    /// Gets or sets the product identifier
    /// </summary>
    public long ProductId { get; set; }

    /// <summary>
    /// Gets or sets the name
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the created date in UTC
    /// </summary>
    public DateTime CreatedDateUtc { get; set; }

    /// <summary>
    /// Gets or sets the quantity
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets order state identifier
    /// </summary>
    public int OrderStateId { get; set; }
  }
}
