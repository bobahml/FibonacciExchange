using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using FibonacciExchangeApi.Filters;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(FibonacciExchangeApi.Startup))]

namespace FibonacciExchangeApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Services.Replace(typeof(IHttpControllerActivator), new ServiceActivator());

            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            config.Services.Replace(typeof(IExceptionLogger), new GlobalExceptionLogger());

            config.MapHttpAttributeRoutes();
 
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseWebApi(config); 
        }
    }
}
