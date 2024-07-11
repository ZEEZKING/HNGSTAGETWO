using HNGSTAGETWO.Interfaces.IRepository;
using HNGSTAGETWO.Models;
using HNGSTAGETWO.Models.ApplicationDBContext;
using Microsoft.EntityFrameworkCore;

namespace HNGSTAGETWO.Implementations.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext Context)
        {
            _Context = Context;
        }
        public async Task<User> GetUserById(string id)
        {
            return await _Context.Users
             .Where(x => x.ID == id)
             .Include(x => x.Organisation)
             .SingleOrDefaultAsync();
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            return await _Context.Users
            .Where(c => c.Email == email && c.Password == password)
            .Include(c => c.Organisation)
            .SingleOrDefaultAsync();
        }
    }
}
