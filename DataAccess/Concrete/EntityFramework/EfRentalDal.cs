using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal:EfEntityRepositoryBase<Rental,CarRentalDBContext>,IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (CarRentalDBContext context=new CarRentalDBContext())
            {
                var result = from r in context.Rentals
                    join c in context.Cars on r.CarId equals c.CarId
                    join cu in context.Customers on r.CustomerId equals cu.CustomerId
                    join u in context.Users on cu.UserId equals u.UserId
                    select new RentalDetailDto
                    {
                        RentalID = r.Id, CarName = c.CarName, CustomerName = u.FirstName + " " + u.LastName,
                        RentDate = r.RentDate, ReturnDate = r.ReturnDate
                    };
                return result.ToList();
            }

        }
    }
}
