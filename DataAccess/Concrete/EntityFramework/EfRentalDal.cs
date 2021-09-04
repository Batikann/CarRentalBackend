using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.DTOs;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal:EfEntityRepositoryBase<Rental,CarRentalDBContext>,IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails(Expression<Func<Rental, bool>> filter = null)
        {
            using (CarRentalDBContext context=new CarRentalDBContext())
            {
                var result = from r in context.Rentals
                    join c in context.Cars on r.CarId equals c.CarId
                    join b in context.Brands on c.BrandId equals b.BrandId
                    join co in context.Colors on c.ColorId equals co.ColorId
                    join cu in context.Customers on r.CustomerId equals cu.CustomerId
                    join u in context.Users on cu.UserId equals u.UserId
                    select new RentalDetailDto
                    {
                        RentalID = r.Id,
                        CarId =c.CarId,
                        CompanyName = cu.CompanyName,
                        DailyPrice = c.DailyPrice,
                        Description = c.Description,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        ColorName = co.ColorName,
                        BrandName = b.BrandName,
                        RentDate = r.RentDate,
                        ReturnDate = r.ReturnDate
                    };
                return result.ToList();
            }

        }
    }
}
