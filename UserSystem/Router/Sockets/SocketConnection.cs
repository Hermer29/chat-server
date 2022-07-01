using System.Net.Sockets;

using ChatServer.Core.Sockets.Abstract;

namespace ChatServer.UserSystem.Router.Sockets
{
    internal class SocketConnection : ISocketConnection
    {
        private readonly TcpClient _client;

        public SocketConnection(TcpClient client)
        {
            _client = client;
        }

        public string ReceiveMessage()
        {
            var network = _client.GetStream();
            var streamReader = new StreamReader(network);
            var line = streamReader.ReadLine();
            return line ?? "";
        }

        public void SendMessage(string message)
        {
            var network = _client.GetStream();
            var writer = new StreamWriter(network);
            writer.WriteLine(message);
            writer.Flush();
        }
    }
}
