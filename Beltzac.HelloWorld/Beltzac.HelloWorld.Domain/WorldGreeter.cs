using Beltzac.HelloWorld.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Beltzac.HelloWorld.Domain
{
    class WorldGreeter : IWorldGreeter
    {
        private const string GREETINGS_TO_WORLD = "Hello World";

        private readonly IMicroserviceIdProvider _microServiceIdentification;
        private readonly IMessageQueue<Greet> _messageQueue;
        private readonly ILogger<WorldGreeter> _logger;

        public WorldGreeter(IMicroserviceIdProvider microServiceIdentification, IMessageQueue<Greet> messageQueue, ILogger<WorldGreeter> logger)
        {
            _microServiceIdentification = microServiceIdentification;
            _messageQueue = messageQueue;
            _logger = logger;
        }

        public async Task ProcessNewGreetAsync(Greet greet)
        {
            //Just log for now
            _logger.LogInformation($"{greet.SentBy}:{greet.SentAt} >>> {greet.Id} - {greet.PoliteMessage}");
        }

        public async Task SendAsync()
        {
            var message = new Greet(GREETINGS_TO_WORLD);
            await _messageQueue.WriteAsync(message);
            _logger.LogDebug($"Message {message.Id} sent from {_microServiceIdentification.Id}");
        }
    }
}
