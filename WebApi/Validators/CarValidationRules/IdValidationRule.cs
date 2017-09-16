using System;
using Model;

namespace WebApi.Validators.CarValidationRules
{
    public class IdValidationRule : ICarValidationRule
    {
        public bool IsValid(Car car)
        {
            return car.Id != Guid.Empty;
        }
    }
}