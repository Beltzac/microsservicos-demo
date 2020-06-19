using System.Threading.Tasks;

namespace Beltzac.HelloWorld.Infrastructure
{
    public interface IMessageQueue<T>
    {
        Task SendAsync(T obj);
        Task<T> ReceiveAsync();
    }
}