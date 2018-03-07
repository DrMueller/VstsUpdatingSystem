using System;
using Microsoft.Extensions.DependencyInjection;
using Mmu.Vus.WebApi.Infrastructure.Initialization.Handlers;
using StructureMap;

namespace Mmu.Vus.WebApi.Infrastructure.Initialization
{
    internal static class ServiceInitializer
    {
        internal static IServiceProvider InitializeServices(IServiceCollection services)
        {
            var result = CreateServiceProvider(services);
            return result;
        }

        private static IServiceProvider CreateServiceProvider(IServiceCollection services)
        {
            var container = ContainerInitializer.CreateInitializedContainer();
            container.Populate(services);
            var result = container.GetInstance<IServiceProvider>();
            return result;
        }
    }
}