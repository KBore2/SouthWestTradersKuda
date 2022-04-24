using Core.Infastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
  /// <summary>
  /// Provides operations to manage order state
  /// </summary>
  public interface IOrderStatesRepository: IRepository<Data.Products.Context.OrderState>
  {
  }
}
