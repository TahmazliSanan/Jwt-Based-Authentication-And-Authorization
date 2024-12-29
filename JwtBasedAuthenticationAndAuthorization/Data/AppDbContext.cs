using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace JwtBasedAuthenticationAndAuthorization.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Entities.Book> Books { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
