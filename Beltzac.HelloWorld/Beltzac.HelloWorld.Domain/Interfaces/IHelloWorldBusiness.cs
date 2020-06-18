namespace Beltzac.HelloWorld.Domain
{
    public interface IHelloWorldBusiness
    {
        void Receive(Message message);
        void Send();
    }
}