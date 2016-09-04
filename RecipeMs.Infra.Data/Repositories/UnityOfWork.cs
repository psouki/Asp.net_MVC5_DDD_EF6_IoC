using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using RecipeMs.Domain.Interfaces.Repositories;
using RecipeMs.Infra.Data.Context;

namespace RecipeMs.Infra.Data.Repositories
{
    
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RecipeMsContext _context;
        private bool _disposed;
        public Dictionary<Type, object>  Repositories = new Dictionary<Type, object>();

        public UnitOfWork()
        {
            _context = new RecipeMsContext();
        }

        public IRepositoryBase<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (Repositories.Keys.Contains(typeof(TEntity)))
            {
                return Repositories[typeof (TEntity)] as IRepositoryBase<TEntity>;
            }   

            IRepositoryBase<TEntity> repository = new RepositoryBase<TEntity>(_context);
            Repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
