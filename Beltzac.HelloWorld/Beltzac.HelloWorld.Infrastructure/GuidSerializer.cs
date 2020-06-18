using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beltzac.HelloWorld.Domain
{
    public class GuidSerializer : ISerializer<Guid>, IDeserializer<Guid>
    {
        public Guid Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            if (isNull)
                return Guid.Empty;

            string asString = Encoding.ASCII.GetString(data);

            return Guid.Parse(asString);
        }

        public byte[] Serialize(Guid data, SerializationContext context)
        {
            string asString = data.ToString();
            return Encoding.ASCII.GetBytes(asString);
        }
    }
}
