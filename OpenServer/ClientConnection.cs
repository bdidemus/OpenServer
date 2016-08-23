using System;
using System.Net.Sockets;
using System.Text;

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
            String inString = string.Empty;

            while( this.HasData )
            {
                inString += Encoding.UTF8.GetString(this.RecieveData(1024));
            }

            return inString;
        }

        public void CloseConnection()
        {
            mSocket.Close();
        }

        public bool HasData
        {
            get
            {
                return mSocket.Available > 0;
            }
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

