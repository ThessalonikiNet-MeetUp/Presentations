using System;
using System.Data.Entity.ModelConfiguration;

namespace EntityFrameworkTutorial.EntityFramework
{
    public class CarConfiguration : EntityTypeConfiguration<Car>
    {
        public CarConfiguration()
        {
            HasRequired(c => c.Category)
                .WithMany(cc => cc.Cars)
                .HasForeignKey(c => c.CategoryId)
                .WillCascadeOnDelete(true);

            HasMany(c => c.PartBrands).WithMany(pb => pb.Cars).Map(ca =>
            {
                ca.MapLeftKey("CarId");
                ca.MapRightKey("PartBrandId");
                ca.ToTable("CarsPartBrands");
            });
        }
    }
}