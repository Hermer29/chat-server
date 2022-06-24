namespace ChatServer.UserSystem.Data
{
    internal interface IDataSource : IUserDataSource, IAuthenticationDataSource, IUserDiectoryDataSource
    {
    }
}
