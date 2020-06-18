using System;

namespace Beltzac.HelloWorld.Domain
{
    public interface IClock
    {
        DateTime GetNow();
    }
}