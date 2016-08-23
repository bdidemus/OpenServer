using System;
using System.Net.Sockets;
using System.Net;


namespace OpenServer
{
    public delegate void AcceptNewClientDelegate(ClientConnection clientConnection);

	public class Server
	{
        /// <summary>
        /// Inititialize the server using the first valid ipv4 address it finds.
        /// If there is only one network interface this can be used, but it is discouraged.
        /// </summary>
        /// <param name="bindPort">Listen port</param>
        /// <param name="newClientDelegate">Call back for new client connection</param>
        public Server( int bindPort, AcceptNewClientDelegate newClientDelegate )
        {
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            IPAddress ipAddress = null;
            foreach( IPAddress ip in localIPs ) 
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = ip;
                    break;
                }
            }

            if (ipAddress != null)
            {
                Init(ipAddress, bindPort, newClientDelegate);
            }
            else
            {
                throw new Exception("Failed to find a bindable ipv4 address");
            }
        }

        /// <summary>
        /// Initializes ther server using an explicite ipv4 address to listen on.
        /// </summary>
        /// <param name="bindAddress">Bind address</param>
        /// <param name="bindPort">Bind port</param>
        /// <param name="newClientDelegate">Call back for new client connections</param>
        public Server (string bindAddress, int bindPort, AcceptNewClientDelegate newClientDelegate)
		{
            Init(IPAddress.Parse(bindAddress), bindPort, newClientDelegate);
		}

        private void Init(IPAddress bindAddress, int bindPort, AcceptNewClientDelegate newClientDelegate )
        {
            mBindAddress = bindAddress;
            mBindPort = bindPort;

            mConnectionManager = new ConnectionManager();

            ConnectionManager.AcceptNewClient = newClientDelegate;
        }

        /// <summary>
        /// Start listening for incoming connections. New incoming connections spawn a new thread
        /// for each client and return a ClientConnection object via the AcceptNewClientDelegate
        /// </summary>
		public void StartServer()
		{
			mIsRunning = true;

			// Create a TCP/IP v4 socket.
			Socket listener = new Socket(AddressFamily.InterNetwork,
				SocketType.Stream, ProtocolType.Tcp );

            IPEndPoint endPoint = new IPEndPoint (mBindAddress, mBindPort);

			// Bind the socket to the local endpoint and listen for incoming connections.
			try 
            {
				listener.Bind(endPoint);
				listener.Listen(100);

				while (mIsRunning) 
                {
					// Start an asynchronous socket to listen for connections.
					Console.WriteLine("Waiting for a connection...");

                    Socket incomingConnection = listener.Accept();

                    Console.WriteLine("Incoming connection from: " + incomingConnection.RemoteEndPoint.ToString() );

                    mConnectionManager.HandleConnection(incomingConnection);
				}

			} catch (Exception e) {
				Console.WriteLine(e.ToString());
			}

			Console.WriteLine("\nPress ENTER to continue...");
			Console.Read();
		}

        /// <summary>
        /// Signal the server to stop. The prevents new connections for being
        /// spawned but does not close existing client connections. 
        /// </summary>
		public void StopServer() 
		{
			mIsRunning = false;
		}

		public bool IsRunning 
		{
			get 
			{
				return mIsRunning;
			}
		}

        public ConnectionManager ConnectionManager
        {
            get
            {
                return mConnectionManager;
            }
        }

		private bool mIsRunning = false;

        private ConnectionManager mConnectionManager;

        private IPAddress mBindAddress;
        private int mBindPort;
	}
}

