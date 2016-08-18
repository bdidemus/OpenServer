using System;
using System.Net.Sockets;
using System.Threading;
using System.Collections;

namespace OpenServer
{
    public class ConnectionManager
    {
        public ConnectionManager()
        {
            mThreads = new ArrayList();
            mClientConnections = new ArrayList();
        }

        public void HandleConnection(Socket socket)
        {
            ClientConnection clientConnection = new ClientConnection(socket);
            Thread thread = new Thread( () => AcceptNewClient(clientConnection) );

            mThreads.Add(thread);
            mClientConnections.Add(clientConnection);
        }

        public void Broadcast(byte[] data)
        {
            Console.WriteLine("FIXME: broadcast function stub");
        }

        public void CleanUpConnections()
        {
            for( int i = 0; i < mClientConnections.Count; i++ )
            {
                if (!((ClientConnection)mClientConnections[i]).IsConnected)
                {
                    ((ClientConnection)mClientConnections).CloseConnection();
                    mClientConnections.Remove(i);
                    mThreads.Remove(i);
                    i--;
                }
            }
        }
           
        public static AcceptNewClientDelegate AcceptNewClient;

        ArrayList mThreads;
        ArrayList mClientConnections;
    }
}

