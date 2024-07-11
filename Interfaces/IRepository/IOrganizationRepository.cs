using HNGSTAGETWO.Models;

namespace HNGSTAGETWO.Interfaces.IRepository
{
    public interface IOrganizationRepository:IBaseRepository<Organisation>
    {
         Task<Organisation> GetOrganisationById(string id);
        Task<List<Organisation>> GetUserOrganisations(string userId);
    }
}
