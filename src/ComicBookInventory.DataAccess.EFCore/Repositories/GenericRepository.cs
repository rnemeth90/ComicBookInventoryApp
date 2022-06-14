using ComicBookInventory.Shared;
using System.Linq.Expressions;

namespace ComicBookInventory.DataAccess
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApiDbContext _dbContext;

        public ApiDbContext DbContext => _dbContext;

        public GenericRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<T> GetAll()
        {
            return DbContext.Set<T>().ToList();   
        }

        public IEnumerable<T> GetWhere(Expression<Func<T, bool>> expression)
        {
            return DbContext.Set<T>().Where(expression).ToList();
        }

        public T Find(Expression<Func<T, bool>> expression)
        {
            return DbContext.Set<T>().FirstOrDefault(expression);
        }

        public void Add(T entity)
        {
            DbContext.Set<T>().Add(entity);
            DbContext.SaveChanges();
        }

        public void AddRange(IEnumerable<T> entities)
        {
            DbContext.Set<T>().AddRange(entities);
            DbContext.SaveChanges();
        }

        public void Update(T entity)
        { 
            DbContext.Set<T>().Update(entity);
            DbContext.SaveChanges();
        }

        public void UpdateRange(IEnumerable<T> entities)
        { 
            DbContext.Set<T>().UpdateRange(entities);
            DbContext.SaveChanges();
        }

        public void Remove(T entity)
        {
            var e = DbContext.Set<T>().Find(entity);
            if (e != null)
            { 
                DbContext.Set<T>().Remove(entity);
                DbContext.SaveChanges();
            }
        }

        public void RemoveById(int id)
        {
            var _entity = DbContext.Set<T>().Find(id);
            if (_entity != null)
            {
                DbContext.Set<T>().Remove(_entity);
                DbContext.SaveChanges();
            }
        }

        public void RemoveWhere(Expression<Func<T, bool>> expression)
        {
            List<T> entities = DbContext.Set<T>().Where(expression).ToList();

            foreach (T entity in entities)
            { 
                DbContext.Set<T>().Remove(entity);
            }
            DbContext.SaveChanges();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            DbContext.Set<T>().RemoveRange(entities);
            DbContext.SaveChanges();
        }
    }
}
