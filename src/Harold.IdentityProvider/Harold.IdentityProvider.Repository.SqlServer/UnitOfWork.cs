using Harold.IdentityProvider.Model.Models;

namespace Harold.IdentityProvider.Repository.SqlServer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HaroldIdentityProviderContext _context;
        public UnitOfWork(HaroldIdentityProviderContext context)
        {
            _context = context;
            Roles = new GenericRepository<Roles>(_context);
            Users = new GenericRepository<Users>(_context);
        }
        
        public IGenericRepository<Roles> Roles { get; }
        public IGenericRepository<Users> Users { get; }
        
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }        
    }
}
