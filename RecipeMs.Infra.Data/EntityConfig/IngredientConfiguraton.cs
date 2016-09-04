using System.Data.Entity.ModelConfiguration;
using RecipeMs.Domain.Entities;

namespace RecipeMs.Infra.Data.EntityConfig
{
    public class IngredientConfiguraton : EntityTypeConfiguration<Ingredient>
    {
        public IngredientConfiguraton()
        {
            HasKey(i => i.IngredientId);

            HasRequired(i => i.Food);
            
            Property(i => i.Importance).IsRequired();

            Property(i => i.Quantity).HasPrecision(8, 2);

            HasRequired(i => i.Recipe)
                .WithMany(r => r.Ingredients)
                .HasForeignKey(i => i.RecipeId);
        }
    }
}
