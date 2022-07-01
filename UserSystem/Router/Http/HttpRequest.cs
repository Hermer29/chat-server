using System.Net;

using ChatServer.Core.Http.Abstract;

namespace ChatServer.UserSystem.Router.Http
{
    internal class HttpRequest : IResponseWrapper
    {
        private readonly HttpListenerContext _context;

        public HttpRequest(HttpListenerContext context)
        {
            _context = context;
        }

        public string Raw
        {
            get
            {
                return new StreamReader(_context.Request.InputStream).ReadToEnd();
            }
        }

        public string RequestPath => _context.Request.Url?.PathAndQuery ?? "";

        public void Respond(string message, int statusCode)
        {
            _context.Response.StatusCode = statusCode;
            var writer = new StreamWriter(_context.Response.OutputStream);
            writer.Write(message);
            writer.Close();
            _context.Response.Close();
        }
    }
}
