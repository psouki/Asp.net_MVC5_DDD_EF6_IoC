using System.Collections.Generic;
using RecipeMs.Domain.Entities;

namespace RecipeMs.Domain.Useful.Comparer
{
    public class NutritionalFactComparer : IEqualityComparer<NutritionalFact>
    {
        public bool Equals(NutritionalFact x, NutritionalFact y)
        {
            return x.NutritionalFactId == y.NutritionalFactId;
        }

        public int GetHashCode(NutritionalFact obj)
        {
            return obj.NutritionalFactId.GetHashCode();
        }
    }
}
