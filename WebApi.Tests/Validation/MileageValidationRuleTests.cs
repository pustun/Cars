using FluentAssertions;
using Model;
using NUnit.Framework;
using WebApi.Validators.CarValidationRules;

namespace WebApi.Tests.Validation
{
    [TestFixture]
    public class MileageValidationRuleTests
    {
        [Test]
        public void Mileage_should_be_null_for_new_cars()
        {
            // arrange
            var car = new Car {New = true};

            var sut = new MileageValidationRule();

            // act
            var isValid = sut.IsValid(car);

            // assert
            isValid.Should().BeTrue();
        }

        [Test]
        public void Mileage_should_be_specified_for_used_cars()
        {
            // arrange
            var car = new Car {New = false, Mileage = 20000};

            var sut = new MileageValidationRule();

            // act
            var isValid = sut.IsValid(car);

            // assert
            isValid.Should().BeTrue();
        }

        [Test]
        public void Should_return_false_if_mileage_zero()
        {
            // arrange
            var car = new Car {New = false, Mileage = 0};

            var sut = new MileageValidationRule();

            // act
            var isValid = sut.IsValid(car);

            // assert
            isValid.Should().BeFalse();
        }

        [Test]
        public void Should_return_false_if_mileage_negative()
        {
            // arrange
            var car = new Car { New = false, Mileage = -500 };

            var sut = new MileageValidationRule();

            // act
            var isValid = sut.IsValid(car);

            // assert
            isValid.Should().BeFalse();
        }

        [Test]
        public void Should_return_true_if_mileage_positive()
        {
            // arrange
            var car = new Car { New = false, Mileage = 50000 };

            var sut = new MileageValidationRule();

            // act
            var isValid = sut.IsValid(car);

            // assert
            isValid.Should().BeTrue();
        }
    }
}
