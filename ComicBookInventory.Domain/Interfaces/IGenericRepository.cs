using System.Linq.Expressions;

namespace ComicBookInventory.Shared
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetWhere(Expression<Func<T, bool>> expression);

        T Find(Expression<Func<T, bool>> expression);
        
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        
        void Remove(T entity);
        void RemoveWhere(Expression<Func<T, bool>> expression);
        void RemoveRange(IEnumerable<T> entities);
    }
}
