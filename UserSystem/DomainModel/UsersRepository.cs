using ChatServer.UserSystem.Data.Abstract;
using ChatServer.UserSystem.Data.EntityFramework;

namespace ChatServer.UserSystem.DomainModel
{
    internal class UsersRepository
    {
        private readonly IUserDirectoryDataSource _dataSource;

        public UsersRepository(IUserDirectoryDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public UserModel[] GetAllUsers()
        {
            return _dataSource.GetAllUsers();
        }

        public MessageModel[] GetLastMessages()
        {
            return _dataSource.GetLastMessages(20);
        }
    }
}
