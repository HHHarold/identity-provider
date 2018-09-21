using System.Collections.Generic;

namespace Harold.IdentityProvider.Model.Models
{
    public partial class Users
    {
        public Users()
        {
            Logins = new HashSet<Logins>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Logins> Logins { get; set; }
    }
}
