﻿using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Repositories
{
    public class Repository<TEntity>:IRepository<TEntity>
        where TEntity:IEntity
    {
        private readonly List<TEntity> _list=new List<TEntity>();
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
