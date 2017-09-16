﻿using System;
using FluentAssertions;
using Model;
using Moq;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using WebApi.Validators;

namespace WebApi.Tests.Modules
{
    [TestFixture]
    public class AddNewCarTests : CarModuleTestsBase
    {
        [Test]
        public void Should_accept_new_car()
        {
            // arrange
            var carId = Guid.NewGuid();
            var carToAdd = new Car { Id = carId };

            var browser = CreateDefaultBrowser();

            // act
            var response = browser.Post("/cars", with => with.JsonBody(carToAdd));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers["Location"].Should().Be($"/cars/{carId}");
        }

        [Test]
        public void Should_validate_car()
        {
            // arrange
            var validatorMock = new Mock<ICarValidator>();
            validatorMock.Setup(x => x.IsValid(It.IsAny<Car>()))
                .Returns(true);

            var car = new Car();

            var browser = CreateBrowserWithValidator(validatorMock.Object);

            // act
            browser.Post("/cars", with => with.JsonBody(car));

            // assert
            validatorMock.Verify(x => x.IsValid(It.IsAny<Car>()));
        }

        [Test]
        public void Should_return_BadRequest_if_car_not_valid()
        {
            // arrange
            var validatorMock = new Mock<ICarValidator>();
            validatorMock.Setup(x => x.IsValid(It.IsAny<Car>()))
                .Returns(false);

            var car = new Car();

            var browser = CreateBrowserWithValidator(validatorMock.Object);

            // act
            var response = browser.Post("/cars", with => with.JsonBody(car));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}