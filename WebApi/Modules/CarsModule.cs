using System;
using System.Collections.Generic;
using Model;
using Nancy;
using Nancy.ModelBinding;
using WebApi.Validators;

namespace WebApi.Modules
{
    public class CarsModule : NancyModule
    {
        private ICarValidator _carValidator;

        public CarsModule(ICarValidator carValidator) : base("cars")
        {
            _carValidator = carValidator;

            Get["/"] = _ => this.GetAll();

            Get["/{id}"] = args => this.GetById(args.id);

            Post["/"] = _ => this.AddCar();

            Put["/{id}"] = args => this.UpdateCar(args.id);

            Delete["/{id}"] = args => this.DeleteCar(args.id);
        }

        private Response DeleteCar(Guid id)
        {
            return new Response {StatusCode = HttpStatusCode.OK};
        }

        private Response UpdateCar(Guid id)
        {
            var car = this.Bind<Car>();

            if (!_carValidator.IsValid(car))
            {
                return new Response {StatusCode = HttpStatusCode.BadRequest};
            }

            return new Response { StatusCode = HttpStatusCode.OK};
        }

        private Response AddCar()
        {
            var car = this.Bind<Car>();

            if (!_carValidator.IsValid(car))
            {
                return new Response {StatusCode = HttpStatusCode.BadRequest};
            }

            return new Response
            {
                StatusCode = HttpStatusCode.Created,
                Headers = { {"Location", $"/cars/{car.Id}"} }
            };
        }

        private Car GetById(Guid id)
        {
            return new Car {Id = id};
        }

        private IEnumerable<Car> GetAll()
        {
            return new[] {new Car()};
        }
    }
}