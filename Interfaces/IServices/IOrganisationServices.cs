using HNGSTAGETWO.Dtos.RequestModel;
using HNGSTAGETWO.Dtos.ResponseModel;
using HNGSTAGETWO.Models;

namespace HNGSTAGETWO.Interfaces.IServices
{
    public interface IOrganisationServices
    {
        Task<OrganisationResponseModel> RegisterOrganization(string userId, OrganisationRequestModel requestModel);
        Task<OrganisationResponseModel> GetOrganizationById(string id);
        Task<BaseResponse> AddUserToAOrganization(string orgId, string userId);
        Task<OrganisationsResponseModel> GetUserOrganisations(string userId);
    }
}
