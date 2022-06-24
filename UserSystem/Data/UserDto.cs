namespace ChatServer.UserSystem.Data
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Color { get; set; }

        internal UserDto(UserModel model)
        {
            Id = model.Id;
            UserName = model.UserName;
            Color = model.Color;
        }
    }
}
