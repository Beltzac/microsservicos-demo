using Beltzac.HelloWorld.Domain;
using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beltzac.HelloWorld.Infrastructure
{
    class MessageQueue : IMessageQueue
    {
        private readonly IMicroServiceIdentification _microServiceIdentification;
        public void Write(string text)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "host1:9092,host2:9092",
                ClientId = Dns.GetHostName()             
            };


            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
           
            }
        }

        public Message Read()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "host1:9092,host2:9092",
                GroupId = "foo",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, Message>(config).Build();
            consumer.Subscribe("kafkaListenTopic_From_Producer");
            var consumeResult = consumer.Consume().Message?.Value;
        }
    }
}
