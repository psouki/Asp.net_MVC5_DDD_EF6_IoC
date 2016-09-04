using System.Data.Entity.ModelConfiguration;
using RecipeMs.Domain.Entities;

namespace RecipeMs.Infra.Data.EntityConfig
{
    public class FoodStageConfiguration : EntityTypeConfiguration<FoodStage>
    {
        public FoodStageConfiguration()
        {
            HasKey(f => f.FoodStageId);

            Property(n => n.Initial).IsRequired();

            Property(n => n.Final).IsRequired();

            Property(n => n.RefusePercentage).HasPrecision(5, 2);

            Property(n => n.ProteinCalorieFactor).HasPrecision(5, 2);

            Property(n => n.FatCalorieFactor).HasPrecision(5, 2);

            Property(n => n.CarbCalorieFactor).HasPrecision(5, 2);

            HasRequired(n => n.Food)
               .WithMany(f => f.FoodStages)
               .HasForeignKey(n => n.FoodId);
        }
    }
}
