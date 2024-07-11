using Microsoft.EntityFrameworkCore;

namespace HNGSTAGETWO.Models.ApplicationDBContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Organisation> Organizations { get; set; }
    }
}
