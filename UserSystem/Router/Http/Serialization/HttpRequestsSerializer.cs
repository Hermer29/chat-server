using System.Text.RegularExpressions;

using ChatServer.Core.Http.Abstract;
using ChatServer.Core.Http.Requests;
using ChatServer.Core.Http.Responses;
using ChatServer.UserSystem.Router.Sockets;

using Hermer29.ObservableExtensions;

using Newtonsoft.Json;

namespace ChatServer.UserSystem.Router.Http.Serialization
{
    internal class HttpRequestsSerializer : IHttpRequestSerializer
    {
        private readonly IHttpRequestsFactory _factory;
        private Dictionary<string, Action<IResponseWrapper>> _actions;

        public HttpRequestsSerializer(IHttpRequestsFactory factory)
        {
            const string AnythingOrNothing = @"[\s\S]{0,}";
            _factory = factory;
            _actions = new Dictionary<string, Action<IResponseWrapper>>
            {
                { @$"^{AnythingOrNothing}/users$", OnUsersRequestReceived },
                { @$"^{AnythingOrNothing}/message/new$", OnRequestNewMessage},
                { @$"^{AnythingOrNothing}/register$", OnRegisterRequestReceived},
                { @$"^{AnythingOrNothing}/message/all$", OnRequestMessages},
                { @$"^{AnythingOrNothing}/me$", OnAccountInfoRequest}
            };

            Subscribe();
        }

        public event Action<IResponseWrapper<MessagesRequest, MessagesResponse>>? MessagesRequested;
        public event Action<IResponseWrapper<UsersRequest, UsersResponse>>? UsersRequested;
        public event Action<IResponseWrapper<RegisterRequest, NullResponse>>? RegisterRequested;
        public event Action<IResponseWrapper<NewMessageRequest, NullResponse>>? NewMessageReceived;
        public event Action<IResponseWrapper<SelfDataRequest, SingleUserResponse>>? AccountInfoRequested;

        private void Subscribe()
        {
            _factory.Subscribe(OnRequestReceived);
        }

        private void OnRequestReceived(IResponseWrapper request)
        {
            Console.WriteLine($"Request {request.RequestPath}");
            foreach(var action in _actions)
            {
                var isMatch = Regex.IsMatch(request.RequestPath, action.Key);
                if (isMatch == false)
                    continue;
                action.Value.Invoke(request);
                break;
            }
        }

        private void OnUsersRequestReceived(IResponseWrapper request)
        {
            Console.WriteLine("Sending users");
            var value = JsonConvert.DeserializeObject<UsersRequest>(request.Raw);
            var wrapper = new GenericResponseWrapper<UsersRequest, UsersResponse>(value, request);
            UsersRequested?.Invoke(wrapper);
        }

        private void OnRequestMessages(IResponseWrapper request)
        {
            Console.WriteLine("Sending messages");
            var value = JsonConvert.DeserializeObject<MessagesRequest>(request.Raw);
            var wrapped = new GenericResponseWrapper<MessagesRequest, MessagesResponse>(value, request);
            MessagesRequested?.Invoke(wrapped);
        }

        private void OnRequestNewMessage(IResponseWrapper request)
        {
            Console.WriteLine("Requesting new message");
            var value = JsonConvert.DeserializeObject<NewMessageRequest>(request.Raw);
            var wrapped = new GenericResponseWrapper<NewMessageRequest, NullResponse>(value, request);
            NewMessageReceived?.Invoke(wrapped);
        }

        private void OnRegisterRequestReceived(IResponseWrapper request)
        {
            Console.WriteLine("Receiving register request");
            var value = JsonConvert.DeserializeObject<RegisterRequest>(request.Raw);
            var wrapped = new GenericResponseWrapper<RegisterRequest, NullResponse>(value, request);
            RegisterRequested?.Invoke(wrapped);
        }

        private void OnAccountInfoRequest(IResponseWrapper request)
        {
            var value = JsonConvert.DeserializeObject<SelfDataRequest>(request.Raw);
            var wrapped = new GenericResponseWrapper<SelfDataRequest, SingleUserResponse>(value, request);
            AccountInfoRequested?.Invoke(wrapped);
        }
    }
}
