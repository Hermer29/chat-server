namespace ChatServer.UserSystem.Data
{
    internal interface IUserDataSource
    {
        void SendMessage(string message, int senderId);
    }
}
