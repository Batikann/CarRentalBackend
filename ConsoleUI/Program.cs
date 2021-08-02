using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //CarTest();
            //ColorTest();
            //BrandTest();

            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            var result = rentalManager.Add(new Rental { CarId = 11, CustomerId = 4, RentDate = DateTime.Now, ReturnDate = null });
            Console.WriteLine(result.Message);

           


        }

        private static void BrandTest()
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            foreach (var item in brandManager.GetAll().Data)
            {
                Console.WriteLine("{0}", item.BrandName);
            }
        }

        private static void ColorTest()
        {
            ColorManager colorManager = new ColorManager(new EfColorDal());
            foreach (var item in colorManager.GetAll().Data)
            {
                Console.WriteLine("{0}", item.ColorName);
            }
        }

        private static void CarTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            var result = carManager.GetCarDetails();
            if (result.Success == false)
            {
                Console.WriteLine(result.Message);
            }
            else
            {
                foreach (var cars in result.Data)
                {
                    Console.WriteLine("{0}-----{1}-----{2}----{3}", cars.CarName, cars.BrandName, cars.ColorName, cars.DailyPrice);
                }
            }
        }
    }
}
