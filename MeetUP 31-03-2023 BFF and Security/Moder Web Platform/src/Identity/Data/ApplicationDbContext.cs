using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Identity.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .Property(p => p.Firstname)
                .IsRequired()
                .HasMaxLength(128);
            builder.Entity<ApplicationUser>()
                .Property(p => p.Lastname)
                .IsRequired()
                .HasMaxLength(128);

            base.OnModelCreating(builder);
        }
    }
}
