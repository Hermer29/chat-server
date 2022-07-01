namespace ChatServer.Core.Exceptions
{

    [Serializable]
    public class DeserializationFailedException : Exception
    {
        public DeserializationFailedException() { }
        public DeserializationFailedException(string message) : base(message) { }
        public DeserializationFailedException(string message, Exception inner) : base(message, inner) { }
        protected DeserializationFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
