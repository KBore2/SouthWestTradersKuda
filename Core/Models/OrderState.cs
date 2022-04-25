using Core.Models.Examples;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
  [SwaggerSchemaFilter(typeof(OrderStateExampleProvider))]
  public class OrderState
  {  
    public int OrderStateId { get; set; }
    public string State { get; set; } = null!;

  }
}
