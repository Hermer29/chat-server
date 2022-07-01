
using Newtonsoft.Json;

namespace ChatServer.UserSystem.Router.Http.Serialization
{
    internal static class SerializableExtensions
    {
        public static string Serialize<TSerializing>(this IEnumerable<TSerializing> source, string rootHeader)
        {
            return JsonConvert.SerializeObject(new Dictionary<string, TSerializing[]>() { { rootHeader, source.ToArray() } });
        }
    }
}
