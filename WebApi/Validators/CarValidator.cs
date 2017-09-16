using System;
using System.Collections.Generic;
using System.Linq;
using Model;

namespace WebApi.Validators
{
    public class CarValidator : ICarValidator
    {
        private readonly IEnumerable<ICarValidationRule> _validationRules;

        public CarValidator(IEnumerable<ICarValidationRule> validationRules)
        {
            _validationRules = validationRules;
        }

        public bool IsValid(Car car)
        {
            return _validationRules.All(x => x.IsValid(car));
        }
    }
}