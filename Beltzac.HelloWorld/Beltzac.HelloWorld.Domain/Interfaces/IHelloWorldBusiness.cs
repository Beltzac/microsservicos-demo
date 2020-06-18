using System.Threading.Tasks;

namespace Beltzac.HelloWorld.Domain
{
    public interface IHelloWorldBusiness
    {
        Task ReceiveAsync(Message message);
        Task SendAsync();
    }
}