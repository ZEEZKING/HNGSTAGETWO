using HNGSTAGETWO.Dtos.RequestModel;
using HNGSTAGETWO.Implementations.Services;
using HNGSTAGETWO.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HNGSTAGETWO.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class OrganisationController : ControllerBase
    {
        private readonly IOrganisationServices _organisationServices;

        public OrganisationController(IOrganisationServices organisationServices)
        {
            _organisationServices = organisationServices;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrganisation([FromForm] OrganisationRequestModel model)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "User ID not found in token." });
            }

            var userId = userIdClaim.Value;

            var organisation = await _organisationServices.RegisterOrganization(userId, model);
            return CreatedAtAction(nameof(GetOrganisation), new { orgId = organisation.Data.OrgId }, organisation);
        }

        [HttpGet("{orgId}")]
        public async Task<IActionResult> GetOrganisation(string orgId)
        {
            var organisation = await _organisationServices.GetOrganizationById(orgId);
            if (organisation == null)
                return NotFound();

            return Ok(organisation);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserOrganisations()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "User ID not found in token." });
            }

            var userId = userIdClaim.Value;
            var organisations = await _organisationServices.GetUserOrganisations(userId);

            return Ok(organisations);
        }

        [HttpPost("{orgId}/users")]
        public async Task<IActionResult> AddUserToOrganisation(string orgId, [FromBody]  string userId)
        {
            await _organisationServices.AddUserToAOrganization(orgId, userId);
            return Ok(new { message = "User added to organisation successfully" });
        }
    }
}
