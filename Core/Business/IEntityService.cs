using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Business
{
    public interface IEntityService<T>
        where T:class,IEntity,new()
    {
        List<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        T GetById(int id);
    }
}
