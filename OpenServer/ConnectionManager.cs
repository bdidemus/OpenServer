using System;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;

namespace OpenServer
{
    public class ConnectionManager
    {
        public ConnectionManager()
        {
            mThreads = new LinkedList<Thread>();
            mClientConnections = new LinkedList<ClientConnection>();
        }

        public void handleConnection(Socket socket)
        {
            ClientConnection clientConnection = new ClientConnection(socket);
            Thread thread = new Thread( () => AcceptNewClient(clientConnection) );

            mThreads.AddFirst(thread);
            mClientConnections.AddFirst(clientConnection);
        }

        public void broadcast(byte[] data)
        {
            Console.WriteLine("FIXME: broadcast function stub");
        }
           
        public static AcceptNewClientDelegate AcceptNewClient;

        LinkedList<Thread> mThreads;
        LinkedList<ClientConnection> mClientConnections;
    }
}

