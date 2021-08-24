using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public IResult Add(Car car)
        {
            if (car.CarName.Length >= 2 && car.DailyPrice > 0)
            {
                _carDal.Add(car);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IResult Delete(Car entity)
        {
            _carDal.Delete(entity);
            return new SuccessResult();

        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }

        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(x => x.CarId == id));
        }

        public IDataResult<List<CarDetailDto>>GetAllCarDetails()
        {
            if (DateTime.Now.Hour==20)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.SystemAlert);
            }
            return new SuccessDataResult<List<CarDetailDto>>( _carDal.GetCarDetails());
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails(int id)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails().Where(x => x.CarId == id)
                .ToList());
        }

        public IDataResult<List<CarDetailDto>> GetCarsByColorAndBrandId(int brandId, int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails()
                .Where(x => x.BrandId == brandId && x.ColorId == colorId).ToList());
        }


        public IDataResult<List<CarDetailDto>>GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<CarDetailDto>> (_carDal.GetCarDetails().Where(x=>x.BrandId==id).ToList());
        }

        public IDataResult<List<CarDetailDto>>GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails().Where(x=>x.ColorId==id).ToList());
        }

        public IResult Update(Car entity)
        {
            _carDal.Update(entity);
            return new SuccessResult();
        }
    }
}
