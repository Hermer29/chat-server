using ChatServer.UserSystem.Data.EntityFramework;

namespace ChatServer.UserSystem.Data.Abstract
{
    internal interface IAuthenticationDataSource
    {
        bool HasRowWithDeviceId(string deviceId);
        void AddRow(string deviceId, string userName, string color);
        UserModel GetUserWithDeviceId(string deviceId);
    }
}
