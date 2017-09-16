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
            var validatorMock = new Mock<ICarValidator>();
            validatorMock.Setup(x => x.IsValid(It.IsAny<Car>()))
                .Returns(true);

            return CreateBrowserWithValidator(validatorMock.Object);
        }

        protected Browser CreateBrowserWithValidator(ICarValidator carValidator)
        {
            return new Browser(with => with.Module(new CarsModule(carValidator)),
                               defaults: to => to.Accept("application/json"));
        }
    }
}
