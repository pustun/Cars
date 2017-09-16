using Model;

namespace WebApi.Validators.CarValidationRules
{
    public class PriceValidationRule : ICarValidationRule
    {
        public bool IsValid(Car car)
        {
            return car.Price > 0;
        }
    }
}