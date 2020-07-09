using System;
using System.Linq;
using System.Security.Cryptography;

namespace TeamCord.Core
{
    public struct PluginUserCredential
    {
        public byte[] Entropy { get; }
        public byte[] CipherText { get; }

        public PluginUserCredential(byte[] entropy, byte[] cipherText)
        {
            Entropy = entropy;
            CipherText = cipherText;
        }

        /// <summary>
        /// Encrypts the given password
        /// </summary>
        /// <param name="secureData"></param>
        /// <param name="cipherText"></param>
        /// <param name="entropy"></param>
        public static PluginUserCredential StoreData(byte[] secureData)
        {
            byte[] tempentropy = new byte[20];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(tempentropy);
            }

            byte[] ciphertext = ProtectedData.Protect(secureData.ToArray(), tempentropy,
                DataProtectionScope.CurrentUser);
            return new PluginUserCredential(tempentropy, ciphertext);
        }

        /// <summary>
        /// Decrypts the stored password and returns it as byte array
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="entropy"></param>
        /// <returns></returns>
        public byte[] GetStoredData()
        {
            try
            {
                return ProtectedData.Unprotect(CipherText, Entropy, DataProtectionScope.CurrentUser);
            }
            catch (Exception ex)
            {
                Logging.Log(ex);
                return null;
            }
        }
    }
}