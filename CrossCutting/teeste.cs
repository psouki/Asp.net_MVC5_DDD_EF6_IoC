using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeMs.Domain.Entities;

namespace CrossCutting
{
    public class Teete
    {
        public class MyClassComparer : IEqualityComparer<NutritionalFact>
        {
            public bool Equals(NutritionalFact x, NutritionalFact y)
            {
                return x.Id== y.Id;
            }

            public int GetHashCode(NutritionalFact obj)
            {
                return obj.Id.GetHashCode();
            }
        }
    }
}
