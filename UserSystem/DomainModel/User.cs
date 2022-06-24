using ChatServer.UserSystem.Data;

namespace ChatServer.UserSystem
{
    public class User
    {
        private readonly IUserDataSource _dataSource;

        internal User(int id, string userName, string color, IUserDataSource dataSource)
        {
            Id = id;
            UserName = userName;
            Color = color;
            _dataSource = dataSource;
        }

        internal User((int id, string userName, string color) data, IUserDataSource dataSource) : this(data.id, data.userName, data.color, dataSource) { }

        internal User(UserModel user, IUserDataSource dataSource) : this(user.Id, user.UserName, user.Color, dataSource) { }

        public int Id { get; }
        public string UserName { get; }
        public string Color { get; }

        public void SendMessage(string message)
        {
            _dataSource.SendMessage(message, Id);
        }
    }
}