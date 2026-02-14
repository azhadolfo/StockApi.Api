using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockApi.Api.Models;

namespace StockApi.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "18588f65-97f3-4a41-b73f-3d5a6c7fa0d0",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "bbe16d99-2a17-463e-be2c-2d41769a5131"
                },
                new IdentityRole
                {
                    Id = "b99c2763-8b22-4cbe-b3d7-88aa10190f86",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "554bcbf6-be81-4aa2-b174-adfdad43e9e5"
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}