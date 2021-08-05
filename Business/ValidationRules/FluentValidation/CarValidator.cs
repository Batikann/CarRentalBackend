using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator:AbstractValidator<Car>
    {
        public CarValidator()
        {

            RuleFor(x => x.BrandId).NotEmpty();
            RuleFor(x => x.ColorId).NotEmpty();
            RuleFor(x => x.CarName).NotEmpty();
            RuleFor(x => x.DailyPrice).NotEmpty();
            RuleFor(x => x.DailyPrice).GreaterThan(0);
            RuleFor(x => x.Description).MinimumLength(20);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.ModelYear).NotEmpty();
        }
    }
}
