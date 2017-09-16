using System;
using FluentAssertions;
using Model;
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

            var browser = CreateDefaultBrowser();

            // act
            var response = browser.Get($"/cars/{carId}");

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var car = response.Body.DeserializeJson<Car>();
            car.Should().NotBeNull();
            car.Id.Should().Be(carId);
        }
    }
}
