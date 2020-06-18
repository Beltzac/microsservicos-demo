using Beltzac.HelloWorld.Domain;
using System.Threading.Tasks;

namespace Beltzac.HelloWorld.Infrastructure
{
    public interface IMessageQueue<T>
    {
        Task WriteAsync(T text);
        Task<T> ReadAsync();
    }
}