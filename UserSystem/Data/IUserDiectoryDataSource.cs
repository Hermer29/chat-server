namespace ChatServer.UserSystem.Data
{
    internal interface IUserDiectoryDataSource
    {
        UserDto[] GetAllUsers();
    }
}
