namespace ChatServer.Core.Http.Abstract
{
    public interface IResponseWrapper
    {
        string RequestPath { get; }
        string Raw { get; }
        void Respond(string message, int statusCode);
    }

    public interface IResponseWrapper<TRequested, TRespond>
    {
        TRequested Request { get; }
        void Respond(TRespond message);
    }
}
