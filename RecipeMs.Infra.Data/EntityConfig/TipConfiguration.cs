using System.Data.Entity.ModelConfiguration;
using RecipeMs.Domain.Entities;

namespace RecipeMs.Infra.Data.EntityConfig
{
    public class TipConfiguration : EntityTypeConfiguration<Tip>
    {
        public TipConfiguration()
        {
            HasKey(t => t.TipId);

            Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(300);

            Property(t => t.Source)
                .IsRequired();

            HasRequired(t => t.Step)
                .WithMany(ti => ti.Tips)
                .HasForeignKey(t => t.StepId);

        }
    }
}
