using System;
using System.Net;
using System.Net.Sockets;

namespace OpenClient
{
    public abstract class Client
    {
        private IPAddress ipAddress;
        private int port;
        private Socket socket;

        public Client(IPAddress ipAddress, int port)
        {
            this.ipAddress = ipAddress;
            this.port = port;
        }

        public IPAddress IPAddress
        {
            get
            {
                return this.IPAddress;
            }
            set
            {
                if (!IPAddress.TryParse(address, this.ipAddress))
                {
                    throw new Exception("Invalid IP Address"); 
                }
            }
        }

        public int Port
        {
            get
            {
                return this.port;
            }

            set
            {
                this.port = value;
            }
        }

        public bool IsConnected
        {
            get
            {
                return this.socket != null && this.socket.Connected;
            }
        }

        public void Connect()
        {
            if (this.socket.Connected)
            {
                throw new Exception("Client is already connected!");
            }

            IPHostEntry ipHostEntry = Dns.GetHostEntry(this.ipAddress);
            IPEndPoint ipEndPoint = new IPEndPoint(this.ipAddress, this.port);

            this.socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            this.socket.Connect(ipEndPoint);           
        }

        public void Disconnect()
        {
            this.socket.Disconnect();
        }

        public void SendData(byte[] data)
        {
            
        }

        public abstract void RecieveData(byte[] data);
    }
}

