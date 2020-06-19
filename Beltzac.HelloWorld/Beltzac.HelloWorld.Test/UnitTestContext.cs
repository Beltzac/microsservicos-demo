using Beltzac.HelloWorld.Domain;
using Beltzac.HelloWorld.Infrastructure;
using Castle.Core.Logging;
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
        public Mock<IOptions<GreetingQueue.GreetQueueOptions>> QueueOptions { get; set; }
        public Mock<ISerializer<Guid>> GuidSerializer { get; set; }
        public Mock<IDeserializer<Guid>> GuidDeserializer { get; set; }
        public Mock<IMicroserviceIdProvider> MicroserviceIdProvider { get; set; }
        public Mock<IMessageQueue<Greeting>> GreetingQueue { get; set; }

        public Mock<ILogger<GreetingManager>> GreetingManagerLogger { get; set; }

        public UnitTestContext() 
        {
            QueueOptions = new Mock<IOptions<GreetingQueue.GreetQueueOptions>>();
            GuidSerializer = new Mock<ISerializer<Guid>>();
            GuidDeserializer = new Mock<IDeserializer<Guid>>();
            MicroserviceIdProvider = new Mock<IMicroserviceIdProvider>();
            GreetingQueue = new Mock<IMessageQueue<Greeting>>();

            GreetingManagerLogger = new Mock<ILogger<GreetingManager>>();
        }
    }
}
