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
  public class ProductsDatabaseTransaction<TEntity> : IDatabaseTransaction<TEntity> where TEntity : class
  {
    private readonly Data.Products.Context.ProductsDBContext _productsDBContextTransaction;

    /// <summary>
    /// Initialises the constructor
    /// </summary>
    /// <param name="productsDBContextTransaction"></param>
    public ProductsDatabaseTransaction(Data.Products.Context.ProductsDBContext productsDBContextTransaction)
    {
      _productsDBContextTransaction = productsDBContextTransaction;
    }

    /// <summary>
    /// Begins a database transaction
    /// </summary>
    /// <returns></returns>
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
      var transaction = await _productsDBContextTransaction.Database.BeginTransactionAsync();
      return transaction;     
    }

    /// <summary>
    /// Commits a database transaction
    /// </summary>
    /// <returns></returns>
    public async Task CommitTransactionAsync()
    {
      await _productsDBContextTransaction.Database.CommitTransactionAsync();
    }

    /// <summary>
    /// Creates a savepoint from which you can rollback from
    /// </summary>
    /// <param name="savePoint"></param>
    public void CreateSavePoint(string savePoint)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Rollsback a database transaction from a savepoint
    /// </summary>
    /// <param name="savePoint"></param>
    public void RollbackToSavePoint(string savePoint)
    {
      throw new NotImplementedException();
    }
  }
}
