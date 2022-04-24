using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Transactions
{
  public class ProductsDatabaseTransaction<TEntity> : IDatabaseTransaction<TEntity> where TEntity : class
  {
    private readonly Data.Products.Context.ProductsDBContext _productsDBContextTransaction;

    public ProductsDatabaseTransaction(Data.Products.Context.ProductsDBContext productsDBContextTransaction)
    {
      _productsDBContextTransaction = productsDBContextTransaction;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
      var transaction = await _productsDBContextTransaction.Database.BeginTransactionAsync();
      return transaction;     
    }

    public async Task CommitTransactionAsync()
    {
      await _productsDBContextTransaction.Database.CommitTransactionAsync();
    }

    public void CreateSavePoint(string savePoint)
    {
      throw new NotImplementedException();
    }

    public void RollbackToSavePoint(string savePoint)
    {
      throw new NotImplementedException();
    }
  }
}
