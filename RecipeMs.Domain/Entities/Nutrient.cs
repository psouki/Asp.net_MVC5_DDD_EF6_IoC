namespace RecipeMs.Domain.Entities
{
    public class Nutrient
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal Value { get; set; }
        public decimal Percentage { get; set; }
        public bool IsTitle { get; set; }
        public bool ShowPercentage { get; set; }
        public int Type { get; set; }
        public int Order { get; set; }
    }
}
