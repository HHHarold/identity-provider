using Harold.IdentityProvider.Model.Models;
using Harold.IdentityProvider.Model.Response;

namespace Harold.IdentityProvider.IService
{
    public interface IUsersService
    {
        GenericResponse<UsersResponse> Authenticate(string username, string password);
        GenericResponse<bool> Register(Users user, string password);
        GenericResponse<bool> Update(Users userUpdated, Users userToUpdate, string password);
    }
}
