﻿using System;
using System.Linq.Expressions;

namespace Okul.Data.Repository
{
	public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        void Delete(TEntity entity, bool softDelete = true);

        void Delete(int id);

        void Update(TEntity entity);

        TEntity GetById(int id);

        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);

        int Count(Expression<Func<TEntity, bool>> predicate = null);
    }
}

