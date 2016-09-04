namespace RecipeMs.Domain.Entities
{
    public class Measurement
    {
        public int MeasurementId { get; set; }
        public string HouseholdUnit { get; set; }
        public string PreciseUnit { get; set; }
        public int PreciseUnitValue { get; set; }
    }
}
