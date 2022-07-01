
using ChatServer.Core.Sockets.Abstract;

namespace ChatServer.UserSystem.Router.Sockets
{
    internal class SocketConnectionSerializer : ISocketConnectionSerializer
    {
        private readonly ISocketConnection _connection;
        private const int AcknowledgementTimeout = 5;

        public SocketConnectionSerializer(ISocketConnection connection)
        {
            _connection = connection;
        }

        public string GetDeviceId()
        {
            _connection.SendMessage(((int)MessageType.Login).ToString());
            return _connection.ReceiveMessage();
        }

        public bool CheckConnection()
        {
            _connection.SendMessage(((int)MessageType.CheckConnection).ToString());
            var receivingAcknowledgement = new Task<string>(_connection.ReceiveMessage);
            return receivingAcknowledgement.Wait(TimeSpan.FromSeconds(AcknowledgementTimeout));
        }

        public void SendMessagesUpdateRequest()
        {
            _connection.SendMessage(((int)MessageType.NewMessage).ToString());
        }

        public void SendUsersUpdateRequest()
        {
            _connection.SendMessage(((int)MessageType.UsersUpdated).ToString());
        }

        public void SendError(Exception exception)
        {
            _connection.SendMessage(exception.GetType().ToString());
        }
    }
}
