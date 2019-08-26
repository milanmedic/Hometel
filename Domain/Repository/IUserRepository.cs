using System.Collections.Generic;
using System.Threading.Tasks;
using Hometel.Domain.Models;

namespace Hometel.Domain.Repositories {
    public interface IUserRepository {
        Task CreateUser(User user);
        Task<User> FindUser(string username);
        Task <IList<User>> ListAllUsersAsync();
        Task <User> UpdateUserAsync(User user);
    }
}