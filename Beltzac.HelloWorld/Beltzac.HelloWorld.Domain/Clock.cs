using System;
using System.Collections.Generic;
using System.Text;

namespace Beltzac.HelloWorld.Domain
{
    class Clock : IClock
    {
        public DateTime GetNow()
        {
            return DateTime.Now;
        }
    }
}
