using System;
using FluentAssertions;
using Model;
using NUnit.Framework;
using WebApi.Validators.CarValidationRules;

namespace WebApi.Tests.Validation
{
    [TestFixture]
    public class IdValidationRuleTests
    {
        [Test]
        public void Should_return_true_if_id_specified()
        {
            // arrange
            var car = new Car {Id = Guid.NewGuid()};

            var sut = new IdValidationRule();

            // act
            var isValid = sut.IsValid(car);

            // assert
            isValid.Should().BeTrue();
        }

        [Test]
        public void Should_return_false_if_id_is_empty()
        {
            // arrange
            var car = new Car { Id = Guid.Empty };

            var sut = new IdValidationRule();

            // act
            var isValid = sut.IsValid(car);

            // assert
            isValid.Should().BeFalse();
        }
    }
}
