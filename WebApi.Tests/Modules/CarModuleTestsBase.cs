using WebApi.Tests.BrowserBuilders;

namespace WebApi.Tests.Modules
{
    public class CarModuleTestsBase
    {
        protected CarModuleBrowserBuilder Browser()
        {
            return new CarModuleBrowserBuilder();
        }
    }
}
