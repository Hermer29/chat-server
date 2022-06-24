using Microsoft.EntityFrameworkCore;

namespace ChatServer.UserSystem.Data
{
    internal class SqlDataSource : IDataSource
    { 
        private SqlDataSource() { }

        public static SqlDataSource Initialize()
        {
            using var context = new ChatDataContext();
            context.Database.EnsureCreated();
            context.Database.Migrate();
            return new SqlDataSource();
        }

        void IAuthenticationDataSource.AddRow(string deviceId, string userName, string color)
        {
            using var context = new ChatDataContext();
            var user = new UserModel()
            {
                DeviceId = deviceId,
                UserName = userName,
                Color = color
            };
            context.Users.Add(user);
            context.SaveChanges();
        }

        public UserModel GetRowWithDeviceId(string deviceId)
        {
            using var context = new ChatDataContext();
            var query = from user in context.Users where user.DeviceId == deviceId select user;
            ThrowIfEmpty(query);
            return query.First();
        }

        UserModel IAuthenticationDataSource.GetUserWithDeviceId(string deviceId)
        {
            var query = GetUsersByDeviceId(deviceId);
            ThrowIfEmpty(query);
            return query.First();
        }

        bool IAuthenticationDataSource.HasRowWithDeviceId(string deviceId)
        {
            var query = GetUsersByDeviceId(deviceId);
            return query.Any();
        }

        void IUserDataSource.SendMessage(string messageText, int senderId)
        {
            using var query = new ChatDataContext();
            var senderUser = query.Users.Where(x => x.Id == senderId).First();
            var message = new MessageModel()
            {
                MessageText = messageText,
                Color = senderUser.Color,
                UserId = senderId,
            };
            query.Messages.Add(message);
            query.SaveChanges();
        }

        private IQueryable<UserModel> GetUsersByDeviceId(string deviceId)
        {
            using var context = new ChatDataContext();
            return from user in context.Users where user.DeviceId == deviceId select user;
        }

        private void ThrowIfEmpty<T>(IQueryable<T> values)
        {
            if (values.Any() == false)
                throw new InvalidOperationException("No records with such device id found");
        }

        public UserDto[] GetAllUsers()
        {
            using var query = new ChatDataContext();
            return query.Users.Select(x => new UserDto(x)).ToArray();
        }
    }
}
