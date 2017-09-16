using System;
using FluentAssertions;
using Model;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;

namespace WebApi.Tests.Modules
{
    [TestFixture]
    public class AddNewCarTests
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
        public void Should_accept_new_car()
        {
            // arrange
            var carId = Guid.NewGuid();
            var carToAdd = new Car { Id = carId };

            // act
            var response = _browser.Post("/cars", with => with.JsonBody(carToAdd));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers["Location"].Should().Be($"/cars/{carId}");
        }
    }
}
