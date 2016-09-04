using System.Data.Entity.ModelConfiguration;
using RecipeMs.Domain.Entities;

namespace RecipeMs.Infra.Data.EntityConfig
{
    public class ValueSourceConfiguration : EntityTypeConfiguration<ValueSource>
    {
        public ValueSourceConfiguration()
        {
            HasKey(v => v.ValueSourceId);

            Property(v => v.Title).IsRequired();
            Property(v => v.Authors).IsRequired();
            Property(v => v.Journal).IsOptional();
        }
    }
}
