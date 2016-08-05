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
            while (mAlive && mClientConnection.IsConnected)
            {
                byte[] incomingData = mClientConnection.RecieveData(1024);

                Console.WriteLine("Recieved ", incomingData.Length.ToString(), " bytes");
            }

            Console.WriteLine("Client connection closed");
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

