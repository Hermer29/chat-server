using ChatServer.UserSystem.Data.EntityFramework;

namespace ChatServer.UserSystem.Data.Abstract
{
    internal interface IUserDirectoryDataSource
    {
        UserModel[] GetAllUsers();
        MessageModel[] GetLastMessages(int amount);
    }
}
