using System.Threading.Tasks;

namespace Beltzac.HelloWorld.Domain
{
    public interface IWorldGreeter
    {
        Task ProcessNewGreetAsync(Greet message);
        Task SendAsync();
    }
}