using Harold.IdentityProvider.Model.Models;

namespace Harold.IdentityProvider.Repository
{
    public interface IUnitOfWork
    {
        IGenericRepository<Logins> Logins { get; }
        IGenericRepository<Roles> Roles { get; }
        IGenericRepository<Users>  Users { get; }
    }
}
