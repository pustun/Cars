using System;
using System.Security.Cryptography.X509Certificates;
using Datalayer;
using FluentAssertions;
using Model;
using Moq;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;

namespace WebApi.Tests.Modules
{
    [TestFixture]
    public class GetCarByIdTests : CarModuleTestsBase
    {
        [Test]
        public void Should_return_car_by_id()
        {
            // arrange
            var carId = Guid.NewGuid();

            var browser = Browser().Build();

            // act
            var response = browser.Get($"/cars/{carId}");

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var car = response.Body.DeserializeJson<Car>();
            car.Should().NotBeNull();
            car.Id.Should().Be(carId);
        }

        [Test]
        public void Should_call_repository()
        {
            // arrange
            var carId = Guid.NewGuid();
            var repositoryMock = new Mock<ICarsRepository>();
            repositoryMock.Setup(x => x.GetById(carId))
                .Returns(new Car {Id = carId});

            var browser = Browser().WithRepository(repositoryMock.Object)
                                    .Build();

            // act
            browser.Get($"/cars/{carId}");
            
            // assert
            repositoryMock.Verify(x => x.GetById(carId));
        }

        [Test]
        public void Should_return_NotFound_if_car_does_not_exsist()
        {
            // arrange
            var carId = Guid.NewGuid();
            var repositoryMock = new Mock<ICarsRepository>();
            repositoryMock.Setup(x => x.GetById(carId))
                .Returns((Car)null);

            var browser = Browser().WithRepository(repositoryMock.Object)
                                    .Build();

            // act
            var response = browser.Get($"/cars/{carId}");

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
