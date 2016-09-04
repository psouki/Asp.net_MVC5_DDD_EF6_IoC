using System.Data.Entity.ModelConfiguration;
using RecipeMs.Domain.Entities;

namespace RecipeMs.Infra.Data.EntityConfig
{
    public class StepConfiguration : EntityTypeConfiguration<Step>
    {
        public StepConfiguration()
        {
            HasKey(s => s.StepId);

            Property(s => s.Description)
                .IsRequired()
                .HasMaxLength(300);

            HasRequired(s => s.Recipe)
                .WithMany(r=>r.Steps)
                .HasForeignKey(s => s.RecipeId);
        }
    }
}
