using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using (TContext TContext = new TContext())
            {
                var addedEntity = TContext.Entry(entity);
                addedEntity.State = EntityState.Added;
                TContext.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext TContext = new TContext())
            {
                var deletedEntity = TContext.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                TContext.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext TContext = new TContext())
            {
                return TContext.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext TContext = new TContext())
            {
                return filter == null ? TContext.Set<TEntity>().ToList() : TContext.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext TContext = new TContext())
            {
                var updatedEntity = TContext.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                TContext.SaveChanges();
            }
        }
    }
}
