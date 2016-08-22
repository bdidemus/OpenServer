using System;
using OpenServer;

namespace OpenServerTest
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            TestServer testServer = new TestServer();
            testServer.StartSrver();
        } 
    }
}
