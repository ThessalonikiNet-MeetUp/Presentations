using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;

namespace EntityFrameworkTutorial.EntityFramework
{
    /// <summary>
    /// Context class responsible for data access.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection") { }

        public DbSet<CarCategory> CarCategories { get; set; }
        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CarCategory>().ToTable("Categories");
            modelBuilder.Entity<CarCategory>().Property(cc => cc.Code).HasColumnName("CategoryCode");

            modelBuilder.Entity<CarCategory>()
                .Property(cc => cc.Code)
                .HasMaxLength(50)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("UN_CarCategory_Code") { IsUnique = true }));

            modelBuilder.Configurations.Add(new CarConfiguration());
            modelBuilder.Configurations.Add(new EngineDetailsConfiguration());
        }
    }
}