﻿using Core.Infastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
  /// <summary>
  /// Provides operations to manage orders
  /// </summary>
  internal interface IOrdersRepository: IRepository<Data.Products.Context.Order>
  {
  }
}
