using System;

namespace ChatServer.Router
{
    public class ConnectionFactory
    {
        private TcpListener _listener;
        
        public ConnectionFactory()
        {
            _listener = new TcpListener();    
        }
        
        public async Connection GetConnection()
        {
            //TODO: Execute connection building
        }
    }
}
