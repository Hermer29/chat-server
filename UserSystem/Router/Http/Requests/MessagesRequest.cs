namespace ChatServer.UserSystem.Router.Http
{
    internal class MessagesRequest
    {
        public IEnumerable<int> OwnedMessages { get; init; }
        public string DeviceId { get; init; }
    }
}
