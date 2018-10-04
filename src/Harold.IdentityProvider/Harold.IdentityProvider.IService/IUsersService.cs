using Harold.IdentityProvider.Model.Models;
using Harold.IdentityProvider.Model.Response;

namespace Harold.IdentityProvider.IService
{
    public interface IUsersService
    {
        GenericResponse<UsersResponse> Authenticate(string username, string password);
        GenericResponse<Users> Register(Users login, string password);
    }
}
