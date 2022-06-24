namespace ChatServer.UserSystem.Data
{
    internal class UserModel
    {
        public int Id { get; set; }
        public string DeviceId { get; set; }
        public string Color { get; set; }
        public string UserName { get; set; }

        public List<MessageModel> Messages { get; set; } 
    }
}
