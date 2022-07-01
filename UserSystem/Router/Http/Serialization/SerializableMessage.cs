using ChatServer.UserSystem.Data.EntityFramework;

namespace ChatServer.UserSystem.Router.Http.Serialization
{
    /// <summary>
    /// With json semantics
    /// </summary>
    internal class SerializableMessage
    {
        public int id { get; set; }
        public string messageText { get; set; }
        public int senderId { get; set; }
        public string messageColor { get; set; }
        public DateTime time { get; set; }

        public static SerializableMessage FromMessageDto(MessageModel message)
        {
            return new SerializableMessage
            {
                id = message.Id,
                messageText = message.MessageText,
                senderId = message.UserId,
                messageColor = message.Color,
                time = message.Time
            };
        }
    }
}
