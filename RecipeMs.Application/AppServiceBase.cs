using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;
using RecipeMs.Application.Interfaces;
using RecipeMs.Application.Useful;
using RecipeMs.CrossCutting.Common.Query;
using RecipeMs.Domain.Interfaces.Services;

namespace RecipeMs.Application
{
    public class AppServiceBase<TEntity> : IAppServiceBase where TEntity : class
    {
        private readonly IServiceBase<TEntity> _service;

        public AppServiceBase(IServiceBase<TEntity> service)
        {
            _service = service;
        }

        public void Add(string data)
        {
            if (string.IsNullOrEmpty(data)) return;

            TEntity entity = JsonConvert.DeserializeObject<TEntity>(data);
            _service.Add(entity);
            _service.Complete();
        }

        public string GetById(int id)
        {
            TEntity entity = _service.GetById(id);
            string result = entity !=null ? JsonHelper<TEntity>.Serialize(entity) : string.Empty;
            return result;
        }

        public string GetAll()
        {
            IEnumerable<TEntity> entities = _service.GetAll();
            string result = entities.Any() ? JsonHelper<TEntity>.Serialize(entities) : string.Empty;
            return result;
        }

        public void Update(string data)
        {
            if (string.IsNullOrEmpty(data)) return;

            TEntity entity = JsonConvert.DeserializeObject<TEntity>(data);
            _service.Update(entity);
            _service.Complete();
        }

        public void Remove(int id)
        {
            TEntity entity = _service.GetById(id);
            _service.Remove(entity);
            _service.Complete();
        }

        public string Get(ICollection<QueryFilter> filters)
        {
            Expression<Func<TEntity, bool>> query = ExpressionBuilder.GetExpression<TEntity>(filters);
            TEntity entity = _service.Get(query);
            string result = entity != null ? JsonHelper<TEntity>.Serialize(entity) : string.Empty;
            return result;
        }

        public string Find(ICollection<QueryFilter> filters)
        {
            Expression<Func<TEntity, bool>> query = ExpressionBuilder.GetExpression<TEntity>(filters);
            IEnumerable<TEntity> entities = _service.Find(query).ToList();
            string result = entities.Any() ? JsonHelper<TEntity>.Serialize(entities) : string.Empty;
            return result;
        }
    }
}
