using System;
using System.Net.Sockets;

namespace OpenServer
{
    public class ClientConnection
	{
        public ClientConnection(Socket socket)
        {
            mSocket = socket;
        }

        public byte[] RecieveData(uint chunkSize)
        {
            byte[] data = new byte[chunkSize];

            mSocket.Receive(data);

            return data;
        }

        public string RecieveString()
        {
            String inString = null;

            return null;
        }

        public void CloseConnection()
        {
            mSocket.Close();
        }

        public bool IsConnected
        {
            get
            {
                return mSocket.Connected;
            }
        }

        private Socket mSocket;
	}
}

