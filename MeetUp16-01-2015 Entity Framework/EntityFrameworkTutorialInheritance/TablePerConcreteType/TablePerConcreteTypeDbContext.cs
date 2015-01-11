using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace EntityFrameworkTutorialInheritance.TablePerConcreteType
{
    /// <summary>
    /// Context class responsible for data access based on table per concrete type inheritance mapping.
    /// http://weblogs.asp.net/manavi/inheritance-mapping-strategies-with-entity-framework-code-first-ctp5-part-3-table-per-concrete-type-tpc-and-choosing-strategy-guidelines
    /// </summary>
    public class TablePerConcreteTypeDbContext : DbContext
    {
        public TablePerConcreteTypeDbContext()
            : base("TablePerConcreteType"){}

        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>().Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Car>().Map(mc =>
            {
                mc.MapInheritedProperties();
                mc.ToTable("Cars");
            });
            modelBuilder.Entity<Motorbike>().Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Motorbike>().Map(mc =>
            {
                mc.MapInheritedProperties();
                mc.ToTable("Motorbikes");
            });
            modelBuilder.Entity<Truck>().Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Truck>().Map(mc =>
            {
                mc.MapInheritedProperties();
                mc.ToTable("Trucks");
            });
        }
    }
}