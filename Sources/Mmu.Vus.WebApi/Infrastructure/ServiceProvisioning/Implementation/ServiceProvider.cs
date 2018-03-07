using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using StructureMap;

namespace Mmu.Vus.WebApi.Infrastructure.ServiceProvisioning.Implementation
{
    public class ServiceProvider : IServiceProvider, IDependencyResolver
    {
        private readonly IContainer _container;

        public ServiceProvider(IContainer container)
        {
            _container = container;
        }

        // Wouldn't work due to the Singleton I guess, but whatever
        public IDependencyScope BeginScope()
        {
            var childContainer = _container.CreateChildContainer();
            var result = childContainer.GetInstance<ServiceProvider>();
            return result;
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public object GetService(Type serviceType)
        {
            try
            {
                var result = _container.GetInstance(serviceType);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.GetAllInstances(serviceType).Cast<object>().ToList();
            }
            catch
            {
                return new List<Object>();
            }
        }
    }
}