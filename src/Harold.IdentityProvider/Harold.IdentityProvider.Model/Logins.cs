namespace Harold.IdentityProvider.Model.Models
{
    public partial class Logins
    {
        public int LoginId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }

        public Roles Role { get; set; }
        public Users User { get; set; }
    }
}
