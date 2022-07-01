using ChatServer.UserSystem.Data.Abstract;
using ChatServer.UserSystem.Data.EntityFramework;

namespace ChatServer.UserSystem.DomainModel
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

        public event Action NewMessageReceived;

        public int Id { get; }
        public string UserName { get; }
        public string Color { get; }

        public void SaveMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException(nameof(message));
            _dataSource.SendMessage(message, Id);
        }
    }
}