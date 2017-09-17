using System.Runtime.InteropServices;
using Nancy;

namespace WebApi.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => Response.AsFile("Static/index.html");

            Get["/readme.md"] = _ => Response.AsFile("bin/readme.md");
        }
    }
}