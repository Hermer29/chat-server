using ChatServer.Core.Sockets.Abstract;
using ChatServer.UserSystem.Router.Sockets;
using Hermer29.ObservableExtensions;

namespace ChatServer.UserSystem.DomainModel
{
    internal class SocketInteractions
    {
        private readonly ISocketConnectionsFactory _connections;
        private readonly UserFactory _authentication;
        private int ConnectionCheckFrequency = 10;
        private int Seconds2Milliseconds = 1000;
        private Dictionary<ISocketConnectionSerializer, SocketRelationsQueue> _queues = new();

        public SocketInteractions(ISocketConnectionsFactory connections, UserFactory authentication)
        {
            _connections = connections;
            _authentication = authentication;
            _authentication.NewMessageReceived += NotifyNewMessage;
            _authentication.UsersUpdated += NotifyUsersUpdated;
        }

        public async void Initialize()
        {
            await foreach(var connection in _connections.AsAsyncEnumerable())
            {
                ContinueConversation(new SocketConnectionSerializer(connection));
            }
        }

        private async void ContinueConversation(ISocketConnectionSerializer connection)
        {
            while(await Authenticate(connection) == false) {}
            var requestsQueue = new SocketRelationsQueue();
            _queues.Add(connection, requestsQueue);
            var cancelationToken = new CancellationToken();
            while(true)
            {
                await Task.Delay(ConnectionCheckFrequency * Seconds2Milliseconds)  ;
                requestsQueue.Append(new CheckConnectionRequest(connection), (obj) => RemoveConnection(obj, connection, cancelationToken));
                if (cancelationToken.IsCancellationRequested)
                    break;
            }
            NotifyUsersUpdated();
        }

        private void RemoveConnection(object connectionCheck, ISocketConnectionSerializer connection, CancellationToken token)
        {
            var isConnected = (bool)connectionCheck;
            if (isConnected == false)
                _queues.Remove(connection);
        }

        private async Task<bool> Authenticate(ISocketConnectionSerializer connection)
        {
            var deviceId = await Task.Run(connection.GetDeviceId);
            if (_authentication.HasAccount(deviceId))
            {
                _authentication.GetUserByDeviceId(deviceId);
                return true;
            }
            return false;
        }

        private void NotifyNewMessage()
        {
            foreach (var keyValue in _queues)
            {
                keyValue.Value.Append(new NewMessageUpdateRequest(keyValue.Key));
            }
        }

        private void NotifyUsersUpdated()
        {
            foreach(var keyValue in _queues)
            {
                keyValue.Value.Append(new NotifyUsersUpdated(keyValue.Key));
            }
        }
    }
}
