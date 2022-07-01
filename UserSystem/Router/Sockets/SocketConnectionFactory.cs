using System.Net;
using System.Net.Sockets;

using ChatServer.Core.Sockets.Abstract;

using Hermer29.ObservableExtensions;

namespace ChatServer.UserSystem.Router.Sockets
{
    internal class SocketConnectionsFactory : ISocketConnectionsFactory
    {
        private TcpListener _socket;
        private ObserversPool<ISocketConnection> _pool = new();
        private CancellationToken _cancel;

        public SocketConnectionsFactory()
        {
            _socket = new TcpListener(IPAddress.Any, 8001);
        }

        public async void Initialize()
        {
            try
            {
                _socket.Start();
                while (true)
                {
                    _cancel.ThrowIfCancellationRequested();
                    var client = await _socket.AcceptTcpClientAsync();
                    _pool.NotifyAll(new SocketConnection(client));
                }
            }
            catch (OperationCanceledException)
            {
                return;
            }
        }

        public IDisposable Subscribe(IObserver<ISocketConnection> observer)
        {
            return _pool.AddObserver(observer);
        }
    }
}
