using ChatServer.UserSystem.Data.Abstract;
using ChatServer.UserSystem.Data.EntityFramework;

using Microsoft.EntityFrameworkCore;

namespace ChatServer.UserSystem.Data
{
    internal class SqlDataSource : IDataSource
    { 
        private SqlDataSource() { }

        public static SqlDataSource Initialize()
        {
            var context = new ChatDataContext();
            context.Database.EnsureCreated();
            context.Database.Migrate();
            return new SqlDataSource();
        }

        void IAuthenticationDataSource.AddRow(string deviceId, string userName, string color)
        {
            var context = new ChatDataContext();
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
            var context = new ChatDataContext();
            var query = from user in context.Users 
                        where user.DeviceId == deviceId 
                        select user;
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
            var query = new ChatDataContext();
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
            var context = new ChatDataContext();
            return from user in context.Users where user.DeviceId == deviceId select user;
        }

        private void ThrowIfEmpty<T>(IQueryable<T> values)
        {
            if (values.Any() == false)
                throw new InvalidOperationException("No records found");
        }

        public UserModel[] GetAllUsers()
        {
            var query = new ChatDataContext();
            return query.Users.ToArray();
        }

        public MessageModel[] GetLastMessages(int amount)
        {
            var query = new ChatDataContext();
            return query.Messages.Take(amount).OrderByDescending(x => x.Time).ToArray();
        }

        public void MakeOffline(int id)
        {
            var query = new ChatDataContext();
            query.Users.Where(x => x.Id == id).First().IsOnline = false;
            query.SaveChanges();
        }

        public void MakeOnline(int id)
        {
            var query = new ChatDataContext();
            query.Users.Where(x => x.Id == id).First().IsOnline = true;
            query.SaveChanges();
        }
    }
}
