using FluentAssertions;
using Model;
using NUnit.Framework;
using WebApi.Validators.CarValidationRules;

namespace WebApi.Tests.Validation
{
    [TestFixture]
    public class FuelValidationRuleTests
    {
        [Test]
        public void Shoul_return_false_if_fuel_not_specified()
        {
            // arrange
            var car = new Car();

            var sut = new FuelValidationRule();

            // act
            var isValid = sut.IsValid(car);

            // assert
            isValid.Should().BeFalse();
        }

        [Test]
        public void Shoul_return_true_if_fuel_specified()
        {
            // arrange
            var car = new Car {Fuel = Fuel.Diesel};

            var sut = new FuelValidationRule();

            // act
            var isValid = sut.IsValid(car);

            // assert
            isValid.Should().BeTrue();
        }
    }
}
