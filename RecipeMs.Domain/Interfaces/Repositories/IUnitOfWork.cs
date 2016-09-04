using System;

namespace RecipeMs.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryBase<TEntity> Repository<TEntity>() where TEntity : class;
        void Complete();
    }
}