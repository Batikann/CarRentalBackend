using Core.Business;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface ICustomerService:IEntityService<Customer>
    {
        IDataResult<List<CustomerDetailDto>> GetCustomersDetails();
    }
}
