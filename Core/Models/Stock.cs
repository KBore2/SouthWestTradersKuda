﻿using Core.Models.Examples;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
  /// <summary>
  /// Represents stock
  /// </summary>
  [SwaggerSchemaFilter(typeof(StockExampleProvider))]
  public class Stock
  {
    /// <summary>
    /// Gets or sets the stock identifier
    /// </summary>
    public long StockId { get; set; }

    /// <summary>
    /// Gets or sets the product identifier
    /// </summary>
    public long ProductId { get; set; }

    /// <summary>
    /// Gets or sets the available stock
    /// </summary>
    public int AvailableStock { get; set; }
  }
}
