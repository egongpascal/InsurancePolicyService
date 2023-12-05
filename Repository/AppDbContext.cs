using InsurancePolicyService.Domain;
using InsurancePolicyService.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace InsurancePolicyService.Repository
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<PurchaseHistory> History { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}