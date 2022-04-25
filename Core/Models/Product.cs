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
  /// Represents a product
  /// </summary>
  [SwaggerSchemaFilter(typeof(ProductExampleProvider))]
  public class Product
  {
    public long ProductId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
  }
}
