using System;
using System.Data.Entity.ModelConfiguration;

namespace EntityFrameworkTutorial.EntityFramework
{
    public class EngineDetailsConfiguration : EntityTypeConfiguration<EngineDetails>
    {
        public EngineDetailsConfiguration()
        {
            HasKey(ed => ed.CarId);
            HasRequired(ed => ed.Car).WithOptional(c => c.EngineDetails).WillCascadeOnDelete(true);
        }
    }
}
