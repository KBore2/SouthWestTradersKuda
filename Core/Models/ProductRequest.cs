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
  /// Represents the product request
  /// </summary>
  [SwaggerSchemaFilter(typeof(ProductRequestExampleProvider))]
  public class ProductRequest
  {

    /// <summary>
    /// Gets or sets the name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the price
    /// </summary>
    public decimal Price { get; set; }
  }
}
