namespace HNGSTAGETWO.Models
{
    public class Organisation : BaseEntity
    {
        public string? Name {  get; set; }  
        public string? Description { get; set; }
        public ICollection<User>?  Users { get; set; } = new List<User>();
    }
}
