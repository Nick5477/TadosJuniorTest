using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace Domain.Repositories
{
    class Repository<TEntity>:IRepository<TEntity>
        where TEntity:IEntity
    {
        private readonly List<TEntity> _list;
        public void Add(TEntity entity)
        {
            _list.Add(entity);
        }

        public IEnumerable<TEntity> All()
        {
            return _list;
        }

        public void Delete(TEntity entity)
        {
            _list.Remove(entity);
        }
    }
}
