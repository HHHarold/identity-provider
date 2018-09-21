using System.Collections.Generic;

namespace Harold.IdentityProvider.Model.Models
{
    public partial class Roles
    {
        public Roles()
        {
            Logins = new HashSet<Logins>();
        }

        public int RolId { get; set; }
        public string Name { get; set; }

        public ICollection<Logins> Logins { get; set; }
    }
}
