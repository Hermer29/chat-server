namespace ChatServer.Core.Exceptions
{

    [Serializable]
    public class SerializationFailedException : Exception
    {
        public SerializationFailedException() { }
        public SerializationFailedException(string message) : base(message) { }
        public SerializationFailedException(string message, Exception inner) : base(message, inner) { }
        protected SerializationFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
