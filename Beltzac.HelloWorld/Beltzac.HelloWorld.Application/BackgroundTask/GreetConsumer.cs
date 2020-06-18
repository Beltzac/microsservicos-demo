using Beltzac.HelloWorld.Domain;
using Beltzac.HelloWorld.Infrastructure;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Beltzac.HelloWorld.Application.BackgroundTask
{
    public class GreetConsumer : BackgroundService
    {
        private readonly IWorldGreeter _worldGreeter;
        private readonly IMessageQueue<Greet> _messageQueue;       

        public GreetConsumer(IWorldGreeter worldGreeter, IMessageQueue<Greet> messageQueue)
        {
            _worldGreeter = worldGreeter;
            _messageQueue = messageQueue;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(() => StartConsumer(stoppingToken));
            return Task.CompletedTask;
        }

        private async Task StartConsumer(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var message = await _messageQueue.ReadAsync();
                if (message != null)
                    await _worldGreeter.ProcessNewGreetAsync(message);                
            }
        }
    }
}
