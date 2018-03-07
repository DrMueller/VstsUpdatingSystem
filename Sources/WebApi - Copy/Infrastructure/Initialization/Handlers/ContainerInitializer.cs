using StructureMap;

namespace Mmu.Vus.WebApi.Infrastructure.Initialization.Handlers
{
    internal static class ContainerInitializer
    {
        public static Container CreateInitializedContainer()
        {
            var result = new Container();

            result.Configure(
                config =>
                {
                    config.Scan(
                        scan =>
                        {
                            // Fun fact: TheCallingAssembly doesn't work in AspNet Core
                            scan.AssemblyContainingType(typeof(ContainerInitializer));
                            scan.WithDefaultConventions();
                        });
                });

            return result;
        }
    }
}