using ChatServer.Core.Http.Responses;

namespace ChatServer.UserSystem.Router.Http
{
    internal class UsersResponse : FaultableResult
    {
        public IEnumerable<SingleUserResponse> Users { get; init; }
    }
}
