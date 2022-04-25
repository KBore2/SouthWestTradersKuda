using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Mocks
{
  public abstract class DbContextMockBase<TDbContext> where TDbContext : DbContext
  {
    public abstract TDbContext GetDbContext();
    public abstract void PopulateEntities(TDbContext dbContext);
  }
}
