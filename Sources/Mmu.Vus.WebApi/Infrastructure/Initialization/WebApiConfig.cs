using System.Web.Http;

namespace Mmu.Vus.WebApi.Infrastructure.Initialization
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.DependencyResolver = ServiceInitialization.InitializeServices();
        }
    }
}