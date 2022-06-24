using ChatServer.UserSystem.Data;

namespace ChatServer.UserSystem
{
    internal class Authentication
    {
        private readonly IDataSource _dataSource;

        public Authentication(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public bool HasAccount(string deviceId)
        {
            return _dataSource.HasRowWithDeviceId(deviceId);
        }

        public User SignUp(string deviceId, string userName, string color)
        {
            if (_dataSource.HasRowWithDeviceId(deviceId))
                throw new InvalidOperationException("Account already exists");

            _dataSource.AddRow(deviceId, userName, color);
            return new User(_dataSource.GetUserWithDeviceId(deviceId), _dataSource);
        }

        public User SignIn(string deviceId)
        {
            if (_dataSource.HasRowWithDeviceId(deviceId) == false)
                throw new InvalidOperationException("Account doesn't exists");

            return new User(_dataSource.GetUserWithDeviceId(deviceId), _dataSource);
        }
    }
}
