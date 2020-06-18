using Beltzac.HelloWorld.Domain;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Beltzac.HelloWorld.Application.BackgroundTask
{
    public class GreetProducer : IHostedService, IDisposable
    {
        private readonly IWorldGreeter _helloWorldBusiness;
        private Timer _timer;

        public GreetProducer(IWorldGreeter helloWorldBusiness)
        {
            _helloWorldBusiness = helloWorldBusiness;
        }

        private void DoWork(object state)
        {
            _helloWorldBusiness.SendAsync();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
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
