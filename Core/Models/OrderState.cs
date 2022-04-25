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
  /// Represents an order state
  /// </summary>
  [SwaggerSchemaFilter(typeof(OrderStateExampleProvider))]
  public class OrderState
  {  
    /// <summary>
    /// Gets or sets the order state identifier
    /// </summary>
    public int OrderStateId { get; set; }

    /// <summary>
    /// Gets or sets the state
    /// </summary>
    public string State { get; set; } = null!;

  }
}
