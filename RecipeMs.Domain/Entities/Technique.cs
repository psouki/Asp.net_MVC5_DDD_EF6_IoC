namespace RecipeMs.Domain.Entities
{
    public class Technique
    {
        public int TechniqueId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Humidity { get; set; }
        public int Pace { get; set; }
        public int Difficulty { get; set; }
        public bool Heat { get; set; }
    }
}
