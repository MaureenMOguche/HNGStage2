using HNGStage2.Models;
using Microsoft.EntityFrameworkCore;

namespace HNGStage2.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }
    }
}
