namespace Harold.IdentityProvider.Model.Response
{
    public class UsersResponse
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
