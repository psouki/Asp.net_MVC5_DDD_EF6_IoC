using System.Data.Entity.ModelConfiguration;
using RecipeMs.Domain.Entities;

namespace RecipeMs.Infra.Data.EntityConfig
{
    public class RecipeConfiguration : EntityTypeConfiguration<Recipe>
    {
        public RecipeConfiguration()
        {
            HasKey(r => r.RecipeId);

            Property(r => r.Name)
               .IsRequired()
               .HasMaxLength(150);

            Property(r => r.Rating).HasPrecision(5, 2);

            Property(r => r.Dificulty).HasPrecision(5, 2);

            Property(r => r.Source).IsRequired();

            HasMany(r => r.Tags)
                .WithMany(t=>t.Recipes)
                .Map(rt =>
                {
                    rt.ToTable("RecipeTag");
                    rt.MapLeftKey("RecipeId");
                    rt.MapRightKey("TagId");
                });

            HasMany(r => r.Categories)
                .WithMany(c=>c.Recipes)
                .Map(rc =>
                {
                    rc.ToTable("RecipeCategory");
                    rc.MapLeftKey("RecipeId");
                    rc.MapRightKey("CategoryId");
                });
        }
    }
}
