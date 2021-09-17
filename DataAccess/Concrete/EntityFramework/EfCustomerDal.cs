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
            using (CarRentalDBContext context = new CarRentalDBContext())
            {
                var result = from cu in context.Customers
                             join u in context.Users
                                 on cu.UserId equals u.UserId
                             select new CustomerDetailDto
                             {
                                 CustomerId = cu.CustomerId,
                                 Email = u.Email,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 CompanyName = cu.CompanyName,
                                 Findeks = cu.Findeks
                             };
                return result.ToList();
            }
        }

        public CustomerDetailDto GetCustomerDetailsByUserId(int userId)
        {
            using (CarRentalDBContext context = new CarRentalDBContext())
            {
                var result = from cu in context.Customers
                             join u in context.Users
                                 on cu.UserId equals u.UserId
                             where cu.UserId == userId
                             select new CustomerDetailDto
                             {
                                 CustomerId = cu.CustomerId,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 CompanyName = cu.CompanyName,
                                 Email = u.Email,
                                 Findeks = cu.Findeks
                             };
                return result.SingleOrDefault();
            }
        }

        public CustomerDetailDto GetCustomerDetailsDetailsById(int customerId)
        {
            using (CarRentalDBContext context = new CarRentalDBContext())
            {
                var result = from cu in context.Customers
                             join u in context.Users
                                 on cu.UserId equals u.UserId
                             where cu.CustomerId == customerId
                             select new CustomerDetailDto
                             {
                                 CustomerId = cu.CustomerId,
                                 CompanyName = cu.CompanyName,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 Email = u.Email,
                                 Findeks = cu.Findeks,

                             };

                return result.SingleOrDefault();
            }

        }
    }
}
