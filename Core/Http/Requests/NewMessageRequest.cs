namespace ChatServer.Core.Http.Requests
{
    public class NewMessageRequest
    {
        public string DeviceId { get; set; }
        public string Message { get; set; }
    }
}
