using UserAPI.Models.DTO;
using UserAPI.Models;

namespace UserAPI.Interfaces
{
    
        public interface IRepo<K, T>
        {
            T Get(string name);
            ICollection<T> GetAll();
            T Add(User user);
            T Update(UserDTO user);
            T Update(UserRegisterDTO user);
            T Delete(string name);

        }
    }

