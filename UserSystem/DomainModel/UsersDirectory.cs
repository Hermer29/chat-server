using ChatServer.UserSystem.Data;

namespace ChatServer.UserSystem
{
    internal class UsersDirectory
    {
        private readonly IUserDiectoryDataSource _dataSource;

        public UsersDirectory(IUserDiectoryDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public UserDto[] GetAllUsers()
        {
            return _dataSource.GetAllUsers();
        }
    }
}
