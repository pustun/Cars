using Nancy;

namespace WebApi.Modules
{
  public class CarModule : NancyModule
  {
    public CarModule()
    {
      Get["/"] = _ => "Hello World";
    }
  }
}