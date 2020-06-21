using Beltzac.HelloWorld.Application.BackgroundTask;
using Beltzac.HelloWorld.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading;
using System.Threading.Tasks;

namespace Beltzac.HelloWorld.Test.Application
{
    [TestClass]
    public class GreetingProducerWorkerTest
    {
        [TestMethod]
        public async Task ProducerQueuesMessages()
        {
            int expectedCount = 2;
            int count = 0;
            
            var ctx = new UnitTestContext();

            //todo parametrizar tempo do timer
            var hostedService = new GreetingProducerWorker(ctx.GreetingManager.Object, ctx.GetLoggerMock<GreetingProducerWorker>().Object);

            //Cancel after X calls
            ctx.GreetingManager
                .Setup(x => x.SendAsync(It.IsAny<Greeting>()))                
                .Callback(async () =>
                {
                    count++;
                    if (count == expectedCount)
                        await hostedService.StopAsync(CancellationToken.None);
                });


            //act
            await hostedService.StartAsync(CancellationToken.None);
            await Task.Delay(6000); //Time to execute 2x

            ctx.GreetingManager.Verify(x => x.SendAsync(It.IsAny<Greeting>()), Times.Exactly(expectedCount));
        }
    }
}
