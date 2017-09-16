using FluentAssertions;
using Model;
using Moq;
using NUnit.Framework;
using WebApi.Services;
using WebApi.Validators.CarValidationRules;

namespace WebApi.Tests.Validation
{
    [TestFixture]
    public class FirstRegistrationValidationRuleTests
    {
        private FirstRegistrationValidationRule _sut;

        [OneTimeSetUp]
        public void FixtureSetup()
        {
            var today = 16.September(2017);
            var calendarMock = new Mock<ICalendar>();
            calendarMock.Setup(x => x.Today)
                .Returns(today);

            _sut = new FirstRegistrationValidationRule(calendarMock.Object);
        }

        [Test]
        public void FirstRegistration_should_be_null_for_new_cars()
        {
            // arrange
            var car = new Car {New = true};

            // act
            var isValid = _sut.IsValid(car);

            // assert
            isValid.Should().BeTrue();
        }

        [Test]
        public void FirstRegistartion_should_exist_for_used_cars()
        {
            // arrange
            var car = new Car {New = false, FirstRegistration = 12.May(2011)};

            // act
            var isValid = _sut.IsValid(car);

            // assert
            isValid.Should().BeTrue();
        }

        [Test]
        public void FirstRegistartion_should_be_in_past()
        {
            // arrange
            var car = new Car {New = false, FirstRegistration = 20.September(2017)};

            // act
            var isValid = _sut.IsValid(car);

            // assert
            isValid.Should().BeFalse();
        }
    }
}
