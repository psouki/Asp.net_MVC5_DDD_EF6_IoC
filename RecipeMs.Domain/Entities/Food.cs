using System.Collections.Generic;

namespace RecipeMs.Domain.Entities
{
    public class Food
    {
        public Food()
        {
            Benefits = new List<Benefit>();
            FoodStages = new List<FoodStage>();
        }

        public int FoodId { get; set; }
        public string Name { get; set; }
        public int FoodGroup { get; set; }
        public string FoodSubgroup { get; set; }

        public ICollection<Benefit> Benefits { get; set; }
        public ICollection<FoodStage> FoodStages { get; set; }
    }
}
