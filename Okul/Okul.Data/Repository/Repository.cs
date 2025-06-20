using System;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Okul.Data.Models;

namespace Okul.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly OkulDbContext _db;
        private readonly DbSet<TEntity> _dbSet;
            
        public Repository(OkulDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(TEntity entity, bool softDelete = true)
        {
            // Eğer soft delete alanı varsa, yansıtılabilir; yoksa doğrudan silinir.
            if (softDelete)
            {
                var prop = typeof(TEntity).GetProperty("IsDeleted");
                if (prop != null)
                {
                    prop.SetValue(entity, true);
                    _dbSet.Update(entity);
                    return;
                }
            }

            _dbSet.Remove(entity);
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
                Delete(entity);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = _dbSet.AsQueryable();

            // Soft delete alanı varsa, sadece silinmemiş olanları filtrele
            var prop = typeof(TEntity).GetProperty("IsDeleted");
            if (prop != null)
            {
                query = query.Where(e => EF.Property<bool>(e, "IsDeleted") == false);
            }

            return predicate != null ? query.Where(predicate) : query;
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}

