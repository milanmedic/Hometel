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

        public async Task<IList<User>> ListAllUsersAsync(){
            return await _context.Users.ToListAsync();
        }
        public async Task <User> UpdateUserAsync(User user){
            var entity = await _context.Users.FirstOrDefaultAsync(item => item.Username == user.Username);

            // Validate entity is not null
            if (entity != null)
            {
                // Make changes on entity
                entity.Name = user.Name;
                entity.Surname = user.Surname;
                entity.Gender = user.Gender;
                entity.PasswordHash = user.PasswordHash;
                entity.PasswordSalt = user.PasswordSalt;

                // Update entity in DbSet
                _context.Users.Update(entity);

                // Save changes in database
                _context.SaveChanges();
            }
            return entity;
            }
    }
}