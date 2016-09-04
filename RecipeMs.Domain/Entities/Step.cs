using System.Collections.Generic;

namespace RecipeMs.Domain.Entities
{
    public class Step
    {
        public Step()
        {
            Tips = new List<Tip>();
        }

        public int StepId { get; set; }
        public string Description { get; set; }
        public string PicturePath { get; set; }
        public ICollection<Tip> Tips { get; set; }

        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
