using System;

namespace Beltzac.HelloWorld.Domain
{
    interface IClock
    {
        DateTime GetNow();
    }
}