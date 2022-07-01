namespace ChatServer.UserSystem.DomainModel
{
    internal class SocketRelationsQueue
    {
        private Queue<(IRequest, Action<object>)> _requests = new();
        private bool _executing;

        public void Append(IRequest request, Action<object> callback = null)
        {
            if (_executing == false && _requests.Any() == false)
            {
                ExecuteRequest((request, callback));
                return;
            }

            _requests.Enqueue((request, callback));
        }

        private async void ExecuteRequest((IRequest, Action<object>) request)
        {
            _executing = true;
            var result = await Task.Run(request.Item1.WaitForResponse);
            request.Item2?.Invoke(result);
            if (_requests.Count != 0)
            {
                ExecuteRequest(_requests.Dequeue());
                return;
            }
            _executing = false;
        }
    }
}
