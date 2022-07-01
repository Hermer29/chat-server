using ChatServer.UserSystem.Router.Sockets;

namespace ChatServer.UserSystem.DomainModel
{
    internal class NotifyUsersUpdated : IRequest
    {
        private readonly ISocketConnectionSerializer _connection;

        public NotifyUsersUpdated(ISocketConnectionSerializer connection)
        {
            _connection = connection;
        }

        public Task<object> WaitForResponse()
        {
            _connection.SendUsersUpdateRequest();
            return null;
        }
    }
}
