using Microsoft.OpenApi.Any;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Examples
{
  internal class OrderStateExampleProvider : ISchemaFilter
  {
    public void Apply(Microsoft.OpenApi.Models.OpenApiSchema schema, SchemaFilterContext context)
  {
    schema.Example = GetExample();
  }

  public OpenApiObject GetExample()
  {
    return new OpenApiObject
    {
      ["orderStateId"] = new OpenApiInteger(3),     
      ["state"] = new OpenApiString("Completed")    
    };
  }
}
}

