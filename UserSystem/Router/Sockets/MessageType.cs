namespace ChatServer.UserSystem.Router.Sockets
{
    internal enum MessageType : int
    {
        Login,
        NewMessage,
        UsersUpdated,
        CheckConnection,
        Acknowledgement
    }
}
