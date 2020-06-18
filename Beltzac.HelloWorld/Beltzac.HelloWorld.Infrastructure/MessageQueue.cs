using Beltzac.HelloWorld.Domain;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Beltzac.HelloWorld.Infrastructure
{
    class MessageQueue : IMessageQueue, IDisposable
    {
        private readonly IProducer<Ignore, Message> _producer;
        private readonly IConsumer<Ignore, Message> _consumer;

        private readonly MessageQueueOptions _options;

        public MessageQueue(IOptions<MessageQueueOptions> options)
        {
            _options = options.Value;

            var producerConfig = new ProducerConfig
            {
                BootstrapServers = _options.Servers
            };

            //todo:Não parece muito testavel
            _producer = new ProducerBuilder<Ignore, Message>(producerConfig).Build();

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _options.Servers,
                GroupId = _options.Group,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            //todo:Não parece muito testavel
            _consumer = new ConsumerBuilder<Ignore, Message>(consumerConfig).Build();
            _consumer.Subscribe(_options.Topic);
        }

        public async Task WriteAsync(Message message)
        {
            var kafkaMessage = new Message<Ignore, Message> { Value = message };
            await _producer.ProduceAsync(_options.Topic, kafkaMessage);
        }

        public async Task<Message> ReadAsync()
        {
            var kafkaMessage = await Task.Run(() => _consumer.Consume(_options.MillisecondsReadTimeout)); //Não existe o ConsumeAsync ainda nessa versão
            return kafkaMessage.Message?.Value;
        }

        public void Dispose()
        {
            _consumer?.Dispose();
            _producer?.Dispose();
        }

        public class MessageQueueOptions
        {
            public string Servers { get; }
            public string Topic { get; }
            public string Group { get; }
            public int MillisecondsReadTimeout { get; }
        }
    }
}
