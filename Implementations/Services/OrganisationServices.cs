using HNGSTAGETWO.Dtos;
using HNGSTAGETWO.Dtos.RequestModel;
using HNGSTAGETWO.Dtos.ResponseModel;
using HNGSTAGETWO.Interfaces.IRepository;
using HNGSTAGETWO.Interfaces.IServices;
using HNGSTAGETWO.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HNGSTAGETWO.Implementations.Services
{
    public class OrganisationServices : IOrganisationServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrganizationRepository _organizationRepository;

        public OrganisationServices(IUserRepository userRepository, IOrganizationRepository organizationRepository)
        {
            _userRepository = userRepository;
            _organizationRepository = organizationRepository;
        }

        public async Task<BaseResponse> AddUserToAOrganization(string orgId, string userId)
        {
            var userOrg =  await _userRepository.ExistsAsync(x=>x.OrganizationId== orgId);
            if (userOrg) { throw new Exception("User Already In  this Organisation"); }
            var user = new User
            {
                OrganizationId = orgId,
            };
            await _userRepository.CreateAsync(user);
            return new BaseResponse
            {
                Message = "User Added Successfully",
                Sucesss = true,
            };
            
        }


        public async Task<OrganisationResponseModel> GetOrganizationById(string id)
        {
            var orgId  =  await _organizationRepository.GetOrganisationById(id);
            if (orgId == null) { throw new Exception("Organisation Not Found"); }
            return new OrganisationResponseModel
            {
                Message = "Organisation Found",
                Sucesss =  true,
                Data = new OrganisationDto
                {
                    OrgId = orgId.ID,
                    Name = orgId.Name,
                    Description = orgId.Description,
                }
            };
        }

        public async Task<OrganisationsResponseModel> GetUserOrganisations(string userId)
        {
            var org = await _organizationRepository.GetUserOrganisations(userId);
            if (org == null) { throw new Exception("User Organisations Not  Found"); }
            return new OrganisationsResponseModel
            {
                Message = "Organisation Found",
                Sucesss = true,
                Data = org.Select(x => new OrganisationDto
                {
                    OrgId  =  x.ID,
                    Name = x.Name,
                    Description = x.Description,

                }).ToList(),

            };
        }

        public async Task<OrganisationResponseModel> RegisterOrganization(string  userId,OrganisationRequestModel requestModel)
        {
            var organ = new Organisation
            {
                Name = requestModel.Name,
                Description = requestModel.Description,
                DateCreated = DateTime.UtcNow,
            };
            await _organizationRepository.CreateAsync(organ);
            return new OrganisationResponseModel
            {
                Message = "Organization Created Successfully",
                Sucesss = true,
                Data  = new OrganisationDto
                {
                    OrgId = organ.ID,
                    Name = requestModel.Name,
                    Description = requestModel.Description,
                }
            };
        }
    }
}
