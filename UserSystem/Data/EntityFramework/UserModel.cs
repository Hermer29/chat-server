namespace ChatServer.UserSystem.Data.EntityFramework
{
    internal class UserModel
    {
        public int Id { get; set; }
        public string DeviceId { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public bool IsOnline { get; set; }

        public List<MessageModel> Messages { get; set; } = null!;
    }
}
