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
        IResult CheckAvailableDate(Rental rental);
        IResult RentalCarControl(int id);
    }
}
