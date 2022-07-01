using ChatServer.Core.Http.Abstract;
using ChatServer.Core.Http.Requests;
using ChatServer.Core.Http.Responses;
using ChatServer.UserSystem.Router.Http;

namespace ChatServer.UserSystem.Router.Sockets
{
    internal interface IHttpRequestSerializer
    {
        event Action<IResponseWrapper<MessagesRequest, MessagesResponse>> MessagesRequested;
        event Action<IResponseWrapper<UsersRequest, UsersResponse>> UsersRequested;
        event Action<IResponseWrapper<RegisterRequest, NullResponse>> RegisterRequested;
        event Action<IResponseWrapper<NewMessageRequest, NullResponse>> NewMessageReceived;
        event Action<IResponseWrapper<SelfDataRequest, SingleUserResponse>>? AccountInfoRequested;
    }
}
