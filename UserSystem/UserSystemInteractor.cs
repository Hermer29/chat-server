using ChatServer.UserSystem.Data;

namespace ChatServer.UserSystem
{
    public class UserSystemInteractor 
    {
        private readonly Authentication _authentication;
        private readonly UsersDirectory _users;

        public UserSystemInteractor()
        {
            var dataSource = SqlDataSource.Initialize();
            _authentication = new Authentication(dataSource);
            _users = new UsersDirectory(dataSource);
        }

        public void SignUp(string deviceId, string userName, string color)
        {
            _authentication.SignUp(deviceId, userName, color);
        }

        public User SignIn(string deviceId)
        {
            return _authentication.SignIn(deviceId);
        }

        public bool HasAccount(string deviceId)
        {
            return _authentication.HasAccount(deviceId);
        }

        public UserDto[] GetAllUsers()
        {
            return _users.GetAllUsers();
        }
    }
}
