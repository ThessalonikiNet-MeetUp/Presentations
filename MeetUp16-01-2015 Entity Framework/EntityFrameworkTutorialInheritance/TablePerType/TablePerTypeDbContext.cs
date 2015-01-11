using System;
using System.Data.Entity;

namespace EntityFrameworkTutorialInheritance.TablePerType
{
    /// <summary>
    /// Context class responsible for data access based on table per type inheritance mapping.
    /// http://weblogs.asp.net/manavi/inheritance-mapping-strategies-with-entity-framework-code-first-ctp5-part-2-table-per-type-tpt
    /// </summary>
    public class TablePerTypeDbContext : DbContext
    {
        public TablePerTypeDbContext()
            : base("TablePerType") { }

        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Car>().ToTable("Cars");
            modelBuilder.Entity<Motorbike>().ToTable("MotorBikes");
            modelBuilder.Entity<Truck>().ToTable("Trucks");
        }
    }
}