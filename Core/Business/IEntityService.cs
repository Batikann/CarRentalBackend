using Core.Entities;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Abstract;

namespace Core.Business
{
    public interface IEntityService<T>
        where T:class,IEntity,new()
    {
        IDataResult<List<T>> GetAll();
        IResult Add(T entity);
        IResult Update(T entity);
        IResult Delete(T entity);
        IDataResult<T> GetById(int id);
    }
}
