using System.Data.Entity.ModelConfiguration;
using RecipeMs.Domain.Entities;

namespace RecipeMs.Infra.Data.EntityConfig
{
    public class CategoryConfiguration :EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            HasKey(c => c.CategoryId);

            Property(c => c.Name)
                .IsRequired();

            Property(c => c.Type)
                .IsRequired();
        }
    }
}
