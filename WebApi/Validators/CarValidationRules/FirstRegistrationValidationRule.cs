using System;
using Model;
using WebApi.Services;

namespace WebApi.Validators.CarValidationRules
{
    public class FirstRegistrationValidationRule : ICarValidationRule
    {
        private ICalendar _calendar;

        public FirstRegistrationValidationRule(ICalendar calendar)
        {
            _calendar = calendar;
        }

        public bool IsValid(Car car)
        {
            if (car.New)
            {
                return !car.FirstRegistration.HasValue;
            }

            return car.FirstRegistration.HasValue
                   && car.FirstRegistration.Value.Date <= _calendar.Today.Date;
        }
    }
}