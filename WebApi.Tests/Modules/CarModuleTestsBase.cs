using System;
using System.Linq.Expressions;
using Datalayer;
using Model;
using Moq;
using Nancy.Testing;
using WebApi.Modules;
using WebApi.Services;
using WebApi.Validators;

namespace WebApi.Tests.Modules
{
    public class CarModuleTestsBase
    {
        protected Browser CreateDefaultBrowser()
        {
            var validator = MockValidator();
            var repository = MockRepository();
            var mapper = MockMapper();
            return CreateBrowser(validator,
                                 repository,
                                 mapper);
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

        protected Browser CreateBrowser(ICarValidator carValidator)
        {
            return CreateBrowser(carValidator,
                                 MockRepository(),
                                 MockMapper());
        }

        protected Browser CreateBrowser(ICarsRepository repository)
        {
            return CreateBrowser(MockValidator(),
                                 repository,
                                 MockMapper());
        }

        protected Browser CreateBrowser(ICarsRepository carsRepository,
                                        ICarSortExpressionMapper sortExpressionMapper)
        {
            return CreateBrowser(MockValidator(),
                                 carsRepository,
                                 sortExpressionMapper);
        }

        protected Browser CreateBrowser(ICarValidator carValidator,
                                        ICarsRepository carsRepository,
                                        ICarSortExpressionMapper sortExpressionMapper)
        {
            return new Browser(with => with.Module(
                                    new CarsModule(carValidator,
                                                   carsRepository,
                                                   sortExpressionMapper)),
                               defaults: to => to.Accept("application/json"));
        }
    }
}
