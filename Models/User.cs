﻿namespace HNGSTAGETWO.Models
{
    public class User : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password {  get; set; }  
        public string? PhoneNumber {  get; set; }
        public string? OrganizationId {  get; set; }  
        public Organisation? Organisation { get; set; }
    }
}
