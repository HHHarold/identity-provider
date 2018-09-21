using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Harold.IdentityProvider.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetByCondition(Expression<Func<T, bool>> expression);
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }
}
