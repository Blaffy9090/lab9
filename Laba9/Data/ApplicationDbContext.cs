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

        public DbSet<Tovar> tovars {  get; set; }

        public DbSet<Zakaz> zakazs { get; set; }

        public DbSet<KategoriyaTovara> kategoriyas { get; set; }
    }
}
