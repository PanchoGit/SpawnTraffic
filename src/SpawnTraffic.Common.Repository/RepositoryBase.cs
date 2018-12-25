using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SpawnTraffic.Common.Repository.Interfaces;

namespace SpawnTraffic.Common.Repository
{
    public abstract class RepositoryBase<TEntity, TId> : IRepositoryBase<TEntity, TId> where TEntity : class
    {
        protected DbSet<TEntity> Set;

        protected DbContext Context { get; set; }

        protected RepositoryBase(DbContext context)
        {
            SetDbContext(context);
        }

        public void SetDbContext(DbContext context)
        {
            Context = context;

            Set = context.Set<TEntity>();
        }

        public TEntity Get(TId id)
        {
            return Set.Find(id);
        }

        public List<TEntity> Get()
        {
            return Set.ToList();
        }

        public TEntity Insert(TEntity entity)
        {
            Set.Add(entity);

            Context.SaveChanges();

            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            Set.Update(entity);

            Context.SaveChanges();

            return entity;
        }

        public void Delete(TId id)
        {
            Set.Remove(Set.Find(id));

            Context.SaveChanges();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
