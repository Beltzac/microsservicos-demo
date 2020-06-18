using Beltzac.HelloWorld.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Beltzac.HelloWorld.Domain
{
    class HelloWorldBusiness : IHelloWorldBusiness
    {
        private const string HELLO_TEXT = "Hello World";

        private readonly IMicroServiceIdentification _microServiceIdentification;
        private readonly IClock _clock;
        private readonly IMessageQueue _messageQueue;
        private readonly ILogger<HelloWorldBusiness> _logger;

        public HelloWorldBusiness(IMicroServiceIdentification microServiceIdentification, IClock clock, IMessageQueue messageQueue, ILogger<HelloWorldBusiness> logger)
        {
            _microServiceIdentification = microServiceIdentification;
            _clock = clock;
            _messageQueue = messageQueue;
            _logger = logger;
        }

        public async Task ReceiveAsync(Message message)
        {
            _logger.LogInformation($"{message.SenderId}: sent {message.SentAt}: received {_clock.GetNow()} >>> {message.Id} - {message.Payload}");
        }

        public async Task SendAsync()
        {
            var message = new Message(_microServiceIdentification.Id, _clock.GetNow(), HELLO_TEXT);
            await _messageQueue.WriteAsync(message);
            _logger.LogDebug("Message sent.", message);
        }
    }
}
