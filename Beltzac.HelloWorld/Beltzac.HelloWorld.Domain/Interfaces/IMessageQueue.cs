using Beltzac.HelloWorld.Domain;

namespace Beltzac.HelloWorld.Infrastructure
{
    public interface IMessageQueue
    {
        void Write(Message text);
        Message Read();
    }
}