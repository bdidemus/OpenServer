using System;
using OpenServer;

namespace OpenServerTest
{
    public class Client
    {
        private ClientConnection mClientConnection;
        private bool mAlive = true;

        public Client(ClientConnection clientConnection)
        {
            mClientConnection = clientConnection; 
        }

        public void Listen()
        {
            while (mAlive && mClientConnection.IsConnected && 
                mClientConnection.HasData)
            {
                string incomingData = mClientConnection.RecieveString();

                Console.WriteLine("Recieved: " + incomingData);
            }
        }

        public void Send()
        {
            //TODO: Send something
        }

        public bool IsAlive
        {
            get
            {
                return mAlive;
            }
            set
            {
                mAlive = value;
            }
        }
    }
}

