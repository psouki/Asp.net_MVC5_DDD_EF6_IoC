using System.Data.Entity.ModelConfiguration;
using RecipeMs.Domain.Entities;

namespace RecipeMs.Infra.Data.EntityConfig
{
    public class TechniqueConfiguration: EntityTypeConfiguration<Technique>
    {
        public TechniqueConfiguration()
        {
            HasKey(t => t.TechniqueId);

            Property(t => t.Name).IsRequired();
        }
    }
}
