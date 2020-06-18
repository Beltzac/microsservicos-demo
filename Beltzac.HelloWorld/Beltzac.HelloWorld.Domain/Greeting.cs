using System;
using System.Collections.Generic;
using System.Text;

namespace Beltzac.HelloWorld.Domain
{
    public class Greeting
    {
        private const string DEFAULT_GREETINGS = "Hello World";

        protected Greeting()
        {
        }

        public Guid Id { get; set; }
        public Guid? SentBy { get; set; }
        public DateTime? SentAt { get; set; }
        public string PoliteMessage { get; set; }

        public override string ToString()
        {
            string description = $"Id: {Id} - Message: {PoliteMessage}";

            if (SentBy != null)
                description += $" - Timestamp: {SentAt} - Origin: {SentBy}";

            return description;
        }

        public static class Factory
        {
            public static Greeting CreateNew(string politeMessage)
            {
                return new Greeting
                {
                    Id = Guid.NewGuid(),
                    PoliteMessage = politeMessage
                };
            }

            public static Greeting CreateDefault()
            {
                return CreateNew(Greeting.DEFAULT_GREETINGS);
            }

            public static Greeting CreateFromQueue(Guid id, Guid sentBy, DateTime sentAt, string politeMessage)
            {
                return new Greeting
                {
                    Id = id,
                    SentAt = sentAt,
                    SentBy = sentBy,
                    PoliteMessage = politeMessage
                };
            }
        }
    }
}
