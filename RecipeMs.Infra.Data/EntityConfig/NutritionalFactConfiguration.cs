using System.Data.Entity.ModelConfiguration;
using RecipeMs.Domain.Entities;

namespace RecipeMs.Infra.Data.EntityConfig
{
    public class NutritionalFactConfiguration : EntityTypeConfiguration<NutritionalFact>
    {
        public NutritionalFactConfiguration()
        {
            HasKey(n => n.NutritionalFactId);

            Property(n => n.Footnote).HasMaxLength(300);

            Property(n => n.Value).HasPrecision(10, 3);

            HasRequired(n => n.FactDefinition);

            HasRequired(n => n.FoodStage)
                .WithMany(f => f.NutritionalFacts)
                .HasForeignKey(n => n.FoodStageId);
        }
    }
}
