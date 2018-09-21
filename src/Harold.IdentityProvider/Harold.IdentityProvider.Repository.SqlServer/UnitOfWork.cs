using Harold.IdentityProvider.Model.Models;

namespace Harold.IdentityProvider.Repository.SqlServer
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(HaroldIdentityProviderContext context)
        {
            Logins = new GenericRepository<Logins>(context);
            Roles = new GenericRepository<Roles>(context);
            Users = new GenericRepository<Users>(context);
        }
        public IGenericRepository<Logins> Logins { get; }

        public IGenericRepository<Roles> Roles { get; }

        public IGenericRepository<Users> Users { get; }
    }
}
