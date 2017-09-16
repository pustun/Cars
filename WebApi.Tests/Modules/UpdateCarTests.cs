using System;
using FluentAssertions;
using Model;
using Moq;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using WebApi.Validators;

namespace WebApi.Tests.Modules
{
    [TestFixture]
    public class UpdateCarTests : CarModuleTestsBase
    {
        [Test]
        public void Should_accept_car_for_update()
        {
            // arrange
            var carId = Guid.NewGuid();
            var carToUpdate = new Car { Id = carId };

            var browser = CreateDefaultBrowser();

            // act
            var response = browser.Put($"/cars/{carId}", with => with.JsonBody(carToUpdate));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void Should_validate_car()
        {
            // arrange
            var validatorMock = new Mock<ICarValidator>();
            validatorMock.Setup(x => x.IsValid(It.IsAny<Car>()))
                .Returns(true);

            var carId = Guid.NewGuid();
            var car = new Car {Id = carId};

            var browser = CreateBrowserWithValidator(validatorMock.Object);

            // act
            browser.Put($"/cars/{carId}", with => with.JsonBody(car));

            // assert
            validatorMock.Verify(x => x.IsValid(It.IsAny<Car>()));
        }

        [Test]
        public void Should_return_BadRequest_if_car_not_valid()
        {
            // arrange
            var validatorMock = new Mock<ICarValidator>();
            validatorMock.Setup(x => x.IsValid(It.IsAny<Car>()))
                .Returns(false);

            var carId = Guid.NewGuid();
            var car = new Car {Id = carId};

            var browser = CreateBrowserWithValidator(validatorMock.Object);

            // act
            var response = browser.Put($"/cars/{carId}", with => with.JsonBody(car));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
