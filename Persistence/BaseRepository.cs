using System.Linq;

namespace Hometel.Persistence {
    public abstract class BaseRepository {
        protected readonly AppDbContext _context;
        public BaseRepository(AppDbContext context) => _context = context; 
    }
}