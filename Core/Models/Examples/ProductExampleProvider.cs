using Microsoft.OpenApi.Any;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Examples
{
  internal class ProductExampleProvider : ISchemaFilter
  {
    public void Apply(Microsoft.OpenApi.Models.OpenApiSchema schema, SchemaFilterContext context)
    {
      schema.Example = GetExample();
    }

    public OpenApiObject GetExample()
    {
      return new OpenApiObject
      {      
        ["productId"] = new OpenApiInteger(5),      
        ["name"] = new OpenApiString("Azure Fundamental"),
        ["description"] = new OpenApiString("An introduction in Azure"),
        ["price"] = new OpenApiDouble(150.5),
      
      };
    }
  } 
  
}
