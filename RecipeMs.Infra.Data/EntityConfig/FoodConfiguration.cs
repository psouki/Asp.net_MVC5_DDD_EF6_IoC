using System.Data.Entity.ModelConfiguration;
using RecipeMs.Domain.Entities;

namespace RecipeMs.Infra.Data.EntityConfig
{
    public class FoodConfiguration : EntityTypeConfiguration<Food>
    {
        public FoodConfiguration()
        {
            HasKey(f => f.FoodId);

            Property(f => f.Name).IsRequired();

            Property(f => f.FoodGroup).IsRequired();
        }
    }
}
