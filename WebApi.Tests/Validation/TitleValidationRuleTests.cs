using FluentAssertions;
using Model;
using NUnit.Framework;
using WebApi.Validators.CarValidationRules;

namespace WebApi.Tests.Validation
{
    [TestFixture()]
    public class TitleValidationRuleTests
    {
        [Test]
        public void Should_return_true_if_title_specified()
        {
            // arrange
            var car = new Car {Title = "Audi A4"};

            var sut = new TitleValidationRule();

            // act
            var isValid = sut.IsValid(car);

            // assert
            isValid.Should().BeTrue();
        }

        [Test]
        public void Should_return_false_if_title_not_specified()
        {
            // arrange
            var car = new Car();

            var sut = new TitleValidationRule();

            // act
            var isValid = sut.IsValid(car);

            // assert
            isValid.Should().BeFalse();
        }

        [Test]
        public void Should_return_false_if_title_empty()
        {
            // arrange
            var car = new Car {Title = string.Empty};

            var sut = new TitleValidationRule();

            // act
            var isValid = sut.IsValid(car);

            // assert
            isValid.Should().BeFalse();
        }

        [Test]
        public void Should_return_false_if_title_white_space()
        {
            // arrange
            var car = new Car { Title = "    " };

            var sut = new TitleValidationRule();

            // act
            var isValid = sut.IsValid(car);

            // assert
            isValid.Should().BeFalse();
        }
    }
}
