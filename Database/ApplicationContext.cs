using Web_2k.Models;
using Microsoft.EntityFrameworkCore;


namespace Web_2k.Database
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Questionnaire> Questionnaires { get; set; }

        public DbSet<Meeting> Meetings { get; set; }

    }
}
