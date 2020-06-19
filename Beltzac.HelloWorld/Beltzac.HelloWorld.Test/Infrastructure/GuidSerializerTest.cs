using Beltzac.HelloWorld.Domain;
using Confluent.Kafka;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Beltzac.HelloWorld.Test.Infrastructure
{
    [TestClass]
    public class GuidSerializerTest
    {
        [TestMethod]
        public void CanSerializeBackAndForth()
        {
            var serializer = new GuidSerializer();
            var context = new SerializationContext(MessageComponentType.Key, "topic");
            Guid originalId = Guid.NewGuid();

            var serialized = serializer.Serialize(originalId, context);
            Guid deserializedId = serializer.Deserialize(serialized, false, context);

            Assert.AreEqual(originalId, deserializedId);
        }

        [TestMethod]
        public void NullReturnsEmptyGuid()
        {
            var serializer = new GuidSerializer();
            var context = new SerializationContext(MessageComponentType.Key, "topic");

            Guid deserializedId = serializer.Deserialize(null, true, context);

            Assert.AreEqual(Guid.Empty, deserializedId);
        }
    }
}
