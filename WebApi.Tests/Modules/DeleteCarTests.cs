using System;
using Datalayer;
using FluentAssertions;
using Moq;
using Nancy;
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

            var browser = Browser().Build();

            // act
            var response = browser.Delete($"/cars/{carId}");

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void Should_call_repository()
        {
            // arrange
            var carId = Guid.NewGuid();

            var repositoryMock = new Mock<ICarsRepository>();
            repositoryMock.Setup(x => x.Delete(It.IsAny<Guid>()));

            var browser = Browser().WithRepository(repositoryMock.Object)
                                    .Build();

            // act
            browser.Delete($"/cars/{carId}");

            // assert
            repositoryMock.Verify(x => x.Delete(carId));
        }
    }
}
