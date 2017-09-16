using System;
using FluentAssertions;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;

namespace WebApi.Tests.Modules
{
    [TestFixture]
    public class DeleteCarTests : CarModuleTestsBase
    {
        [Test]
        public void Should_remove_car()
        {
            // arrange
            var carId = Guid.NewGuid();

            var browser = CreateDefaultBrowser();

            // act
            var response = browser.Delete($"/cars/{carId}");

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
