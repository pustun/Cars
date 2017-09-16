using System;
using FluentAssertions;
using Model;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;

namespace WebApi.Tests.Modules
{
    [TestFixture]
    public class UpdateCarTests
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
        public void Should_accept_car_for_update()
        {
            // arrange
            var carId = Guid.NewGuid();
            var carToUpdate = new Car { Id = carId };

            // act
            var response = _browser.Put($"/cars/{carId}", with => with.JsonBody(carToUpdate));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
