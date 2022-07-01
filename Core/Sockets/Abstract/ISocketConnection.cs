namespace ChatServer.Core.Sockets.Abstract
{
    public interface ISocketConnection
    {
        public string ReceiveMessage();
        public void SendMessage(string message);
    }
}
