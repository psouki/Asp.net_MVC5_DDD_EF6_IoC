using System.Collections.Generic;

namespace RecipeMs.Domain.Entities
{
    public class Condition
    {
        public Condition()
        {
            ConditionsHelpers = new List<Benefit>();
        }

        public int ConditionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Symptoms { get; set; }
        public ICollection<Benefit> ConditionsHelpers { get; set; }
    }
}
