using System.Data.Entity.ModelConfiguration;
using RecipeMs.Domain.Entities;

namespace RecipeMs.Infra.Data.EntityConfig
{
    public class FactDefinitionConfiguration :EntityTypeConfiguration<FactDefinition>
    {
        public FactDefinitionConfiguration()
        {
            HasKey(f => f.FactDefinitioId);

            Property(f => f.Name).IsRequired();
            Property(f => f.Unit).IsRequired();

            Property(d => d.DailyValue).HasPrecision(8, 3);
        }
    }
}
