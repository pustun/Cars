using System.Collections.Generic;
using FluentAssertions;
using Model;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;

namespace WebApi.Tests.Modules
{
    [TestFixture]
    public class GetAllCarsTests
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
        public void Should_return_all_cars()
        {
            // act
            var response = _browser.Get("/cars");

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var cars = response.Body.DeserializeJson<IEnumerable<Car>>();
            cars.Should().NotBeNull();
            cars.Should().NotBeEmpty();
        }
    }
}
