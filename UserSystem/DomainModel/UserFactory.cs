using ChatServer.Core.Exceptions;
using ChatServer.UserSystem.Data.Abstract;

namespace ChatServer.UserSystem.DomainModel
{
    internal class UserFactory
    {
        private readonly IDataSource _dataSource;

        public UserFactory(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public event Action NewMessageReceived;
        public event Action UsersUpdated;

        public bool HasAccount(string deviceId)
        {
            return _dataSource.HasRowWithDeviceId(deviceId);
        }

        public User RegisterUser(string deviceId, string userName, string color)
        {
            if (_dataSource.HasRowWithDeviceId(deviceId))
                throw new AccountException("Account already exists");

            _dataSource.AddRow(deviceId, userName, color);
            UsersUpdated?.Invoke();
            return new User(_dataSource.GetUserWithDeviceId(deviceId), _dataSource);
        }

        public User GetUserByDeviceId(string deviceId)
        {
            if (_dataSource.HasRowWithDeviceId(deviceId) == false)
                throw new AccountException("Account doesn't exists");

            var user = new User(_dataSource.GetUserWithDeviceId(deviceId), _dataSource);
            user.NewMessageReceived += () => NewMessageReceived?.Invoke();
            return user;
        }
    }
}