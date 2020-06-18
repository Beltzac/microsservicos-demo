using Beltzac.HelloWorld.Domain;
using Beltzac.HelloWorld.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Beltzac.HelloWorld.IoC
{
    public static class ServicesExtensions
    {
        public static void AddApplicationIoCServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.AddDomainServices(configuration);
            services.AddInfrastructureServices(configuration);          
        }
    }
}
