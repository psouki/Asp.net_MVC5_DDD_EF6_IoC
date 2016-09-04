using System.Collections.Generic;
using Ninject;

namespace RecipeMs.Infra.ImportData.Interfaces
{
    public interface IClassConvertion<TIn, TOut> where TIn : class where TOut: class 
    {
        ICollection<TOut> ConverterEntities(ICollection<TIn> classesToConvert);
        void Save(ICollection<TOut> classToSave, IKernel kernel);
    }
}
