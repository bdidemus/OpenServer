using System;
using System.Security.Cryptography;

namespace OpenServer
{
    class Encryption
    {
        private string privateKeyFileName;
        private string publicKeyFileName;

        public Encryption()
        {

        }

        public string encrypt(string message)
        {
            string encryptedMsg = string.Empty;

            return encryptedMsg;
        }

        public string decrypt(string message)
        {
            string decryptedMsg = string.Empty;


            return decryptedMsg;
        }

        public byte[] encrypt(byte[] data)
        {
            byte[] encryptedData = null;

            return encryptedData;
        }

        public byte[] decrypt( byte[] data )
        {
            byte[] decryptedData = null;

            return decryptedData;
        }
    }
}
