namespace ChatServer.UserSystem.Data.Abstract
{
    internal interface IUserDataSource
    {
        void SendMessage(string message, int senderId);
        void MakeOffline(int id);
        void MakeOnline(int id);
    }
}
