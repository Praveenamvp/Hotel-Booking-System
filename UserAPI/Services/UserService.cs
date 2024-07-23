using System.Security.Cryptography;
using System.Text;
using UserAPI.Interfaces;
using UserAPI.Models.DTO;
using UserAPI.Models;

namespace UserAPI.Services
{
    public class UserService
    {
        private IRepo<string, User> _repo;
        private ITokenGenerate _tokenService;

        public UserService(IRepo<string, User> repo, ITokenGenerate tokenGenerate)
        {
            _repo = repo;
            _tokenService = tokenGenerate;
        }

        public UserDTO Register(UserRegisterDTO userRegisterDTO)
        {
            UserDTO user = null;
            var hmac = new HMACSHA512();
            userRegisterDTO.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(userRegisterDTO.PasswordString));
            userRegisterDTO.HashKey = hmac.Key;
            var resultUserDTO = _repo.Add(userRegisterDTO);
            if (resultUserDTO != null)
            {
                user = new UserDTO();
                user.UserName = resultUserDTO.UserName;
                user.UserType = resultUserDTO.UserType;
                user.Token = _tokenService.GenerateToken(user);

                return user;
            }
            return null;



        }
        public UserDTO Login(UserDTO userDTO)

        {
            UserDTO user = null;
            var userData = _repo.Get(userDTO.UserName);
            if (userData != null)
            {
                var hmac = new HMACSHA512(userData.HashKey);
                var password = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                for (int i = 0; i < password.Length; i++)
                {
                    if (password[i] != userData.Password[i])
                    {
                        return null;
                    }
                }
                user = new UserDTO();
                user.UserName = userDTO.UserName;
                user.UserType = userData.UserType;
                user.Token = _tokenService.GenerateToken(user);
            }

            return user;


        }

      

    }
}

