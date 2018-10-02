using Harold.IdentityProvider.Model.Models;

namespace Harold.IdentityProvider.IService
{
    public interface IUsersService
    {
        GenericResponse<Users> Authenticate(string username, string password);
        GenericResponse<Users> Register(Users login, string password);
    }
}
