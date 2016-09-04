using System.Data.Entity.ModelConfiguration;
using RecipeMs.Domain.Entities;

namespace RecipeMs.Infra.Data.EntityConfig
{
    public class ConditionConfiguration : EntityTypeConfiguration<Condition>
    {
        public ConditionConfiguration()
        {
            HasKey(c => c.ConditionId);

            Property(c => c.Name)
                .IsRequired();

            Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(300);

            Property(c => c.Symptoms)
               .IsRequired()
               .HasMaxLength(300);

        }
    }
}
