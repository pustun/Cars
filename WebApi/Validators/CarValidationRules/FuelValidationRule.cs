using System;
using Model;

namespace WebApi.Validators.CarValidationRules
{
    public class FuelValidationRule : ICarValidationRule
    {
        public bool IsValid(Car car)
        {
            return Enum.IsDefined(typeof(Fuel), car.Fuel)
                    && car.Fuel != Fuel.None;
        }
    }
}