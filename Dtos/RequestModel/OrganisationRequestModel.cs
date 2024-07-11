using System.ComponentModel.DataAnnotations;

namespace HNGSTAGETWO.Dtos.RequestModel
{
    public class OrganisationRequestModel
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}
