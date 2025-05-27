using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Laba9.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tovar> Tovary {  get; set; }

        public DbSet<Zakaz> Zakazy { get; set; }

        public DbSet<KategoriyaTovara> KategoriiTovarov { get; set; }
    }
}
