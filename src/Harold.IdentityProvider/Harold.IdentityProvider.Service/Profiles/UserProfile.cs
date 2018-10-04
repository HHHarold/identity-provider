using AutoMapper;
using Harold.IdentityProvider.Model.Models;
using Harold.IdentityProvider.Model.Requests;
using Harold.IdentityProvider.Model.Response;

namespace Harold.IdentityProvider.Service.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UsersRequest, Users>();
            CreateMap<Users, UsersResponse>();
        }
    }
}
