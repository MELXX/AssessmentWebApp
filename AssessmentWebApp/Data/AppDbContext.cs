using AssessmentWebApp.Data.Models;
using Microsoft.EntityFrameworkCore;
namespace AssessmentWebApp.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<User> User { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
