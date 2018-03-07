using System.Web.Http.Dependencies;
using Mmu.Vus.WebApi.Infrastructure.Initialization.Handlers;
using Mmu.Vus.WebApi.Infrastructure.ServiceProvisioning;
using Mmu.Vus.WebApi.Infrastructure.ServiceProvisioning.Implementation;

namespace Mmu.Vus.WebApi.Infrastructure.Initialization
{
    public static class ServiceInitialization
    {
        public static IDependencyResolver InitializeServices()
        {
            var container = ContainerInitializer.CreateInitializedContainer();
            var serviceProvider = container.GetInstance<ServiceProvider>();
            ServiceProviderSingleton.Initialize(serviceProvider);

            return serviceProvider;
        }
    }
}