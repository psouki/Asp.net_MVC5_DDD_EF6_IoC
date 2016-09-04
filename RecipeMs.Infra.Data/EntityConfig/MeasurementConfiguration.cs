using System.Data.Entity.ModelConfiguration;
using RecipeMs.Domain.Entities;

namespace RecipeMs.Infra.Data.EntityConfig
{
    public class MeasurementConfiguration : EntityTypeConfiguration<Measurement>
    {
        public MeasurementConfiguration()
        {
            HasKey(m => m.MeasurementId);

            Property(m => m.HouseholdUnit).IsRequired();
            Property(m => m.PreciseUnit).IsRequired();
        }
    }
}
