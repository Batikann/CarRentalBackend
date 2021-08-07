using Business.Abstract;
using Business.Constants;
using Core.Business;
using Core.Utilities.Helper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        ICarService _carService;

        private readonly string DefaultImage = "default.jpg";
        public CarImageManager(ICarImageDal carImageDal, ICarService carService)
        {
            _carImageDal = carImageDal;
            _carService = carService;
        }

        public IResult Add(IFormFile image, CarImage img)
        {
            var result = BusinessRules.Run(CheckIfCarIsExists((img.CarId)),
                CheckIfImageNumberLimitForCar(img.CarId),
                CheckIfFileExtensionValid(image.FileName));

            if (result != null)
            {
                return result;
            }

            img.ImagePath = FileOperationsHelper.Add(image);
            img.Date = DateTime.Now;
            _carImageDal.Add(img);
            return new SuccessResult(Messages.AddImageSuccessful);
        }

        public IResult Delete(CarImage img)
        {
            IResult result = BusinessRules.Run(CheckIfImagePathIsExists(img.ImagePath));

            if (result != null)
            {
                return result;
            }

            _carImageDal.Delete(img);
            FileOperationsHelper.Delete(img.ImagePath);
            return new SuccessResult(Messages.DeleteImage);
        }

        public IDataResult<CarImage> FindByID(int Id)
        {
            CarImage img = new CarImage();
            if (_carImageDal.GetAll().Any(x => x.Id == Id))
            {
                img = _carImageDal.GetAll().FirstOrDefault(x => x.Id == Id);
                return new SuccessDataResult<CarImage>(img);
            }
            return new ErrorDataResult<CarImage>();

        }

        public IDataResult<CarImage> Get(CarImage img)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(x => x.Id == img.Id));
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<List<CarImage>> GetCarListByCarID(int carID)
        {
            if (!_carImageDal.GetAll().Any(x => x.CarId == carID))
            {
                List<CarImage> img = new List<CarImage>()
                {
                    new CarImage
                    {
                        CarId=carID,
                        ImagePath=DefaultImage
                    }
                };
                return new SuccessDataResult<List<CarImage>>(img);
            }

            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(x => x.CarId == carID));
        }

        public IResult Update(IFormFile image, CarImage img)
        {
            var result = BusinessRules.Run(CheckIfImageIsExists(img.Id),
                                                  CheckIfFileExtensionValid(image.FileName));
            if (result != null)
            {
                return result;
            }

            var carImage = _carImageDal.Get(x => x.Id == img.Id);
            carImage.Date = DateTime.Now;
            carImage.ImagePath = FileOperationsHelper.Add(image);
            FileOperationsHelper.Delete(img.ImagePath);
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.UpdateImageSuccessful);
        }


        private IResult CheckIfCarIsExists(int carId)
        {
            if (!_carService.GetAll().Data.Any(x => x.CarId == carId))
            {
                return new ErrorResult(Messages.NotExistCar);
            }
            return new SuccessResult();
        }

        private IResult CheckIfFileExtensionValid(string file)
        {
            if (!Regex.IsMatch(file, @"([A-Za-z0-9\-]+)\.(png|PNG|gif|GIF|jpg|JPG|jpeg|JPEG)"))
            {
                return new ErrorResult(Messages.InvalidFileExtension);
            }
            return new SuccessResult();
        }

        private IResult CheckIfImagePathIsExists(string path)
        {
            if (!_carImageDal.GetAll().Any(x => x.ImagePath == path))
            {
                return new ErrorResult(Messages.NotExist);
            }
            return new SuccessResult();
        }

        private IResult CheckIfImageNumberLimitForCar(int carId)
        {
            if (_carImageDal.GetAll(x => x.CarId == carId).Count == 5)
            {
                return new ErrorResult(Messages.ImageNumberLimitExceeded);
            }
            return new SuccessResult();
        }

        private IResult CheckIfImageIsExists(int id)
        {
            if (!_carImageDal.GetAll().Any(x => x.Id == id))
            {
                return new ErrorResult(Messages.NotExist);
            }
            return new SuccessResult();
        }
    }
}
