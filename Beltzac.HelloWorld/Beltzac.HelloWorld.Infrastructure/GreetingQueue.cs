using Beltzac.HelloWorld.Domain;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Beltzac.HelloWorld.Infrastructure
{
    //todo:Não parece muito testavel
    public class GreetingQueue : IMessageQueue<Greeting>, IDisposable
    {
        private const string SENDER_ID_KEY = "SENDER_ID";

        private readonly IProducer<Guid, string> _producer;
        private readonly IConsumer<Guid, string> _consumer;

        private readonly ISerializer<Guid> _guidSerializer;
        private readonly IDeserializer<Guid> _guidDeserializer;

        private readonly IMicroserviceIdProvider _microserviceIdProvider;

        private readonly GreetQueueOptions _options;

        public GreetingQueue(IOptions<GreetQueueOptions> options, ISerializer<Guid> guidSerializer, IDeserializer<Guid> guidDeserializer, IMicroserviceIdProvider microserviceIdProvider)
        {
            _guidSerializer = guidSerializer;
            _guidDeserializer = guidDeserializer;
            _microserviceIdProvider = microserviceIdProvider;

            _options = options.Value;

            var producerConfig = new ProducerConfig
            {
                BootstrapServers = _options.Servers
            };

            _producer = new ProducerBuilder<Guid, string>(producerConfig)
                .SetKeySerializer(_guidSerializer)
                .Build();

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _options.Servers,
                GroupId = _options.Group,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Guid, string>(consumerConfig)
                .SetKeyDeserializer(_guidDeserializer)
                .Build();

            _consumer.Subscribe(_options.Topic);
        }

        public async Task SendAsync(Greeting message)
        {
            var kafkaMessage = new Message<Guid, string> { Key = message.Id, Value = message.PoliteMessage };

            var context = new SerializationContext(MessageComponentType.Key, _options.Topic);
            var serializedKey = _guidSerializer.Serialize(_microserviceIdProvider.Id, context);

            kafkaMessage.Headers = new Headers
            {
                { SENDER_ID_KEY, serializedKey }
            };

            await _producer.ProduceAsync(_options.Topic, kafkaMessage);
        }

        public async Task<Greeting> ReceiveAsync()
        {
            var consumeResult = await Task.Run(() => _consumer.Consume(_options.MillisecondsReadTimeout)); //Não existe o ConsumeAsync ainda nessa versão

            if (consumeResult == null)
                return null;

            var kafkaMessage = consumeResult.Message;

            var senderKeyHeader = kafkaMessage.Headers?.FirstOrDefault(h => h.Key == SENDER_ID_KEY);
            var senderKeyBytes = senderKeyHeader?.GetValueBytes();
            var context = new SerializationContext(MessageComponentType.Key, _options.Topic);
            var senderKey = _guidDeserializer.Deserialize(senderKeyBytes, senderKeyBytes == null, context);

            return Greeting.Factory.CreateFromQueue(kafkaMessage.Key, senderKey, kafkaMessage.Timestamp.UtcDateTime, kafkaMessage.Value);
        }

        public void Dispose()
        {
            _consumer?.Dispose();
            _producer?.Dispose();
        }

        public class GreetQueueOptions
        {
            public string Servers { get; set; }
            public string Topic { get; set; }
            public string Group { get; set; }
            public int MillisecondsReadTimeout { get; set; }
        }
    }
}
