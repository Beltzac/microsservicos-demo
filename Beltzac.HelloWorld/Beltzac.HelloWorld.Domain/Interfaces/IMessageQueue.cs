using Beltzac.HelloWorld.Domain;
using System.Threading.Tasks;

namespace Beltzac.HelloWorld.Infrastructure
{
    public interface IMessageQueue
    {
        Task WriteAsync(Message text);
        Task<Message> ReadAsync();
    }
}