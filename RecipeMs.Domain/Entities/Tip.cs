
namespace RecipeMs.Domain.Entities
{
    public class Tip
    {
        public int TipId { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }

        public int StepId { get; set; }
        public Step Step { get; set; }
    }
}
