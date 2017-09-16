using System;
using FluentAssertions;
using Model;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;

namespace WebApi.Tests.Modules
{
    [TestFixture]
    public class GetCarByIdTests
    {
        private Browser _browser;

        [OneTimeSetUp]
        public void FixtureSetup()
        {
            var bootstrapper = new DefaultNancyBootstrapper();
            _browser = new Browser(bootstrapper,
                                      defaults: to => to.Accept("application/json"));
        }

        [Test]
        public void Should_return_car_by_id()
        {
            // arrange
            var carId = Guid.NewGuid();

            // act
            var response = _browser.Get($"/cars/{carId}");

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var car = response.Body.DeserializeJson<Car>();
            car.Should().NotBeNull();
            car.Id.Should().Be(carId);
        }
    }
}
