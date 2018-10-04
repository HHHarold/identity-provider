using Harold.IdentityProvider.IService;
using Harold.IdentityProvider.Model.Models;
using Harold.IdentityProvider.Repository;
using System;
using System.Linq;

namespace Harold.IdentityProvider.Service
{
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UsersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public GenericResponse<Users> Authenticate(string username, string password)
        {
            Users login = _unitOfWork.Users.Get(filter: u => u.Username== username).FirstOrDefault();

            if (login == null) return new GenericResponse<Users> { Success = false, Message = "Username does not exists." };
            if (!VerifyPasswordHash(password, login.PasswordHash, login.PasswordSalt))
                return new GenericResponse<Users> { Success = false, Message = "Incorrect password." };

            return new GenericResponse<Users> { Success = true, Data = login };
        }

        public GenericResponse<Users> Register(Users user, string password)
        {
            if (_unitOfWork.Users.Get(filter: u => u.Username == user.Username).Any())
                return new GenericResponse<Users> { Success = false, Message = "Username is not available" };

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _unitOfWork.Users.Create(user);
            _unitOfWork.Save();

            return new GenericResponse<Users> { Success = true, Data = user };
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        
        private static bool VerifyPasswordHash (string password, byte[] storedHash, byte[] storedSalt)
        {
            //TODO: Verify this exepctions
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }

       
    }
}
