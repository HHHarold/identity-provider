using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Harold.IdentityProvider.Repository
{
    public interface IGenericRepository<T> where T : class
    {        
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, string orderBy = null, string includeProperties = "");
        T GetById(object id);
        void Create(T entity);
        void Update(T entityToUpdate);
        void Delete(object id);
        void Delete(T entityToDelete);
    }
}
