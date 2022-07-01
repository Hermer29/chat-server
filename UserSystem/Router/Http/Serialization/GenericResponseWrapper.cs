using ChatServer.Core.Http.Abstract;

using Newtonsoft.Json;

namespace ChatServer.UserSystem.Router.Http
{
    internal class GenericResponseWrapper<TIn, TOut> : IResponseWrapper<TIn, TOut>
    {
        private readonly TIn _requestValue;
        private readonly IResponseWrapper _response;

        public GenericResponseWrapper(TIn requestValue, IResponseWrapper response)
        {
            _requestValue = requestValue;
            _response = response;
        }

        public TIn Request => _requestValue;

        public virtual void Respond(TOut message)
        {
            var data = JsonConvert.SerializeObject(message, Formatting.Indented);
            Console.WriteLine(data.ToString());
            _response.Respond(data, 200);
        }
    }
}
