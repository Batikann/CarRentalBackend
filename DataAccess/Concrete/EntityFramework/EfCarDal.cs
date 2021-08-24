using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarRentalDBContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (CarRentalDBContext context=new CarRentalDBContext())
            {
                var result = from p in context.Cars
                             join c in context.Colors on p.ColorId equals c.ColorId
                             join b in context.Brands on p.BrandId equals b.BrandId
                             select new CarDetailDto { CarName = p.CarName,
                                 BrandName = b.BrandName, 
                                 ColorName = c.ColorName, 
                                 DailyPrice = p.DailyPrice,
                                 Description = p.Description,
                                 ModelYear = p.ModelYear,
                                 CarId = p.CarId,
                                 BrandId =b.BrandId,
                                 ColorId = c.ColorId,
                                 ImagePath = (from i in context.CarImages where i.CarId ==p.CarId select i.ImagePath).FirstOrDefault()
                             };

                return result.ToList();
            }
        }
    }
}
