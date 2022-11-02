using System;
using System.Linq;
using System.Security.Cryptography;

namespace TeamCord.Core
{
    /// <summary>
    /// Logic to store encrypted data under user profile
    /// </summary>
    public class PluginUserCredential
    {
        public byte[] Entropy { get; }
        public byte[] CipherText { get; }

        public PluginUserCredential(byte[] entropy, byte[] cipherText)
        {
            Entropy = entropy;
            CipherText = cipherText;
        }

        /// <summary>
        /// Encrypts the given data
        /// </summary>
        /// <param name="secureData"></param>
        /// <param name="cipherText"></param>
        /// <param name="entropy"></param>
        public static PluginUserCredential StoreData(byte[] secureData)
        {
            Logging.Log("Encrypting data...", LogLevel.LogLevel_DEBUG);
            byte[] tempentropy = new byte[20];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                Logging.Log("Generating random entropy for encryption", LogLevel.LogLevel_DEBUG);
                rng.GetBytes(tempentropy);
            }
            byte[] ciphertext = ProtectedData.Protect(secureData.ToArray(), tempentropy,
                DataProtectionScope.CurrentUser);
            return new PluginUserCredential(tempentropy, ciphertext);
        }

        /// <summary>
        /// Decrypts the stored data and returns it as byte array
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="entropy"></param>
        /// <returns></returns>
        public byte[] GetStoredData()
        {
            try
            {
                Logging.Log("Trying to decrypt stored data...", LogLevel.LogLevel_DEBUG);
                return ProtectedData.Unprotect(CipherText, Entropy, DataProtectionScope.CurrentUser);
            }
            catch (Exception ex)
            {
                Logging.Log(ex);
                return new byte[0];
            }
        }
    }
}