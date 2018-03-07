namespace Mmu.Vus.WebApi.Infrastructure.ServiceProvisioning
{
    public static class ServiceProviderSingleton
    {
        public static IServiceProvider Instance { get; private set; }

        public static void Initialize(IServiceProvider instance)
        {
            Instance = instance;
        }
    }
}