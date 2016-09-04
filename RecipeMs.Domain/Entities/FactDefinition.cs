namespace RecipeMs.Domain.Entities
{
    public class FactDefinition
    {
        public int FactDefinitioId { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Unit { get; set; }
        public int DecimalRoundNumber { get; set; }
        public string ShowGroup { get; set; }
        public int Type { get; set; }
        public int Order { get; set; }
        public bool IsPrincipal { get; set; }
        public bool IsTitle { get; set; }
        public decimal DailyValue { get; set; }
        public int UsdaNutriRefId { get; set; }
    }
}
