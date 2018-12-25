using System.Collections.Generic;

namespace SpawnTraffic.Common.Repository.Interfaces
{
    public interface IRepositoryBase<TEntity, in TId> where TEntity : class
    {
        TEntity Get(TId id);

        List<TEntity> Get();

        TEntity Insert(TEntity entity);

        void Delete(TId id);

        void SaveChanges();
    }
}