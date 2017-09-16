using Model;

namespace WebApi.Validators.CarValidationRules
{
    public class FuelValidationRule : ICarValidationRule
    {
        public bool IsValid(Car car)
        {
            return car.Fuel != Fuel.None;
        }
    }
}