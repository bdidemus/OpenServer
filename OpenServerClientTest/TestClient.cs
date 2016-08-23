using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenClient;

namespace OpenServerClientTest
{
    class TestClient : Client
    {
        public override void HandleIncomingData(Byte[] data)
        {
            Console.WriteLine("Data Incoming!");
        }
    }
}
