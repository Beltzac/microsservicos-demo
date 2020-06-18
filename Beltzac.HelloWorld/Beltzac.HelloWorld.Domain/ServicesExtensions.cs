using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Beltzac.HelloWorld.Domain
{
    public static class ServicesExtensions
    {
        public static void AddDomainServices(this IServiceCollection services, IConfiguration configuration)
        { 
            services.AddSingleton<IGreetingManager, GreetingManager>();
        }
    }
}
