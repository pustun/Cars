using Model;

namespace WebApi.Validators.CarValidationRules
{
    public class TitleValidationRule : ICarValidationRule
    {
        public bool IsValid(Car car)
        {
            return !string.IsNullOrWhiteSpace(car.Title);
        }
    }
}