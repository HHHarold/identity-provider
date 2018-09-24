using Harold.IdentityProvider.Model.Request;
using Harold.IdentityProvider.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Harold.IdentityProvider.IService
{
    public interface IRolesService
    {
        Response<IEnumerable<RolesResponse>> Get(Expression<Func<RolesRequest, bool>> filter = null, string orderBy = null, string includeProperties = "");
        Response<RolesResponse> GetById(object id);
        Response<bool> Create(RolesRequest entity);
        Response<bool> Update(RolesRequest entityToUpdate);
        Response<bool> Delete(object id);
        Response<bool> Delete(RolesRequest entityToDelete);
    }
}
