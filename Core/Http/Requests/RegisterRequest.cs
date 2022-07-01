namespace ChatServer.Core.Http.Requests
{
    /// <summary>
    /// with json semantics
    /// </summary>
    public class RegisterRequest
    {
        public string color { get; set; } = null!;
        public string name { get; set; } = null!;
        public string deviceId { get; set; } = null!;
    }
}
