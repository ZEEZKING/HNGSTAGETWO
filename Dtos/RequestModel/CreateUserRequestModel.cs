using System.ComponentModel.DataAnnotations;

namespace HNGSTAGETWO.Dtos.RequestModel
{
    public class CreateUserRequestModel
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
    }
}
