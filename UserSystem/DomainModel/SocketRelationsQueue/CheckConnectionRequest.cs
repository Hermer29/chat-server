using ChatServer.UserSystem.Router.Sockets;

namespace ChatServer.UserSystem.DomainModel
{
    internal class CheckConnectionRequest : IRequest<bool>
    {
        private readonly ISocketConnectionSerializer _connection;

        public CheckConnectionRequest(ISocketConnectionSerializer connection)
        {
            _connection = connection;
        }

        public async Task<bool> WaitForResponse()
        {
            return await Task.Run(_connection.CheckConnection);
        }

        async Task<object> IRequest.WaitForResponse()
        {
            return await WaitForResponse();
        }
    }
}
