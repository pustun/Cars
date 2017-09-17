using System;
using System.Linq.Expressions;
using Datalayer;
using Model;
using Nancy;
using Nancy.ModelBinding;
using WebApi.Common;
using WebApi.Services;
using WebApi.Validators;

namespace WebApi.Modules
{
    public class CarsModule : NancyModule
    {
        private readonly ICarValidator _carValidator;
        private readonly ICarsRepository _carsRepository;
        private readonly ICarSortExpressionMapper _sortExpressionMapper;

        public CarsModule(ICarValidator carValidator,
                          ICarsRepository carsRepository,
                          ICarSortExpressionMapper sortExpressionMapper)
            : base("cars")
        {
            _carValidator = carValidator;
            _carsRepository = carsRepository;
            _sortExpressionMapper = sortExpressionMapper;

            Get["/"] = _ => this.GetAll();

            Get["/{id}"] = args => this.GetById(args.id);

            Post["/"] = _ => this.AddCar();

            Put["/{id}"] = args => this.UpdateCar(args.id);

            Delete["/{id}"] = args => this.DeleteCar(args.id);
        }

        private Response DeleteCar(Guid id)
        {
            _carsRepository.Delete(id);

            return new Response {StatusCode = HttpStatusCode.OK};
        }

        private Response UpdateCar(Guid id)
        {
            var car = this.Bind<Car>();

            if (!_carValidator.IsValid(car))
            {
                return new Response {StatusCode = HttpStatusCode.BadRequest};
            }

            _carsRepository.Update(car);

            return new Response { StatusCode = HttpStatusCode.OK};
        }

        private Response AddCar()
        {
            var car = this.Bind<Car>();

            if (!_carValidator.IsValid(car))
            {
                return new Response {StatusCode = HttpStatusCode.BadRequest};
            }

            _carsRepository.Add(car);

            return new Response
            {
                StatusCode = HttpStatusCode.Created,
                Headers = { {"Location", $"/cars/{car.Id}"} }
            };
        }

        private Response GetById(Guid id)
        {
            var car = _carsRepository.GetById(id);

            if (car == null)
            {
                return new Response {StatusCode = HttpStatusCode.NotFound};
            }

            return Response.AsJson(car);
        }

        private Response GetAll()
        {
            Expression<Func<Car, object>> defaultSortExpression = car => car.Id;

            var sortDefintion = this.Bind<SortDefinition>();

            var sortExpression = string.IsNullOrWhiteSpace(sortDefintion.Sort)
                ? defaultSortExpression
                : _sortExpressionMapper.Map(sortDefintion.Sort);

            var cars = _carsRepository.GetAll(sortExpression);

            return Response.AsJson(cars);
        }
    }
}