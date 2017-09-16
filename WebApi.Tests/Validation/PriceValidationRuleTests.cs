using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Model;
using NUnit.Framework;
using NUnit.Framework.Internal;
using WebApi.Validators.CarValidationRules;

namespace WebApi.Tests.Validation
{
    [TestFixture()]
    public class PriceValidationRuleTests
    {
        [Test]
        public void Should_return_false_if_price_zero()
        {
            // arrange
            var car = new Car();

            var sut = new PriceValidationRule();

            // act
            var isValid = sut.IsValid(car);

            // assert
            isValid.Should().BeFalse();
        }

        [Test]
        public void Should_return_false_if_price_negative()
        {
            // arrange
            var car = new Car {Price = -42};

            var sut = new PriceValidationRule();

            // act
            var isValid = sut.IsValid(car);

            // assert
            isValid.Should().BeFalse();
        }

        [Test]
        public void Should_return_true_if_price_positive()
        {
            // arrange
            var car = new Car { Price = 100500 };

            var sut = new PriceValidationRule();

            // act
            var isValid = sut.IsValid(car);

            // assert
            isValid.Should().BeTrue();
        }
    }
}
