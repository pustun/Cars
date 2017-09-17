using System;
using Datalayer;
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

            var browser = Browser().Build();

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

            var browser = Browser().WithValidator(validatorMock.Object)
                                    .Build();

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

            var browser = Browser().WithValidator(validatorMock.Object)
                                    .Build();

            // act
            var response = browser.Put($"/cars/{carId}", with => with.JsonBody(car));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public void Should_call_repository()
        {
            // arrange
            var repositoryMock = new Mock<ICarsRepository>();
            repositoryMock.Setup(x => x.Update(It.IsAny<Car>()));

            var carId = Guid.NewGuid();
            var car = new Car {Id = carId};

            var browser = Browser().WithRepository(repositoryMock.Object)
                                    .Build();

            // act
            browser.Put($"/cars/{carId}", with => with.JsonBody(car));

            // assert
            repositoryMock.Verify(x => x.Update(It.Is((Car carToUpdate) => carToUpdate.Id == carId)));
        }
    }
}
