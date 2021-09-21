using Core.Business;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IRentalService:IEntityService<Rental>
    {
        IDataResult<List<RentalDetailDto>> GetRentalDetails();
        IDataResult<List<RentalDetailDto>> GetRentalByCustomerId(int customerId);
        IDataResult<List<RentalDetailDto>> GetRentalByCarId(int carId);
        IResult CheckAvailableDate(Rental rental);
        IResult RentalCarControl(int id);
        IResult EndRental(Rental rental);
        IResult IsCarAvailable(Rental rental);
         IResult CheckCarRentalStatus(Rental rental);
    }
}
