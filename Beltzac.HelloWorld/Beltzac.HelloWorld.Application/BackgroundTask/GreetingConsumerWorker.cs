using Beltzac.HelloWorld.Domain;
using Beltzac.HelloWorld.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Beltzac.HelloWorld.Application.BackgroundTask
{
    public class GreetingConsumerWorker : BackgroundService
    {
        private readonly IGreetingManager _worldGreeter;
        private readonly IMessageQueue<Greeting> _messageQueue;
        private readonly ILogger<GreetingConsumerWorker> _logger;

        public GreetingConsumerWorker(IGreetingManager worldGreeter, IMessageQueue<Greeting> messageQueue, ILogger<GreetingConsumerWorker> logger)
        {
            _worldGreeter = worldGreeter;
            _messageQueue = messageQueue;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(() => StartConsumer(stoppingToken));
            return Task.CompletedTask;
        }

        private async Task StartConsumer(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting to consume messages");
            while (!stoppingToken.IsCancellationRequested)
            {
                Greeting greet = null;
                try
                {
                    greet = await _messageQueue.ReceiveAsync();

                    if (greet != null)
                        await _worldGreeter.ReceiveAsync(greet);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing greet message queue", greet);
                }       
            }
        }
    }
}
