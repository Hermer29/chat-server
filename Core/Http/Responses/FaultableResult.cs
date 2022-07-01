namespace ChatServer.Core.Http.Responses
{
    public class FaultableResult
    {
        public bool faulted { get; init; }
        public string reason { get; init; }
    }
}
