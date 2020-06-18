using Beltzac.HelloWorld.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Beltzac.HelloWorld.Domain
{
    class GreetingManager : IGreetingManager
    {
        private readonly IMicroserviceIdProvider _microServiceIdentification;
        private readonly IMessageQueue<Greeting> _messageQueue;
        private readonly ILogger<GreetingManager> _logger;

        public GreetingManager(IMicroserviceIdProvider microServiceIdentification, IMessageQueue<Greeting> messageQueue, ILogger<GreetingManager> logger)
        {
            _microServiceIdentification = microServiceIdentification;
            _messageQueue = messageQueue;
            _logger = logger;
        }

        /// <summary>
        /// Acknowledge a greeting from someone
        /// </summary>
        public async Task ReceiveAsync(Greeting greeting)
        {
            //Just logging for now
            _logger.LogInformation($"{_microServiceIdentification.Id} <<< {greeting.Id} - {greeting.PoliteMessage} - {greeting.SentAt} - {greeting.SentBy}");           
        }

        /// <summary>
        /// Send a greeting for anyone to receive
        /// </summary>
        public async Task SendAsync(Greeting greeting)
        {            
            await _messageQueue.WriteAsync(greeting);
            _logger.LogInformation($"{_microServiceIdentification.Id} >>> {greeting.Id} - {greeting.PoliteMessage}");
        }
    }
}
