namespace ChatServer.UserSystem.DomainModel
{
    internal interface IRequest<T> : IRequest
    {
        new Task<T> WaitForResponse();
    }

    internal interface IRequest
    {
        Task<object> WaitForResponse();
    }
}
