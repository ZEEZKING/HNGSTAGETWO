using HNGSTAGETWO.Interfaces.IRepository;
using HNGSTAGETWO.Models;
using HNGSTAGETWO.Models.ApplicationDBContext;
using Microsoft.EntityFrameworkCore;

namespace HNGSTAGETWO.Implementations.Repository
{
    public class OrganizationRepository : BaseRepository<Organisation>, IOrganizationRepository
    {
        public OrganizationRepository(ApplicationDbContext Context)
        {
            _Context = Context;
        }

        public async Task<Organisation> GetOrganisationById(string id)
        {
           return  await  _Context.Organizations
                .Where(x  => x.ID == id)
                .Include(x=>x.Users)
                .SingleOrDefaultAsync();
        }

        public async Task<List<Organisation>> GetUserOrganisations(string userId)
        {
            return await _Context.Organizations
              .Where(o => o.Users.Any(u => u.ID == userId))
              .ToListAsync();
        }
    }
}
