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
            base.ApplicationStartup(container, pipelines);

            container.Register<ICarValidator, CarValidator>().AsSingleton();

            container.Register<ICarValidationRule, IdValidationRule>().AsSingleton();
            container.Register<ICarValidationRule, TitleValidationRule>().AsSingleton();
            container.Register<ICarValidationRule, PriceValidationRule>().AsSingleton();
            container.Register<ICarValidationRule, FuelValidationRule>().AsSingleton();
            container.Register<ICarValidationRule, MileageValidationRule>().AsSingleton();
            container.Register<ICarValidationRule, FirstRegistrationValidationRule>().AsSingleton();

            container.Register<ICalendar, Calendar>().AsSingleton();

            container.Register<ICarsRepository, CarsRepository>().AsSingleton();

            container.Register<ICarSortExpressionMapper, CarSortExpressionMapper>().AsSingleton();
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);

            pipelines.AfterRequest.AddItemToEndOfPipeline(ctx =>
            {
                ctx.Response.WithHeader("Access-Control-Allow-Origin", "*")
                    .WithHeader("Access-Control-Allow-Methods", "POST,GET,PUT,DELETE,OPTIONS")
                    .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type");
            });
        }
    }
}