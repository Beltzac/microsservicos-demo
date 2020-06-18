using Beltzac.HelloWorld.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beltzac.HelloWorld.Domain
{
    class HelloWorldBusiness : IHelloWorldBusiness
    {
        private const string HELLO_TEXT = "Hello World";

        private readonly IMicroServiceIdentification _microServiceIdentification;
        private readonly IClock _clock;
        private readonly IMessageQueue _messageQueue;
        private readonly IConsole _console;

        public HelloWorldBusiness(IMicroServiceIdentification microServiceIdentification, IClock clock, IMessageQueue messageQueue, IConsole console)
        {
            _microServiceIdentification = microServiceIdentification;
            _clock = clock;
            _messageQueue = messageQueue;
            _console = console;
        }

        public void Receive(Message message)
        {
            _console.Write($"{message.SenderId}: sent {message.SentAt}: received {_clock.GetNow()} >>> {message.Id} - {message.Payload}");
        }

        public void Send()
        {
            var message = new Message(_microServiceIdentification.Id, _clock.GetNow(), HELLO_TEXT);
            _messageQueue.Write(message);
        }
    }
}
