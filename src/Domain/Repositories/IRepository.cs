using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : IEntity
    {
        void Add(TEntity entity);

        IEnumerable<TEntity> All();
        void Delete(TEntity entity);
    }
}
