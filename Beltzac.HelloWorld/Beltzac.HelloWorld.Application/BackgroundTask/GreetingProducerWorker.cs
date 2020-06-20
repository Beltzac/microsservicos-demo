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
        private readonly IGreetingManager _worldGreeter;
        private readonly ILogger<GreetingProducerWorker> _logger;
        private Timer _timer;

        public GreetingProducerWorker(IGreetingManager helloWorldBusiness, ILogger<GreetingProducerWorker> logger)
        {
            _worldGreeter = helloWorldBusiness;
            _logger = logger;
        }

        private void DoWork(object state)
        {
            try
            {
                var greet = Greeting.Factory.CreateDefault();
                _worldGreeter.SendAsync(greet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending greetings");          
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting to produce messages");
            _timer = new Timer(DoWork, null, TimeSpan.Zero,  TimeSpan.FromSeconds(5));
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
