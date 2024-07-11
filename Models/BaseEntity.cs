namespace HNGSTAGETWO.Models
{
    public class BaseEntity
    {
        public string ID { get; set; }  =  Guid.NewGuid().ToString().Trim('0','5');
        public bool IsDeleted { get; set; }  = false;
        public DateTime DateCreated { get; set; }  = DateTime.UtcNow;
    }
}
