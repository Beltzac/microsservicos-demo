using Beltzac.HelloWorld.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Beltzac.HelloWorld.Domain
{
    public class GreetingManager : IGreetingManager
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
        /// Send a greeting for anyone to receive
        /// </summary>
        public async Task SendAsync(Greeting greeting)
        {            
            await _messageQueue.SendAsync(greeting);
            DisplayGreetingsTraffic($"{_microServiceIdentification.Id} >>> {greeting}");
        }

        /// <summary>
        /// Acknowledge a greeting from someone
        /// </summary>
        public async Task ReceiveAsync(Greeting greeting)
        {
            //Just logging for now
            DisplayGreetingsTraffic($"{_microServiceIdentification.Id} <<< {greeting}");
        }

        public virtual void DisplayGreetingsTraffic(string message)
        {
            _logger.LogInformation(message);
        }
    }
}
