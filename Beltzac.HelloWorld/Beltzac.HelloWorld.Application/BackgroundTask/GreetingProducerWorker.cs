using Beltzac.HelloWorld.Domain;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Beltzac.HelloWorld.Application.BackgroundTask
{
    public class GreetingProducerWorker : IHostedService, IDisposable
    {
        private readonly IGreetingManager _greetingManager;
        private readonly ILogger<GreetingProducerWorker> _logger;
        private Timer _timer;

        public GreetingProducerWorker(IGreetingManager greetingManager, ILogger<GreetingProducerWorker> logger)
        {
            _greetingManager = greetingManager;
            _logger = logger;
        }

        private void DoWork(object state)
        {
            Greeting greeting = null;
            try
            {
                greeting = Greeting.Factory.CreateDefault();
                _greetingManager.SendAsync(greeting);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending greetings -> {greeting}", greeting);
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting to produce messages");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
