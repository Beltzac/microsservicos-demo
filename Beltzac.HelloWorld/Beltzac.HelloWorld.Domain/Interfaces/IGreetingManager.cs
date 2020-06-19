using System.Threading.Tasks;

namespace Beltzac.HelloWorld.Domain
{
    public interface IGreetingManager
    {
        Task SendAsync(Greeting greeting);
        Task ReceiveAsync(Greeting greeting);
    }
}