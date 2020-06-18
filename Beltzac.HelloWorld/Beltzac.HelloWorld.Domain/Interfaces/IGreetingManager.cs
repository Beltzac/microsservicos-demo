using System.Threading.Tasks;

namespace Beltzac.HelloWorld.Domain
{
    public interface IGreetingManager
    {
        Task ReceiveAsync(Greeting greeting);
        Task SendAsync(Greeting greeting);
    }
}