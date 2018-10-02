using System.Collections.Generic;

namespace Harold.IdentityProvider.Model.Models
{
    public class Roles
    {
        public Roles()
        {
            Users = new HashSet<Users>();
        }

        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Users> Users { get; set; }
    }
}
