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

        public byte[] recieveData()
        {
            byte[] data = null;

            return data;
        }

        public void closeConnection()
        {
            mSocket.Close();
        }

        private Socket mSocket;
	}
}

