using ComicBookInventory.Shared;
using System.Linq.Expressions;

namespace ComicBookInventory.DataAccess
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApiDbContext _dbContext;

        public GenericRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();   
        }

        public IEnumerable<T> GetWhere(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Where(expression).ToList();
        }

        public T Find(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().FirstOrDefault(expression);
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);    
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
        }

        public void Update(T entity)
        { 
            _dbContext.Set<T>().Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        { 
            _dbContext.Set<T>().UpdateRange(entities);
        }

        public void Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void RemoveWhere(Expression<Func<T, bool>> expression)
        {
            List<T> entities = _dbContext.Set<T>().Where(expression).ToList();

            foreach (T entity in entities)
            { 
                _dbContext.Set<T>().Remove(entity);
            }
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }
    }
}
