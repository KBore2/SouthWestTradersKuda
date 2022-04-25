using Microsoft.OpenApi.Any;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Examples
{
  internal class StockExampleProvider : ISchemaFilter
  {
    public void Apply(Microsoft.OpenApi.Models.OpenApiSchema schema, SchemaFilterContext context)
    {
      schema.Example = GetExample();
    }

    public OpenApiObject GetExample()
    {
      return new OpenApiObject
      {
        ["stockId"] = new OpenApiInteger(100),
        ["productId"] = new OpenApiInteger(5),
        ["availableStock"] = new OpenApiInteger(150),
      
      };
    }
  }
}

