using System;
using OpenServer;

namespace OpenServerTest
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Server server = new Server(3383, new AcceptNewClientDelegate(AcceptNewClient));
            server.StartServer();
        }

        public static void AcceptNewClient(ClientConnection ClientConnection)
        {
            Console.WriteLine("Accepted client!");
        }
    }
}
