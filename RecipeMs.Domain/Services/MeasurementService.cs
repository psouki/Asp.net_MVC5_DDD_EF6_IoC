using RecipeMs.Domain.Entities;
using RecipeMs.Domain.Interfaces.Repositories;
using RecipeMs.Domain.Interfaces.Services;

namespace RecipeMs.Domain.Services
{
    public class MeasurementService : ServiceBase<Measurement>, IMeasurementService
    {
        public MeasurementService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
