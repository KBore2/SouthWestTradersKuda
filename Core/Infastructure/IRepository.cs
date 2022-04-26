using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infastructure
{
    /// <summary>
    /// Provides abstraction to manage generic entities.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
      /// <summary>
      /// Retrieves all the entities
      /// </summary>
      /// <returns></returns>
      IEnumerable<TEntity> GetAll();

      /// <summary>
      /// Retrieves an entity by key
      /// </summary>
      /// <param name="key"></param>
      /// <returns></returns>
      TEntity GetByKey(object key);

      /// <summary>
      /// Retrieves an entity by composite key
      /// </summary>
      /// <param name="key1"></param>
      /// <param name="key2"></param>
      /// <returns></returns>
      TEntity GetByKey(object key1, object key2);

      /// <summary>
      /// Retrieves entities based on a predicate
      /// </summary>
      /// <param name="predicate"></param>
      /// <returns></returns>
      IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

      /// <summary>
      /// Persists an entity
      /// </summary>
      /// <param name="entity"></param>

      public void Add(TEntity entity);

      /// <summary>
      /// Persists a range of entities
      /// </summary>
      /// <param name="entities"></param> 
      void AddRange(ICollection<TEntity> entities);

      /// <summary>
      /// Deletes an entity
      /// </summary>
      /// <param name="entity"></param>
      void Delete(TEntity entity);

      /// <summary>
      /// Deletes a range of entities
      /// </summary>
      /// <param name="entities"></param>
      void DeleteRange(ICollection<TEntity> entities);

      /// <summary>
      /// Updates an entity
      /// </summary>
      /// <param name="entity"></param>
      void Update(TEntity entity);

      /// <summary>
      /// Calls the save changes method to signify the transaction/operation is complete
      /// </summary>
      void Save();
    }
  }


