using AutoMapper;
using Harold.IdentityProvider.Model.Models;
using Harold.IdentityProvider.Model.Request;
using Harold.IdentityProvider.Model.Response;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Harold.IdentityProvider.Service.Profiles
{
    public class RolesProfile : Profile
    {
        public RolesProfile()
        {
            CreateMap<RolesRequest, Roles>();
            CreateMap<Roles, RolesResponse>();
            CreateMap<Expression<Func<RolesRequest, bool>>, Expression<Func<Roles, bool>>>();
        }        
    }
}
