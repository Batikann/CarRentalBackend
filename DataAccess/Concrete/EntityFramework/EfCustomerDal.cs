using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, CarRentalDBContext>, ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomerDetails()
        {
            using (CarRentalDBContext context=new CarRentalDBContext())
            {
                var result = from cu in context.Customers
                    join u in context.Users
                        on cu.UserId equals u.UserId
                    select new CustomerDetailDto
                    {
                        CustomerId = cu.CustomerId, Email = u.Email, FirstName = u.FirstName, LastName = u.LastName,
                        UserId = u.UserId,CompanyName = cu.CompanyName
                    };
                return result.ToList();
            }
        }
    }
}
