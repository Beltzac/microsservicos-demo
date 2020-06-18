using System;

namespace Beltzac.HelloWorld.Domain
{
    public interface IMicroserviceIdProvider
    {
        Guid Id { get; }
    }
}