using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using RecipeMs.Domain.Interfaces.Repositories;
using RecipeMs.Domain.Interfaces.Services;

namespace RecipeMs.Domain.Services
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryBase<TEntity> _repository;

        public ServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.Repository<TEntity>();
        }

        public void Add(TEntity obj)
        {
            _repository.Add(obj);
        }

        public TEntity GetById(int id)
        {
            return _repository.GetById(id);
        }

        public ICollection<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public void Update(TEntity obj)
        {
            _repository.Update(obj);
        }

        public void Remove(TEntity obj)
        {
            _repository.Remove(obj);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Get(predicate);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate, ICollection<string> includes)
        {
            return _repository.Get(predicate, includes);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Find(predicate);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, ICollection<string> includes)
        {
            return _repository.Find(predicate, includes);
        }

        public void Complete()
        {
           _unitOfWork.Complete();
        }

        public void Dispose()
        {
           _unitOfWork.Dispose();
        }

       
    }
}
