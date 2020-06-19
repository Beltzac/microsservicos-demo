using Beltzac.HelloWorld.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Beltzac.HelloWorld.Test.Domain
{
    [TestClass]
    public class GreetingManagerTest
    {
        [TestMethod]
        public async Task SendCallsLoggerAndQueues()
        {
            var ctx = new UnitTestContext();
            var greeting = Greeting.Factory.CreateDefault();
            var greetingManager = new Mock<GreetingManager>(ctx.MicroserviceIdProvider.Object, ctx.GreetingQueue.Object, ctx.GreetingManagerLogger.Object);
            greetingManager.CallBase = true;

            await greetingManager.Object.SendAsync(greeting);

            greetingManager.Verify(x => x.DisplayGreetingsTraffic(It.IsAny<string>()), Times.Once());
            ctx.GreetingQueue.Verify(x => x.SendAsync(greeting), Times.Once());
        }

        [TestMethod]
        public async Task ReceiveCallsLogger()
        {
            var ctx = new UnitTestContext();
            var greeting = Greeting.Factory.CreateDefault();
            var greetingManager = new Mock<GreetingManager>(ctx.MicroserviceIdProvider.Object, ctx.GreetingQueue.Object, ctx.GreetingManagerLogger.Object);
            greetingManager.CallBase = true;

            await greetingManager.Object.ReceiveAsync(greeting);

            greetingManager.Verify(x => x.DisplayGreetingsTraffic(It.IsAny<string>()), Times.Once());
        }
    }
}
