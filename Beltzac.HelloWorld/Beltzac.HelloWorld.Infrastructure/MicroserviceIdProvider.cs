using Beltzac.HelloWorld.Domain;
using System;

namespace Beltzac.HelloWorld.Infrastructure
{
    public class MicroserviceIdProvider : IMicroserviceIdProvider
    {
        public MicroserviceIdProvider()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }
    }
}
