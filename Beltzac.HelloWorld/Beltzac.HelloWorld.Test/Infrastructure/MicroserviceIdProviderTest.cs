using Beltzac.HelloWorld.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Beltzac.HelloWorld.Test.Infrastructure
{
    [TestClass]
    public class MicroserviceIdProviderTest
    {
        [TestMethod]
        public void GeneratesValidId()
        {
            var provider = new MicroserviceIdProvider();

            Guid id = provider.Id;

            Assert.AreNotEqual(Guid.Empty, id);
        }

        [TestMethod]
        public void SameProviderSameId()
        {
            var provider = new MicroserviceIdProvider();

            Guid id = provider.Id;
            Guid id2 = provider.Id;

            Assert.AreEqual(id, id2);
        }
    }
}
