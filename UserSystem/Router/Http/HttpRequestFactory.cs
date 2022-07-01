using System.Net;
using ChatServer.Core.Http.Abstract;
using Hermer29.ObservableExtensions;

namespace ChatServer.UserSystem.Router.Http
{
    internal class HttpRequestsFactory : IHttpRequestsFactory
    {
        private HttpListener _httpListener;
        private const int Port = 8000;
        private ObserversPool<IResponseWrapper> _observers;

        public HttpRequestsFactory()
        {
            _httpListener = new HttpListener();
            _httpListener.Prefixes.Add($"http://localhost:{Port}/");
            _observers = new ObserversPool<IResponseWrapper>();
        }

        public async void Initialize()
        {
            _httpListener.Start();
            while (true)
            {
                var context = await _httpListener.GetContextAsync() ?? throw new NullReferenceException();
                var request = new HttpRequest(context);
                _observers.NotifyAll(request);
            }
        }

        public IAsyncEnumerable<IResponseWrapper> ListenForHttpRequests()
        {
            if (_httpListener.IsListening == false)
                throw new InvalidOperationException("Initialize first");
            return this.AsAsyncEnumerable();
        }

        public IDisposable Subscribe(IObserver<IResponseWrapper> observer)
        {
            return _observers.AddObserver(observer);
        }
    }
}
