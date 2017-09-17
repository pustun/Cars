using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Datalayer;
using FluentAssertions;
using Model;
using Moq;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using WebApi.Services;

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

        [Test]
        public void Should_call_repository()
        {
            // arrange
            var carId1 = Guid.NewGuid();
            var carId2 = Guid.NewGuid();

            var repositoryMock = new Mock<ICarsRepository>();
            repositoryMock.Setup(x => x.GetAll(It.IsAny<Expression<Func<Car, object>>>()))
                .Returns(new[] {new Car {Id = carId1}, new Car {Id = carId2}});

            var browser = CreateBrowser(repositoryMock.Object);

            // act
            var response = browser.Get("/cars");

            // assert
            repositoryMock.Verify(x => x.GetAll(It.IsAny<Expression<Func<Car, object>>>()));

            var idsFromResponse = response.Body.DeserializeJson<IEnumerable<Car>>()
                .Select(x => x.Id)
                .ToArray();

            idsFromResponse.ShouldBeEquivalentTo(new [] {carId1, carId2});
        }

        [Test]
        public void Should_call_CarSortExpressionMapper_if_sort_provided()
        {
            // arrange
            Expression<Func<Car, object>> sortExpression = car => car.Mileage;

            var sortMapperMock = new Mock<ICarSortExpressionMapper>();
            sortMapperMock.Setup(x => x.Map(It.IsAny<string>()))
                .Returns(sortExpression);

            var repositoryMock = new Mock<ICarsRepository>();
            repositoryMock.Setup(x => x.GetAll(It.IsAny<Expression<Func<Car, object>>>()));

            var browser = CreateBrowser(repositoryMock.Object, sortMapperMock.Object);

            // act
            browser.Get("/cars", with => with.Query("sort", "mileage"));

            // assert
            sortMapperMock.Verify(x => x.Map(It.Is((string sort) => sort == "mileage")));
            repositoryMock.Verify(x => x.GetAll(sortExpression));
        }
    }
}
