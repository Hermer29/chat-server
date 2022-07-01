using System.Data;
using ChatServer.Core.Exceptions;
using ChatServer.Core.Http.Abstract;
using ChatServer.Core.Http.Requests;
using ChatServer.Core.Http.Responses;
using ChatServer.UserSystem.Router.Http;
using ChatServer.UserSystem.Router.Sockets;

namespace ChatServer.UserSystem.DomainModel
{
    internal class HttpMessagesHandler
    {
        private readonly IHttpRequestSerializer _requests;
        private readonly UserFactory _authentication;
        private readonly UsersRepository _users;

        public HttpMessagesHandler(IHttpRequestSerializer requests, UserFactory authentication, UsersRepository users)
        {
            _requests = requests;
            _authentication = authentication;
            _users = users;
        }

        public void Initialize()
        {
            _requests.RegisterRequested += RegisterUser;
            _requests.MessagesRequested += SendMessages;
            _requests.UsersRequested += SendUsers;
            _requests.NewMessageReceived += RegisterMessage;
            _requests.AccountInfoRequested += SendBackUserInfo;
        }

        private void SendBackUserInfo(IResponseWrapper<SelfDataRequest, SingleUserResponse> obj)
        {
            var deviceId = obj.Request.deviceId;
            var isAccountExists = IsAccountExists(deviceId);
            if(isAccountExists == false)
            {
                obj.Respond(new SingleUserResponse
                {
                    faulted = true,
                    reason = nameof(NotAuthenticatedException)
                });
                return;
            }
            var user = _authentication.GetUserByDeviceId(deviceId);
            obj.Respond(new SingleUserResponse
            {
                id = user.Id,
                color = user.Color,
                userName = user.UserName
            });
        }

        private void RegisterMessage(IResponseWrapper<NewMessageRequest, NullResponse> requestWrapper)
        {
            var deviceId = requestWrapper.Request.DeviceId;
            var userExists = _authentication.HasAccount(deviceId);
            if(userExists == false)
            {
                requestWrapper.Respond(
                    new NullResponse
                    {
                        faulted = true,
                        reason = nameof(NotAuthenticatedException)
                    });
                return;
            }    
            var message = requestWrapper.Request.Message;
            _authentication.GetUserByDeviceId(deviceId).SaveMessage(message);
            requestWrapper.Respond(new NullResponse());
        }

        private bool IsAccountExists(string deviceId)
        {
            return _authentication.HasAccount(deviceId);
        }

        private void RegisterUser(IResponseWrapper<RegisterRequest, NullResponse> request)
        {
            var isAlreadyExists = IsAccountExists(request.Request.deviceId);
            NullResponse response = new NullResponse();
            if (isAlreadyExists)
            {
                response = new NullResponse
                {
                    faulted = true,
                    reason = nameof(AccountException)
                };
            }
            else
            {
                _authentication.RegisterUser(
                    request.Request.deviceId,
                    request.Request.name,
                    request.Request.color);
            }

            request.Respond(response);
        }

        private void SendMessages(IResponseWrapper<MessagesRequest, MessagesResponse> data)
        {
            var isRegistered = _authentication.HasAccount(data.Request.DeviceId);
            if (isRegistered == false)
            {
                data.Respond(new MessagesResponse
                {
                    faulted = true,
                    reason = nameof(NotAuthenticatedException)
                });
                return;
            }
            var lastMessages = _users.GetLastMessages();
            if (data.Request.OwnedMessages != null && data.Request.OwnedMessages.Any())
                lastMessages = lastMessages.ExceptBy(data.Request.OwnedMessages, x => x.UserId).ToArray();
            data.Respond(new MessagesResponse
            {
                Messages = lastMessages
            });
        }

        private void SendUsers(IResponseWrapper<UsersRequest, UsersResponse> responseWrapper)
        {
            var deviceId = responseWrapper.Request.DeviceId;
            var isAuthenticated = _authentication.HasAccount(deviceId);
            if (isAuthenticated == false)
            {
                responseWrapper.Respond(new UsersResponse
                {
                    faulted = true,
                    reason = nameof(NotAuthenticatedException)
                });
                return;
            }

            responseWrapper.Respond(new UsersResponse
            {
                Users = _users.GetAllUsers()
                    .Select(x => SingleUserResponse.FromUserModel(x))
            });
        }
    }
}
