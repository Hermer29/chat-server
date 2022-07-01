using ChatServer.UserSystem;

namespace ChatServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var userSystem = new UserSystemCompositeRoot();
            userSystem.Initialize();
            Console.ReadLine();
        }
    }
}