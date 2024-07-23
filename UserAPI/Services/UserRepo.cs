using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using UserAPI.Models.DTO;
using UserAPI.Models;
using UserAPI.Interfaces;

namespace UserAPI.Services
{
    public class UserRepo:IRepo<string, User>
    {
        private readonly UserContext _context;

        public UserRepo(UserContext userContext)
        {
            _context = userContext;

        }
        public User Get(string name)
        {
            try
            {
                User user = _context.Users.FirstOrDefault(u => u.UserName.ToLower()== name.ToLower());
                return user;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;

        }

        public ICollection<User> GetAll()
        {
            ICollection<User> users = _context.Users.ToList();

            if (users != null)
            {
                return users;
            }
            return null;

        }
        public User Delete(string userName)
        {

            try
            {
                User user = Get(userName);
                User userResult = new User();
                userResult.UserName = user.UserName;
                _context.Users.Remove(user);
                _context.SaveChanges();
               
                return userResult;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);


            }
            return null;
        }
        public User Add(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return user;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }

        public User Update(UserDTO user)
        {
            User userData = _context.Users.FirstOrDefault(u => u.UserName == user.UserName); ;
            if (userData != null)
            {
                var hmac = new HMACSHA512();

                userData.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                userData.HashKey = hmac.Key;
                _context.SaveChanges();
                User userResult = new User();
                userResult.UserName = userData.UserName;
                userResult.UserType = userData.UserType;
                return userResult;

            }

            return null;


        }
        public User Update(UserRegisterDTO user)
        {
            User userData = _context.Users.FirstOrDefault(u => u.UserName == user.UserName); ;
            if (userData != null)
            {
                var hmac = new HMACSHA512();

                userData.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.PasswordString));
                userData.HashKey = hmac.Key;
                userData.UserName = user.UserName;
                userData.Email= user.Email;
                userData.PhoneNumber = user.PhoneNumber;
                userData.Age = user.Age;
               
                _context.SaveChanges();
                User userResult = new User();
                userResult.UserName = userData.UserName;
                userResult.UserType = userData.UserType;
                return userResult;


            }

            return null;


        }
    }

}
