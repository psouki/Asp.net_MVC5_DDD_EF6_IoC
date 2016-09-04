using System.Data.Entity.ModelConfiguration;
using RecipeMs.Domain.Entities;

namespace RecipeMs.Infra.Data.EntityConfig
{
    public class MyListConfiguration : EntityTypeConfiguration<MyList>
    {
        public MyListConfiguration()
        {
            HasKey(m => m.MyListId);

            Property(m => m.Name)
                .IsRequired();

            Property(m => m.Query)
                .IsRequired();
        }
    }
}
