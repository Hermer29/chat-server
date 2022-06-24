namespace ChatServer.UserSystem.Data
{
    internal interface IAuthenticationDataSource
    {
        bool HasRowWithDeviceId(string deviceId);
        void AddRow(string deviceId, string userName, string color);
        UserModel GetUserWithDeviceId(string deviceId);
    }
}
