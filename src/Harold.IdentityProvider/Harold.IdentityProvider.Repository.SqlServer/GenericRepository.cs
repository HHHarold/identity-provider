using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Harold.IdentityProvider.Repository.SqlServer
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly HaroldIdentityProviderContext _context;

        public GenericRepository(HaroldIdentityProviderContext context)
        {
            _context = context;
        }

        public bool Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges() > 0; 
        }

        public bool Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public IEnumerable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }
        
        public bool Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return _context.SaveChanges() > 0;
        }
    }
}
