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

        public RentalManager(IRentalDal rentalDal, ICarService carService)
        {
            _rentalDal = rentalDal;
            _carService = carService;
        }

        public IResult Add(Rental rental)
        {
            var results = _rentalDal.GetAll(re => re.CarId == rental.CarId);
            foreach (var result in results)
            {
                if (result.ReturnDate == null || (rental.RentDate >= result.RentDate && rental.RentDate <= result.ReturnDate) ||
                    (rental.ReturnDate >= result.RentDate && rental.RentDate <= result.ReturnDate))
                {
                    return new ErrorResult(Messages.RentalNotAdded);
                }
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);

        }

        public IDataResult<List<RentalDetailDto>> GetRentalByCarId(int carId)
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(x => x.CarId == carId));
        }

        public IResult CheckAvailableDate(Rental rental)
        {
            var result = _rentalDal.GetRentalDetails(r => r.CarId == rental.CarId).Where(r =>
                ((r.RentDate == rental.RentDate) && (r.ReturnDate == rental.ReturnDate)) ||
                ((rental.RentDate >= r.RentDate) && (rental.RentDate <= r.ReturnDate)) ||
                ((rental.ReturnDate >= r.RentDate) && (rental.ReturnDate <= r.ReturnDate))
            ).ToList();
            if (result.Count > 0)
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

        public IDataResult<List<RentalDetailDto>> GetRentalByCustomerId(int customerId)
        {
            return new SuccessDataResult<List<RentalDetailDto>>(
                _rentalDal.GetRentalDetails(x => x.CustomerId == customerId));
        }

        public IResult RentalCarControl(int id)
        {
            var result = _rentalDal.GetRentalDetails(x => x.CarId == id).Any();
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

        public IResult EndRental(Rental rental)
        {
            var result = _rentalDal.GetAll();
            var updateRental = result.LastOrDefault();
            if (updateRental.ReturnDate != null && updateRental.RentDate < DateTime.Now && updateRental.ReturnDate > DateTime.Now)
            {
                updateRental.ReturnDate = DateTime.Now;
                _rentalDal.Update(updateRental);
                return new SuccessResult(Messages.SuccessRentalUpdate);
            }

            return new ErrorResult(Messages.ErrorRentalUpdate);
        }

        public IResult IsCarAvailable(Rental rental)
        {
            var result = _rentalDal.GetAll(r => r.CarId == rental.CarId);

            if (result.Any(r => r.RentDate != null && r.ReturnDate == null))
            {
                return new ErrorResult(Messages.CarIsNotAvailable);
            }
            else
            {
                return new SuccessResult();
            }
        }

        public IResult CheckCarRentalStatus(Rental rental)
        {
            var results = _rentalDal.GetAll(x => x.CarId == rental.CarId);
            foreach (var result in results)
            {
                if (result.ReturnDate == null || (rental.RentDate >= result.RentDate && rental.RentDate <= result.ReturnDate) ||
                    (rental.ReturnDate >= result.RentDate && rental.RentDate <= result.ReturnDate))
                {
                    return new ErrorResult();
                }
            }

            return new SuccessResult();
        }

    }
}
