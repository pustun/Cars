using Model;

namespace WebApi.Validators.CarValidationRules
{
    public class MileageValidationRule : ICarValidationRule
    {
        public bool IsValid(Car car)
        {
            if (car.New)
            {
                return !car.Mileage.HasValue;
            }

            return car.Mileage.HasValue && car.Mileage.Value > 0;
        }
    }
}