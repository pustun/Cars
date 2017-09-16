using Datalayer;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using WebApi.Services;
using WebApi.Validators;
using WebApi.Validators.CarValidationRules;

namespace WebApi
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            container.Register<ICarValidator, CarValidator>().AsSingleton();

            container.Register<ICarValidationRule, IdValidationRule>().AsSingleton();
            container.Register<ICarValidationRule, TitleValidationRule>().AsSingleton();
            container.Register<ICarValidationRule, PriceValidationRule>().AsSingleton();
            container.Register<ICarValidationRule, FuelValidationRule>().AsSingleton();
            container.Register<ICarValidationRule, MileageValidationRule>().AsSingleton();
            container.Register<ICarValidationRule, FirstRegistrationValidationRule>().AsSingleton();

            container.Register<ICalendar, Calendar>().AsSingleton();

            container.Register<ICarsRepository, CarsRepository>().AsSingleton();

            base.ApplicationStartup(container, pipelines);
        }
    }
}