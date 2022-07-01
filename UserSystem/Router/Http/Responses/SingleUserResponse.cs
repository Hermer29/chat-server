using ChatServer.Core.Http.Responses;
using ChatServer.UserSystem.Data.EntityFramework;

namespace ChatServer.UserSystem.Router.Http
{
    /// <summary>
    /// with json semantics
    /// </summary>
    internal class SingleUserResponse : FaultableResult
    {
        public int id { get; set; }
        public string color { get; set; }
        public string userName { get; set; }

        public static SingleUserResponse FromUserModel(UserModel model)
        {
            return new SingleUserResponse
            {
                id = model.Id,
                color = model.Color,
                userName = model.UserName
            };
        }
    }
}
