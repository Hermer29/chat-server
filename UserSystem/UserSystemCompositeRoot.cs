using ChatServer.Core.Http.Abstract;
using ChatServer.Core.Sockets.Abstract;
using ChatServer.UserSystem.Data;
using ChatServer.UserSystem.DomainModel;
using ChatServer.UserSystem.Router.Http;
using ChatServer.UserSystem.Router.Http.Serialization;
using ChatServer.UserSystem.Router.Sockets;

namespace ChatServer.UserSystem
{
    public class UserSystemCompositeRoot 
    {
        private readonly UserFactory _authentication;
        private readonly UsersRepository _users;
        private readonly HttpRequestsFactory _http;
        private readonly SocketConnectionsFactory _socket;
        private readonly SocketConnectionSerializer _socketSerializer;
        private readonly HttpRequestsSerializer _httpSerializer;
        private readonly SocketInteractions _socketInteractions;
        private readonly HttpMessagesHandler _httpInteractions;

        public UserSystemCompositeRoot()
        {
            var dataSource = SqlDataSource.Initialize();
            _authentication = new UserFactory(dataSource);
            _users = new UsersRepository(dataSource);
            _socket = new SocketConnectionsFactory();
            _http = new HttpRequestsFactory();
            _httpSerializer = new HttpRequestsSerializer(_http);
            _socketInteractions = new SocketInteractions(_socket, _authentication);
            _httpInteractions = new HttpMessagesHandler(_httpSerializer, _authentication, _users);
        }

        public void Initialize()
        {
            _http.Initialize();
            _socket.Initialize();
            _socketInteractions.Initialize();
            _httpInteractions.Initialize();
        }
    }
}
