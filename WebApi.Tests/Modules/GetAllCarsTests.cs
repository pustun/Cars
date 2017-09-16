using System.Collections.Generic;
using FluentAssertions;
using Model;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;

namespace WebApi.Tests.Modules
{
    [TestFixture]
    public class GetAllCarsTests : CarModuleTestsBase
    {
        [Test]
        public void Should_return_all_cars()
        {
            // arrange
            var browser = CreateDefaultBrowser();

            // act
            var response = browser.Get("/cars");

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var cars = response.Body.DeserializeJson<IEnumerable<Car>>();
            cars.Should().NotBeNull();
            cars.Should().NotBeEmpty();
        }
    }
}
