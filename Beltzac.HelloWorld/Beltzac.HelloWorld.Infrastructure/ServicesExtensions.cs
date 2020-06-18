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
            services.Configure<GreetQueue.GreetQueueOptions>(options => configuration.GetSection("GreetQueue").Bind(options));
            services.AddTransient<IMessageQueue<Greet>, GreetQueue>();
            services.AddTransient<IMicroserviceIdProvider, MicroserviceIdProvider>();
            services.AddTransient<ISerializer<Guid>, GuidSerializer>();
            services.AddTransient<IDeserializer<Guid>, GuidSerializer>();
        }
    }
}
