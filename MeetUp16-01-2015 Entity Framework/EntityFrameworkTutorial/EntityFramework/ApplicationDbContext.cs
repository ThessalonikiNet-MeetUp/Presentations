using System;
using System.Data.Entity;

namespace EntityFrameworkTutorial.EntityFramework
{
    /// <summary>
    /// Context class responsible for data access based on table per concrete type inheritance mapping.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection") { }

        public DbSet<CarCategory> CarCategories { get; set; }
        public DbSet<Car> Cars { get; set; }
    }
}