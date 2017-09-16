using System;
using Datalayer;
using Model;
using Moq;
using Nancy.Testing;
using WebApi.Modules;
using WebApi.Validators;

namespace WebApi.Tests.Modules
{
    public class CarModuleTestsBase
    {
        protected Browser CreateDefaultBrowser()
        {
            var validator = MockValidator();
            var repository = MockRepository();
            return CreateBrowser(validator, repository);
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
            repositoryMock.Setup(x => x.GetAll())
                .Returns(new[] { new Car { Id = Guid.NewGuid() }, new Car { Id = Guid.NewGuid() } });
            repositoryMock.Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns((Guid id) => new Car { Id = id });

            return repositoryMock.Object;
        }

        protected Browser CreateBrowser(ICarValidator carValidator)
        {
            return CreateBrowser(carValidator, MockRepository());
        }

        protected Browser CreateBrowser(ICarsRepository repository)
        {
            return CreateBrowser(MockValidator(), repository);
        }

        protected Browser CreateBrowser(ICarValidator carValidator, ICarsRepository carsRepository)
        {
            return new Browser(with => with.Module(new CarsModule(carValidator, carsRepository)),
                               defaults: to => to.Accept("application/json"));
        }
    }
}
