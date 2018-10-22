using AutoMapper;
using Harold.IdentityProvider.IService;
using Harold.IdentityProvider.Model.Models;
using Harold.IdentityProvider.Model.Response;
using Harold.IdentityProvider.Repository;
using System;
using System.Linq;

namespace Harold.IdentityProvider.Service
{
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UsersService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public GenericResponse<UsersResponse> Authenticate(string username, string password)
        {
            Users user = _unitOfWork.Users.Get(filter: u => u.Username== username).FirstOrDefault();

            if (user == null) return new GenericResponse<UsersResponse> { Success = false, Message = "Username does not exists." };
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return new GenericResponse<UsersResponse> { Success = false, Message = "Incorrect password." };

            return new GenericResponse<UsersResponse> { Success = true, Data = _mapper.Map<UsersResponse>(user) };
        }

        public GenericResponse<bool> Register(Users user, string password)
        {
            if (_unitOfWork.Users.Get(filter: u => u.Username == user.Username).Any())
                return new GenericResponse<bool> { Success = false, Message = "Username is not available", Data = false };

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _unitOfWork.Users.Create(user);
            _unitOfWork.Save();

            return new GenericResponse<bool> { Success = true, Data = true };
        }

        public GenericResponse<bool> Update(Users userUpdated, Users userToUpdate, string password = null)
        {
            if(userUpdated.Username != userToUpdate.Username)
            {
                if (_unitOfWork.Users.Get(filter: u => u.Username == userUpdated.Username).Any())
                    return new GenericResponse<bool> { Success = false, Message = $"Username {userUpdated.Username} is alredy taken.", Data = false };
            }

            if (!string.IsNullOrWhiteSpace(password))
            {
                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                userUpdated.PasswordHash = passwordHash;
                userUpdated.PasswordSalt = passwordSalt;
            } else
            {
                userUpdated.PasswordHash = userToUpdate.PasswordHash;
                userUpdated.PasswordSalt = userToUpdate.PasswordSalt;
            }

            _unitOfWork.Users.Update(userUpdated);
            _unitOfWork.Save();
            return new GenericResponse<bool> { Success = true, Data = true };
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
