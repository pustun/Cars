using System;
using FluentAssertions;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;

namespace WebApi.Tests.Modules
{
    [TestFixture]
    public class DeleteCarTests
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
        public void Should_remove_car()
        {
            // arrange
            var carId = Guid.NewGuid();

            // act
            var response = _browser.Delete($"/cars/{carId}");

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
