using System;
using System.Collections.Generic;
using System.Text;

namespace Beltzac.HelloWorld.Domain
{
    public class Message
    {
        public Message(Guid senderId, DateTime sentAt, string payload)
        {
            Id = Guid.NewGuid();
            SenderId = senderId;
            SentAt = sentAt;
            Payload = payload;
        }

        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public DateTime SentAt { get; set; }
        public string Payload { get; set; }
    }
}
