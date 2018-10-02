using AutoMapper;
using Harold.IdentityProvider.Model.Models;
using Harold.IdentityProvider.Model.Requests;

namespace Harold.IdentityProvider.Service.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UsersRequest, Users>();
        }
    }
}
