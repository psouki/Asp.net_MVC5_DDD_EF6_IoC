namespace RecipeMs.Domain.Entities
{
    public class NutritionalFact
    {
        public int NutritionalFactId { get; set; }
        public decimal Value { get; set; }
        public string Footnote { get; set; }

        public int FoodStageId { get; set; }
        public FoodStage FoodStage { get; set; }

        public int FactDefinitioId { get; set; }
        public FactDefinition FactDefinition { get; set; }

        public int ValueSourceId { get; set; }
        public ValueSource ValueSource { get; set; }
    }
}
