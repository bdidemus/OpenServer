using System;
using OpenServer;

namespace OpenServerTest
{
    public class TestServer
    {
        private Server mServer;

        public TestServer()
        {
            mServer = new Server(3383, new AcceptNewClientDelegate(AcceptNewClient));
        }

        public static void AcceptNewClient(ClientConnection ClientConnection)
        {
            Console.WriteLine("Accepted client!");

            Client localClient = new Client(ClientConnection);
            localClient.Listen();
        }

        public void StartSrver()
        {
            mServer.StartServer();
        }
    }
}

