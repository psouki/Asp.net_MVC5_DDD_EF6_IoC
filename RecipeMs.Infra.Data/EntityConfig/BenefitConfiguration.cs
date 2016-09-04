using System.Data.Entity.ModelConfiguration;
using RecipeMs.Domain.Entities;

namespace RecipeMs.Infra.Data.EntityConfig
{
    public class BenefitConfiguration : EntityTypeConfiguration<Benefit>
    {
        public BenefitConfiguration()
        {
            HasKey(b => b.BenefitId);

            Property(b => b.Title)
                .IsRequired();

            Property(b => b.Description)
                .IsRequired()
                .HasMaxLength(1000);

            HasMany(b => b.ConditionsHelped)
                .WithMany(c=> c.ConditionsHelpers)
                .Map(bc =>
                {
                    bc.ToTable("BenefitCondition");
                    bc.MapLeftKey("BenefitId");
                    bc.MapRightKey("ConditionId");
                });

            HasMany(b => b.Foods)
                .WithMany(f=>f.Benefits)
                .Map(bf =>
                {
                    bf.ToTable("BenefitFood");
                    bf.MapLeftKey("BenefitId");
                    bf.MapRightKey("FoodId");
                });
        }
    }
}

