using System;
using System.Collections.Generic;
using FluentAssertions;
using Model;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;

namespace WebApi.Tests
{
    [TestFixture]
    public class CarModuleTests
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

        [Test]
        public void Should_accept_new_car()
        {
            // arrange
            var carId = Guid.NewGuid();
            var carToAdd = new Car {Id = carId};

            // act
            var response = _browser.Post("/cars", with => with.JsonBody(carToAdd));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers["Location"].Should().Be($"/cars/{carId}");
        }

        [Test]
        public void Should_accept_car_for_update()
        {
            // arrange
            var carId = Guid.NewGuid();
            var carToUpdate = new Car {Id = carId};

            // act
            var response = _browser.Put($"/cars/{carId}", with => with.JsonBody(carToUpdate));

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
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