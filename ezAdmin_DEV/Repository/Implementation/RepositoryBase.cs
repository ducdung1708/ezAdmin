using Models.DBContext;
using Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly ezSQLDBContext _dbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public RepositoryBase(ezSQLDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public virtual void Add(TEntity Entity)
        {
            _dbContext.Set<TEntity>().Add(Entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Entities"></param>
        /// <returns></returns>
        public virtual void AddRange(List<TEntity> Entities)
        {
            _dbContext.Set<TEntity>().AddRange(Entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public virtual void Delete<T>(T Id)
        {
            TEntity Entity = _dbContext.Set<TEntity>().Find(Id);

            if (Entity != null)
            {
                _dbContext.Set<TEntity>().Remove(Entity);
            }
        }

        public virtual void Delete(TEntity Entity)
        {
            _dbContext.Set<TEntity>().Remove(Entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public virtual TEntity GetById<T>(T Id)
        {
            return _dbContext.Set<TEntity>().Find(Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> All()
        {
            return _dbContext.Set<TEntity>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public void Update(TEntity Entity)
        {
            _dbContext.Set<TEntity>().Attach(Entity);
            _dbContext.Entry(Entity).State = EntityState.Modified;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public void DeleteRange(List<TEntity> items)
        {
            _dbContext.Set<TEntity>().RemoveRange(items);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
