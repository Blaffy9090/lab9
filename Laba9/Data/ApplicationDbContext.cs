using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Laba9.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tovar> Tovary { get; set; }
        public DbSet<Zakaz> Zakazy { get; set; }
        public DbSet<KategoriyaTovara> KategoriiTovarov { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Tovar-KategoriyaTovara relationship
            modelBuilder.Entity<Tovar>()
                .HasOne(t => t.KategoriyaTovara)
                .WithMany(k => k.Tovary)
                .HasForeignKey(t => t.KategoriyaTovaraId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Zakaz-Tovar relationship
            modelBuilder.Entity<Zakaz>()
                .HasOne(z => z.Tovar)
                .WithMany(t => t.Zakazy)
                .HasForeignKey(z => z.TovarId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure decimal precision for price fields
            modelBuilder.Entity<Tovar>()
                .Property(t => t.Cena)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Zakaz>()
                .Property(z => z.ObshayaStoimost)
                .HasColumnType("decimal(18,2)");
        }
    }
}