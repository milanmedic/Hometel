using System.Collections.Generic;
using Hometel.Domain.Models;
using System.Threading.Tasks;

namespace Hometel.Domain.Services {
    public interface IUserService {
        Task<User> Authenticate(string email, string password);
        Task<User> RegisterUserAsync(User user, string password);
        Task<User> GetUserAsync(string username);
    }
}