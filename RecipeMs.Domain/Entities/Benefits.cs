using System.Collections.Generic;

namespace RecipeMs.Domain.Entities
{
    public class Benefit
    {
        public Benefit()
        {
            ConditionsHelped = new List<Condition>();
            Foods = new List<Food>();
        }

        public int BenefitId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Condition> ConditionsHelped { get; set; }
        public ICollection<Food> Foods { get; set; }
    }
}
