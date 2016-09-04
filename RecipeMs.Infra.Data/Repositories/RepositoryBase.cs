using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using RecipeMs.Domain.Interfaces.Repositories;
using RecipeMs.Infra.Data.Context;

namespace RecipeMs.Infra.Data.Repositories
{
    public class RepositoryBase<TEntity> :  IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly RecipeMsContext Db;
        protected IDbSet<TEntity> DbSet;

        public RepositoryBase(RecipeMsContext db)
        {
            Db = db;
            DbSet = Db.Set<TEntity>();
        }

        public void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public TEntity GetById(int id)
        {
           return DbSet.Find(id);
        }

        public ICollection<TEntity> GetAll()
        {
            return DbSet.ToList(); 
        }

        public void Update(TEntity obj)
        {
            Db.Entry(obj).State = EntityState.Modified;
        }

        public void Remove(TEntity obj)
        {
            DbSet.Remove(obj);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).FirstOrDefault();
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate, ICollection<string> includes)
        {
            IQueryable<TEntity> query = DbSet;
            query = query.Where(predicate);
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            TEntity result = query.FirstOrDefault();
            
            return result;
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, ICollection<string> includes)
        {
            IQueryable<TEntity> query = DbSet;
            query = query.Where(predicate);
            query = includes.Aggregate(query, (current, include) => current.Include(include));

            IEnumerable<TEntity> result = query.ToList();
            return result;
        }

    }
}
