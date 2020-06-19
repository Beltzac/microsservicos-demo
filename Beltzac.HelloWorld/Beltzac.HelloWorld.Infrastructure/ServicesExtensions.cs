using Beltzac.HelloWorld.Domain;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Beltzac.HelloWorld.Infrastructure
{
    public static class ServicesExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GreetingQueue.GreetQueueOptions>(options => configuration.GetSection("GreetQueue").Bind(options));
            services.AddSingleton<IMessageQueue<Greeting>, GreetingQueue>();
            services.AddSingleton<IMicroserviceIdProvider, MicroserviceIdProvider>();
            services.AddSingleton<ISerializer<Guid>, GuidSerializer>();
            services.AddSingleton<IDeserializer<Guid>, GuidSerializer>();
        }
    }
}
