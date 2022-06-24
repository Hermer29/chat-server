namespace ChatServer.UserSystem.Data
{
    internal class MessageModel
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public string MessageText { get; set; }

        public int UserId { get; set; }
        public UserModel User { get; set; }
    }
}
