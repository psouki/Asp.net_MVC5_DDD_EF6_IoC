using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RecipeMs.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity: class 
    {
        void Add(TEntity obj);
        TEntity GetById(int id);
        ICollection<TEntity> GetAll();
        void Update(TEntity obj);
        void Remove(TEntity obj);
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(Expression<Func<TEntity, bool>> predicate, ICollection<string> includes);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, ICollection<string> includes);
    }
}
