using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace OpenClient
{
    public abstract class Client
    {
        private IPAddress ipAddress;
        private int port;
        private Socket socket;
		private byte[] incomingBuffer;

        public Client()
        {
        }

        public Client(IPAddress ipAddress, int port)
        {
            this.ipAddress = ipAddress;
            this.port = port;
        }

        public IPAddress IPAddress
        {
            get
			{
				return this.ipAddress;
			}
			set
			{
				this.ipAddress = value;
			}
        }

        public String IPAddressString
        {
            get
            {
                return (this.ipAddress != null ) ? this.ipAddress.ToString() : string.Empty;
            }
            set
            {
                IPAddress.TryParse(value, out this.ipAddress);
            }
        }

        public String RemoteHostName
        {
            set
            {
                IPHostEntry hosts = Dns.GetHostEntry(value);

                for( int i = 0; i < hosts.AddressList.Length; i++)
                {
                    IPAddress address = hosts.AddressList[i];
                    if( address.AddressFamily == AddressFamily.InterNetwork )
                    {
                        this.IPAddress = address;
                        break;
                    }
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

		public bool HasDataAvailable
		{
			get 
			{
				return this.socket != null && this.socket.Available > 0;
			}
		}

        public bool IsConnected
        {
            get
            {
                return this.socket != null && this.socket.Connected;
            }
        }

		public byte[] IncomingDataBuffer
		{
			get 
			{
				return this.incomingBuffer;
			}
		}

		private static void HandleIncomingASyncData(IAsyncResult result)
		{
			RecieveState state = (RecieveState)result.AsyncState;
			Socket socket = state.Socket;

			int bytesRead = socket.EndReceive (result);
			if (bytesRead > 0) 
			{
				socket.BeginReceive(state.NextBuffer, 0, RecieveState.BufferSize, 0, HandleIncomingASyncData, state);
			} 
			else 
			{
				int bufferTotalSize = state.Buffers.Count * RecieveState.BufferSize;
				byte[] totalBuffer = new byte[bufferTotalSize];

				for (int i = 0; i < state.Buffers.Count; i++) 
				{
					
				}
			}
				
		}

        public void Connect()
        {
            if (this.socket != null && this.socket.Connected)
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
			this.socket.Shutdown (SocketShutdown.Both);
			this.socket.Close ();
        }

        public void SendData(byte[] data)
        {
			if (this.socket != null && !this.socket.Connected) 
			{
				throw new Exception ("Connection is closed");
			}

			this.socket.Send (data);
        }

		public bool RecieveData()
		{
			if (this.socket.Connected) 
			{
				throw new Exception ("Connection is closed");
			}

			bool didRecievedData = false;
			byte[] readBuffer = new byte[1024];

			didRecievedData = this.socket.Receive(readBuffer) > 0;

			this.HandleIncomingData (readBuffer);

			return didRecievedData;
		}

		public void RecieveDataAsync()
		{
			if (this.socket.Connected) 
			{
				throw new Exception ("Connection is closed");
			}

			RecieveState state = new RecieveState (this.socket);
			this.socket.BeginReceive (state.NextBuffer, 0, RecieveState.BufferSize, 0,
				new AsyncCallback (HandleIncomingASyncData), state);

		}

        public abstract void HandleIncomingData(byte[] data);
    }

	class RecieveState
	{
		public RecieveState(Socket socket)
		{
			this.Socket = socket;
			this.Buffers = new List<byte[]> ();
		}

		public byte[] NextBuffer
		{
			get
			{
				byte[] buffer = new byte[BufferSize];
				this.Buffers.Add (buffer);

				return buffer;
			}
		}

		public Socket Socket;
		public const int BufferSize = 1024;
		public List<byte[]> Buffers;
	}
}

