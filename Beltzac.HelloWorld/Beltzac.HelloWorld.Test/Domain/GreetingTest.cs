using Beltzac.HelloWorld.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Beltzac.HelloWorld.Test.Domain
{
    [TestClass]
    public class GreetingTest
    {
        [TestMethod]
        public void FactoryCreateNew()
        {
            string randomMessage = "Testing123";

            var greeting = Greeting.Factory.CreateNew(randomMessage);

            Assert.AreNotEqual(Guid.Empty, greeting.Id);
            Assert.AreEqual(randomMessage, greeting.PoliteMessage);
            Assert.IsNull(greeting.SentAt);
            Assert.IsNull(greeting.SentBy);
        }

        [TestMethod]
        public void FactoryCreateDefault()
        {
            var greeting = Greeting.Factory.CreateDefault();

            Assert.AreNotEqual(Guid.Empty, greeting.Id);
            Assert.AreEqual(Greeting.DEFAULT_GREETINGS, greeting.PoliteMessage);
            Assert.IsNull(greeting.SentAt);
            Assert.IsNull(greeting.SentBy);
        }

        [TestMethod]
        public void FactoryCreateFromQueue()
        {
            string randomMessage = "Testing123";
            DateTime sentAt = DateTime.Parse("2020-10-01 20:20:20");
            Guid idMessage = Guid.NewGuid();
            Guid sentBy = Guid.NewGuid();

            var greeting = Greeting.Factory.CreateFromQueue(idMessage, sentBy, sentAt, randomMessage);

            Assert.AreNotEqual(Guid.Empty, greeting.Id);
            Assert.AreEqual(idMessage, greeting.Id);
            Assert.AreEqual(randomMessage, greeting.PoliteMessage);
            Assert.AreEqual(sentAt, greeting.SentAt);
            Assert.AreEqual(sentBy, greeting.SentBy);
        }
    }
}
