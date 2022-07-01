namespace ChatServer.UserSystem.Router.Sockets
{
    public interface ISocketConnectionSerializer
    {
        string GetDeviceId();
        bool CheckConnection();
        void SendMessagesUpdateRequest();
        void SendUsersUpdateRequest();
        void SendError(Exception exception);
    }
}
