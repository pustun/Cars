using System;
using System.Linq.Expressions;
using Datalayer;
using Model;
using Moq;
using Nancy.Testing;
using WebApi.Modules;
using WebApi.Services;
using WebApi.Validators;

namespace WebApi.Tests.BrowserBuilders
{
    public class CarModuleBrowserBuilder
    {
        private ICarValidator _carValidator;
        private ICarsRepository _carsRepository;
        private ICarSortExpressionMapper _carSortExpressionMapper;

        public Browser Build()
        {
            return new Browser(with => with.Module(
                                    new CarsModule(_carValidator ?? MockValidator(),
                                                   _carsRepository ?? MockRepository(),
                                                   _carSortExpressionMapper ?? MockMapper())),
                               defaults: to => to.Accept("application/json"));
        }

        public CarModuleBrowserBuilder WithValidator(ICarValidator validator)
        {
            _carValidator = validator;

            return this;
        }

        public CarModuleBrowserBuilder WithRepository(ICarsRepository carsRepository)
        {
            _carsRepository = carsRepository;

            return this;
        }

        public CarModuleBrowserBuilder WithSortExpressionsMapper(ICarSortExpressionMapper carSortExpressionMapper)
        {
            _carSortExpressionMapper = carSortExpressionMapper;

            return this;
        }

        private ICarValidator MockValidator()
        {
            var validatorMock = new Mock<ICarValidator>();
            validatorMock.Setup(x => x.IsValid(It.IsAny<Car>()))
                .Returns(true);

            return validatorMock.Object;
        }

        private ICarsRepository MockRepository()
        {
            var repositoryMock = new Mock<ICarsRepository>();
            repositoryMock.Setup(x => x.Add(It.IsAny<Car>()));
            repositoryMock.Setup(x => x.Update(It.IsAny<Car>()));
            repositoryMock.Setup(x => x.Delete(It.IsAny<Guid>()));
            repositoryMock.Setup(x => x.GetAll(It.IsAny<Expression<Func<Car, object>>>()))
                .Returns(new[] { new Car { Id = Guid.NewGuid() }, new Car { Id = Guid.NewGuid() } });
            repositoryMock.Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns((Guid id) => new Car { Id = id });

            return repositoryMock.Object;
        }

        private ICarSortExpressionMapper MockMapper()
        {
            Expression<Func<Car, object>> sortExpression = car => car.Id;

            var sortMapperMock = new Mock<ICarSortExpressionMapper>();
            sortMapperMock.Setup(x => x.Map(It.IsAny<string>()))
                .Returns(sortExpression);

            return sortMapperMock.Object;
        }
    }
}
