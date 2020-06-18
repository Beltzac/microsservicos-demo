using Beltzac.HelloWorld.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Beltzac.HelloWorld.Infrastructure
{
    public static class ServicesExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IClock, Clock>();
            services.AddTransient<IMessageQueue, MessageQueue>();
        }
    }
}
