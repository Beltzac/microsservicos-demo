using Beltzac.HelloWorld.Domain;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beltzac.HelloWorld.Infrastructure
{
    class GreetQueue : IMessageQueue<Greeting>, IDisposable
    {
        private string SENDER_ID_KEY = "SENDER_ID";

        private readonly IProducer<Guid, string> _producer;
        private readonly IConsumer<Guid, string> _consumer;

        private readonly ISerializer<Guid> _serializerGuid;
        private readonly IDeserializer<Guid> _deserializerGuid;

        private readonly IMicroserviceIdProvider _microserviceIdProvider;

        private readonly GreetQueueOptions _options;

        public GreetQueue(IOptions<GreetQueueOptions> options, ISerializer<Guid> serializerGuid, IDeserializer<Guid> deserializerGuid, IMicroserviceIdProvider microserviceIdProvider)
        {
            _serializerGuid = serializerGuid;
            _deserializerGuid = deserializerGuid;
            _microserviceIdProvider = microserviceIdProvider;

            _options = options.Value;

            var producerConfig = new ProducerConfig
            {
                BootstrapServers = _options.Servers
            };

            //todo:Não parece muito testavel
            _producer = new ProducerBuilder<Guid, string>(producerConfig)
                .SetKeySerializer(_serializerGuid)                
                .Build();

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _options.Servers,
                GroupId = _options.Group,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            //todo:Não parece muito testavel
            _consumer = new ConsumerBuilder<Guid, string>(consumerConfig)
                .SetKeyDeserializer(_deserializerGuid)
                .Build();

            _consumer.Subscribe(_options.Topic);
        }

        public async Task WriteAsync(Greeting message)
        {
            var kafkaMessage = new Message<Guid, string> { Key = message.Id, Value = message.PoliteMessage };

            var context = new SerializationContext(MessageComponentType.Key, _options.Topic);
            var serializedKey = _serializerGuid.Serialize(_microserviceIdProvider.Id, context);

            kafkaMessage.Headers = new Headers
            {
                { SENDER_ID_KEY, serializedKey }
            };

            await _producer.ProduceAsync(_options.Topic, kafkaMessage);
        }

        public async Task<Greeting> ReadAsync()
        {
            var consumeResult = await Task.Run(() => _consumer.Consume(_options.MillisecondsReadTimeout)); //Não existe o ConsumeAsync ainda nessa versão

            if (consumeResult == null)
                return null;

            var kafkaMessage = consumeResult.Message;

            var senderKeyHeader = kafkaMessage.Headers?.FirstOrDefault(h => h.Key == SENDER_ID_KEY);
            var senderKeyBytes = senderKeyHeader?.GetValueBytes();
            var context = new SerializationContext(MessageComponentType.Key, _options.Topic);
            var senderKey = _deserializerGuid.Deserialize(senderKeyBytes, senderKeyBytes == null, context);

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
