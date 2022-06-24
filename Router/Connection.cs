using System;
using System.Net;
using System.Net.Sockets;

namespace ChatServer.Router
{
    public class Connection
    {
        private TcpListener _socket;
        
        public Connection()
        {
            _socket = new TcpListener();
        }
        
        public void SendMessage(string message)
        {
            //TODO: Send message
        }
        
        public string ReceiveMessage()
        {
            //TODO: Receive message
        }
    }
}
