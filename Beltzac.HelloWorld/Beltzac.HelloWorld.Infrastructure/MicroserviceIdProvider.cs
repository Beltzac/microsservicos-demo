using Beltzac.HelloWorld.Domain;
using System;

namespace Beltzac.HelloWorld.Infrastructure
{
    class MicroserviceIdProvider : IMicroserviceIdProvider
    {
        public MicroserviceIdProvider()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }
    }
}
