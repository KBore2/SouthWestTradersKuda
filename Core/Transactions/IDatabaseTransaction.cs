using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Transactions
{
  /// <summary>
  /// Provides operations to manage database transactions
  /// </summary>
  /// <typeparam name="TEntity"></typeparam>
  public interface IDatabaseTransaction<TEntity> where TEntity : class
  {
    /// <summary>
    /// Begins a database transaction
    /// </summary>
    /// <returns></returns>
    Task<IDbContextTransaction> BeginTransactionAsync();

    /// <summary>
    /// Commits a database transaction
    /// </summary>
    /// <returns></returns>
    Task CommitTransactionAsync();

    /// <summary>
    /// Creates a savepoint from which you can rollback from
    /// </summary>
    /// <param name="savePoint"></param>
    void CreateSavePoint(string savePoint);

    /// <summary>
    /// Rollsback a database transaction from a savepoint
    /// </summary>
    /// <param name="savePoint"></param>
    void RollbackToSavePoint(string savePoint);
  }
}
