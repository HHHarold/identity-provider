using AutoMapper;
using Harold.IdentityProvider.IService;
using Harold.IdentityProvider.Model.Models;
using Harold.IdentityProvider.Model.Request;
using Harold.IdentityProvider.Model.Response;
using Harold.IdentityProvider.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Harold.IdentityProvider.Service
{
    public class RolesService : IRolesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RolesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Response<bool> Create(RolesRequest entity)
        {
            _unitOfWork.Roles.Create(_mapper.Map<Roles>(entity));
            if (_unitOfWork.Save()) return new Response<bool> { Data = true, Success = true };
            return new Response<bool> { Data = false, Success = false };
        }

        public Response<bool> Delete(object id)
        {
            _unitOfWork.Roles.Delete(id);
            if (_unitOfWork.Save()) return new Response<bool> { Data = true, Success = true };
            return new Response<bool> { Data = false, Success = false };
        }

        public Response<bool> Delete(RolesRequest entityToDelete)
        {
            _unitOfWork.Roles.Delete(entityToDelete);
            if (_unitOfWork.Save()) return new Response<bool> { Data = true, Success = true };
            return new Response<bool> { Data = false, Success = false };
        }

        public Response<IEnumerable<RolesResponse>> Get(Expression<Func<RolesRequest, bool>> filter = null, string orderBy = null, string includeProperties = "")
        {
            var data = _unitOfWork.Roles.Get(_mapper.Map<Expression<Func<Roles, bool>>>(filter), orderBy, includeProperties);
            return new Response<IEnumerable<RolesResponse>> { Data = _mapper.Map<IEnumerable<RolesResponse>>(data), Success = true };
        }

        public Response<RolesResponse> GetById(object id)
        {
            var data = _unitOfWork.Roles.GetById(id);
            return new Response<RolesResponse> { Data = _mapper.Map<RolesResponse>(data), Success = true };
        }

        public Response<bool> Update(RolesRequest entityToUpdate)
        {
            _unitOfWork.Roles.Update(_mapper.Map<Roles>(entityToUpdate));
            if (_unitOfWork.Save()) return new Response<bool> { Data = true, Success = true };
            return new Response<bool> { Data = false, Success = false };
        }
    }
}
