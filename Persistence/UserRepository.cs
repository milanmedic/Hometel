using System.Collections.Generic;
using System.Threading.Tasks;
using Hometel.Domain.Repositories;
using Hometel.Domain.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Hometel.Persistence {
    public class UserRepository : BaseRepository, IUserRepository {

        public UserRepository(AppDbContext context) : base(context) {
            
        }
        public async Task CreateUser(User user){
            await _context.Users.AddAsync(user);
            _context.SaveChanges();
        }
        public async Task<User> FindUser(string username) {
            var existingUser = await _context.Users.FirstOrDefaultAsync(suser => suser.Username == username);
            return existingUser;
        }
    }
}