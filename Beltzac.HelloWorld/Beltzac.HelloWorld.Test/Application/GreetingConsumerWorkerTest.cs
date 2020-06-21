using Beltzac.HelloWorld.Application.BackgroundTask;
using Beltzac.HelloWorld.Domain;
using Beltzac.HelloWorld.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading;
using System.Threading.Tasks;

namespace Beltzac.HelloWorld.Test.Application
{
    [TestClass]
    public class GreetingConsumerWorkerTest
    {
        [TestMethod]
        public async Task ConsumerProcessMessages()
        {
            int expectedCount = 5;
            int count = 0;

            var greeting = Greeting.Factory.CreateDefault();
            var ctx = new UnitTestContext();         
            
            var hostedService = new GreetingConsumerWorker(ctx.GreetingManager.Object, ctx.GreetingQueue.Object, ctx.GetLoggerMock<GreetingConsumerWorker>().Object);

            //Cancel after X calls
            ctx.GreetingQueue
                .Setup(x => x.ReceiveAsync())
                .ReturnsAsync(greeting)
                .Callback(async () =>
                    {
                        count++;
                        if (count == expectedCount)
                            await hostedService.StopAsync(CancellationToken.None);
                    });

            //act
            await hostedService.StartAsync(CancellationToken.None);
            await Task.Delay(1000); //Time to execute

            ctx.GreetingQueue.Verify(x => x.ReceiveAsync(), Times.Exactly(expectedCount));
            ctx.GreetingManager.Verify(x => x.ReceiveAsync(greeting), Times.Exactly(expectedCount));
        }
    }
}
