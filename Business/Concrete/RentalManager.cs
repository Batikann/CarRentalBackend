using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.DTOs;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        private ICarService _carService;

        public RentalManager(IRentalDal rentalDal,ICarService carService)
        {
            _rentalDal = rentalDal;
            _carService = carService;
        }

        public IResult Add(Rental entity)
        {
            var resultToCheckRented = _rentalDal.GetRentalDetails(
                r => r.CarId == entity.CarId && DateTime.Compare(entity.RentDate, (DateTime)r.ReturnDate) < 0);
            if (resultToCheckRented.Count > 0)
            {
                return new ErrorResult(Messages.RentalNotAdded);
            }
            _rentalDal.Add(entity);
            return new SuccessResult(Messages.RentalAdded);

        }

        public IResult CheckAvailableDate(Rental rental)
        {
            var result = _rentalDal.GetRentalDetails(r => r.CarId == rental.CarId).Where(r =>
                ((r.RentDate == rental.RentDate) && (r.ReturnDate == rental.ReturnDate)) ||
                ((rental.RentDate >= r.RentDate) && (rental.RentDate <= r.ReturnDate)) ||
                ((rental.ReturnDate >= r.RentDate) && (rental.ReturnDate <= r.ReturnDate))
            ).ToList();
            if (result.Count>0)
            {
                string errorMessage = "This car Already Between" + result[0].RentDate + "and" + result[0].ReturnDate +
                                      ".";
                return new ErrorResult(errorMessage);
            }

            return new SuccessResult("Where Koşullarına Takılmadı.");
        }

        public IResult Delete(Rental entity)
        {
            _rentalDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(x => x.Id == id));
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails());
        }

        public IResult RentalCarControl(int id)
        {
            var result = _rentalDal.GetRentalDetails(x => x.CarId == id && x.RentDate == null).Any();
            if (result)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }

        public IResult Update(Rental entity)
        {
            _rentalDal.Update(entity);
            return new SuccessResult();
        }
    }
}
