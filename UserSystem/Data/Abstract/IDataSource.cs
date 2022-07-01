namespace ChatServer.UserSystem.Data.Abstract
{
    internal interface IDataSource : IUserDataSource, IAuthenticationDataSource, IUserDirectoryDataSource
    {
    }
}
