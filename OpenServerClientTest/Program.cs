using System;
using System.Text;

namespace OpenServerClientTest
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            TestClient testClient = new TestClient();
            testClient.IPAddressString = "192.168.63.60";
            testClient.Port = 3383;

            try
            {
                testClient.Connect();

                byte[] testData;
                string testString = "I'm Mr.MeSeeks. Look at me!";

                testData = Encoding.UTF8.GetBytes(testString.ToCharArray());

                testClient.SendData(testData);

                bool quit = false;
                while( !quit )
                {
                    if( testClient.HasDataAvailable )
                    {
                        testClient.RecieveData();
                    }
                    else
                    {
                       System.Threading.Thread.Sleep(1000);
                    }

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine( ex.ToString() );
            }
            finally
            {
                try
                {
                    testClient.Disconnect();
                }
                catch( Exception ex )
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            Console.ReadKey();
        }
    }
}
