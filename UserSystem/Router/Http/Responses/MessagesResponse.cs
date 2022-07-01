using ChatServer.Core.Http.Responses;
using ChatServer.UserSystem.Data.EntityFramework;

namespace ChatServer.UserSystem.Router.Http
{
    internal class MessagesResponse : FaultableResult
    {
        public IEnumerable<MessageModel> Messages;
    }
}
