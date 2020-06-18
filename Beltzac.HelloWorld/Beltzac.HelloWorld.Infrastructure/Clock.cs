using Beltzac.HelloWorld.Domain;
using System;

namespace Beltzac.HelloWorld.Infrastructure
{
    class Clock : IClock
    {
        /// <summary>
        /// Get Timestamp from somewere
        /// </summary>
        /// <returns></returns>
        public DateTime GetNow()
        {
            return DateTime.Now;
        }
    }
}
