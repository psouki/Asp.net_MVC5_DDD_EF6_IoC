using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using RecipeMs.Domain.Entities;
using RecipeMs.Infra.Data.EntityConfig;

namespace RecipeMs.Infra.Data.Context
{
    public class RecipeMsContext : DbContext
    {
        public RecipeMsContext() : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Benefit> Benefits { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<MyList> MyLists { get; set; }
        public DbSet<NutritionalFact> NutritionalFacts { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Tip> Tips { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<Technique> Techniques { get; set; }
        public DbSet<FactDefinition> FactDefinitions { get; set; }
        public DbSet<ValueSource> ValueSources { get; set; }
        public DbSet<FoodStage> FoodStages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "Id")
                .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            modelBuilder.Properties<string>()
                .Configure(p => p.IsOptional());

            modelBuilder.Properties<decimal>()
                .Configure(p=>p.HasPrecision(10,2));

            modelBuilder.Configurations.Add(new RecipeConfiguration());
            modelBuilder.Configurations.Add(new BenefitConfiguration());
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new ConditionConfiguration());
            modelBuilder.Configurations.Add(new FoodConfiguration());
            modelBuilder.Configurations.Add(new IngredientConfiguraton());
            modelBuilder.Configurations.Add(new MyListConfiguration());
            modelBuilder.Configurations.Add(new NutritionalFactConfiguration());
            modelBuilder.Configurations.Add(new StepConfiguration());
            modelBuilder.Configurations.Add(new TagConfiguration());
            modelBuilder.Configurations.Add(new TipConfiguration());
            modelBuilder.Configurations.Add(new MeasurementConfiguration());
            modelBuilder.Configurations.Add(new TechniqueConfiguration());
            modelBuilder.Configurations.Add(new FactDefinitionConfiguration());
            modelBuilder.Configurations.Add(new ValueSourceConfiguration());
            modelBuilder.Configurations.Add(new FoodStageConfiguration());
        }

    }
}
