using System;
using System.Collections.Generic;
using System.Text;

namespace Beltzac.HelloWorld.Domain
{
    public class Greet
    {
        public Greet()
        {
        }

        public Greet(string politeMessage)
        {
            Id = Guid.NewGuid();
            PoliteMessage = politeMessage;
        }

        public Guid Id { get; set; }
        public Guid? SentBy { get; set; }
        public DateTime? SentAt { get; set; }
        public string PoliteMessage { get; set; }
    }
}
