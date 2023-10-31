using System.Linq.Expressions;

namespace Repository.Interfaces
{
    public interface IRepositoryBase<TEntity> : IDisposable
    where TEntity : class
    {
        /// <summary>
        /// Generate statement to insert Entity into DB. NOT EXECUTED
        /// </summary>
        /// <param name="Entity"></param>
        void Add(TEntity Entity);

        /// <summary>
        /// Generate statement to insert a list of object (Entities) into DB. NOT EXECUTED
        /// </summary>
        /// <param name="Entities"></param>
        void AddRange(List<TEntity> Entities);

        IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Generate statement like "UPDATE TEntity SET (...). NOT EXECUTED
        /// </summary>
        /// <param name="Entity"></param>
        void Update(TEntity Entity);

        /// <summary>
        /// Generate statement to delete by Id. NOT EXECUTED
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Id"></param>
        void Delete<T>(T Id);

        /// <summary>
        /// Generate statement to delete by Entity. NOT EXECUTED
        /// </summary>
        /// <param name="Entity"></param>
        void Delete(TEntity Entity);

        /// <summary>
        /// Generate statement to delete by list of Entity. NOT EXECUTED
        /// </summary>
        /// <param name="items"></param>
        void DeleteRange(List<TEntity> items);

        /// <summary>
        /// Execute the previous statement: INSERT, UPDATE, DELETE
        /// </summary>
        /// <returns></returns>
        int Save();

        TEntity GetById<T>(T Id);

        IEnumerable<TEntity> All();
    }
}
