using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Harold.IdentityProvider.Repository.SqlServer.Extensions;

namespace Harold.IdentityProvider.Repository.SqlServer
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly HaroldIdentityProviderContext _context;
        private readonly DbSet<T> dbSet;

        public GenericRepository(HaroldIdentityProviderContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }

        public void Create(T entity)
        {
            dbSet.Add(entity);
        }
        

        public void Delete(T entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached) dbSet.Attach(entityToDelete);
            dbSet.Remove(entityToDelete);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, string orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = dbSet;
            if (filter != null) query = query.Where(filter);
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperties);
            }
            if(orderBy != null)
            {
                return query.OrderBy(orderBy).AsNoTracking().ToList();
            }
            else
            {
                return query.AsNoTracking().ToList();
            }
        }

        public T GetById(object id)
        {
            var entity = dbSet.Find(id);
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public void Update(T entityToUpdate)
        {
            dbSet.Update(entityToUpdate);
        }
    }
}
