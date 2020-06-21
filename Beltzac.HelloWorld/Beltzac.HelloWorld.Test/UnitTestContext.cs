using Beltzac.HelloWorld.Domain;
using Beltzac.HelloWorld.Infrastructure;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beltzac.HelloWorld.Test
{
    public class UnitTestContext
    {
        public Mock<IOptions<GreetingQueue.GreetQueueOptions>> QueueOptions { get; }
        public Mock<ISerializer<Guid>> GuidSerializer { get; }
        public Mock<IDeserializer<Guid>> GuidDeserializer { get; }
        public Mock<IMicroserviceIdProvider> MicroserviceIdProvider { get; }
        public Mock<IMessageQueue<Greeting>> GreetingQueue { get; }
        public Mock<IGreetingManager> GreetingManager { get; }

        public UnitTestContext() 
        {
            QueueOptions = new Mock<IOptions<GreetingQueue.GreetQueueOptions>>();
            GuidSerializer = new Mock<ISerializer<Guid>>();
            GuidDeserializer = new Mock<IDeserializer<Guid>>();
            MicroserviceIdProvider = new Mock<IMicroserviceIdProvider>();
            GreetingQueue = new Mock<IMessageQueue<Greeting>>();
            GreetingManager = new Mock<IGreetingManager>();
        }

        public Mock<ILogger<T>> GetLoggerMock<T>()
        {
            return new Mock<ILogger<T>>();
        }
    }
}
