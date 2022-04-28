using Microsoft.OpenApi.Any;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Examples
{
  internal class OrderExampleProvider : ISchemaFilter
  {
    public void Apply(Microsoft.OpenApi.Models.OpenApiSchema schema, SchemaFilterContext context)
    {
      schema.Example = GetExample();
    }

    public OpenApiObject GetExample()
    {
      return new OpenApiObject
      {
        ["orderId"] = new OpenApiInteger(9),
        ["productId"] = new OpenApiInteger(5),
        ["orderStateId"] = new OpenApiInteger(1),
        ["name"] = new OpenApiString("Book Order"),
        ["quantity"] = new OpenApiInteger(15),
        ["createdDateUTC"] = new OpenApiDateTime(DateTime.UtcNow)
      };
    }
  }
}
