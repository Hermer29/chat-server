using ChatServer.UserSystem.Router.Sockets;

namespace ChatServer.UserSystem.DomainModel
{
    internal class NewMessageUpdateRequest : IRequest
    {
        private readonly ISocketConnectionSerializer _connection;

        public NewMessageUpdateRequest(ISocketConnectionSerializer connection)
        {
            _connection = connection;
        }

        public Task<object> WaitForResponse()
        {
            _connection.SendMessagesUpdateRequest();
            return null;
        }
    }
}
