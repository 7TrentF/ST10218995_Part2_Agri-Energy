using AgriEnergySolution.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergySolution.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Farmer> Farmer { get; set; }
    
        public DbSet<Products> Products { get; set; }


        public DbSet<FarmerProducts> FarmerProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FarmerProducts>()
                .HasKey(fp => new { fp.FarmerId, fp.ProductId });

            modelBuilder.Entity<FarmerProducts>()
                .HasOne(fp => fp.Farmer)
                .WithMany(f => f.FarmerProducts)
                .HasForeignKey(fp => fp.FarmerId);

            modelBuilder.Entity<FarmerProducts>()
                .HasOne(fp => fp.Products)
                .WithMany(p => p.FarmerProducts)
                .HasForeignKey(fp => fp.ProductId);
        }
        public DbSet<Employee> Employee { get; set; }

    }
}
