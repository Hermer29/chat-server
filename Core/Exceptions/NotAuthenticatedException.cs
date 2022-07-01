namespace ChatServer.Core.Exceptions
{
    [Serializable]
    public class NotAuthenticatedException : Exception
    {
        public NotAuthenticatedException() { }
        public NotAuthenticatedException(string message) : base(message) { }
        public NotAuthenticatedException(string message, Exception inner) : base(message, inner) { }
        protected NotAuthenticatedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}