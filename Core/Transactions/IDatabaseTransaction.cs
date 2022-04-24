using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Transactions
{
  public interface IDatabaseTransaction<TEntity> where TEntity : class
  {
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitTransactionAsync();
    void CreateSavePoint(string savePoint);
    void RollbackToSavePoint(string savePoint);
  }
}
