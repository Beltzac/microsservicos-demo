using Beltzac.HelloWorld.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Beltzac.HelloWorld.Domain
{
    public static class ServicesExtensions
    {
        public static void AddDomainServices(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddTransient<IMicroServiceIdentification, MicroServiceIdentification>();
            services.AddTransient<IHelloWorldBusiness, HelloWorldBusiness>();
        }
    }
}
