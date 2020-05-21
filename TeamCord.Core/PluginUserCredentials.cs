using System;
using System.Linq;
using System.Security.Cryptography;

namespace TeamCord.Core
{
    public struct PluginUserCredentials
    {
        public byte[] Entropy { get; }
        public byte[] CipherText { get; }

        public PluginUserCredentials(byte[] entropy, byte[] cipherText)
        {
            Entropy = entropy;
            CipherText = cipherText;
        }

        /// <summary>
        /// Encrypts the given password
        /// </summary>
        /// <param name="securePassword"></param>
        /// <param name="cipherText"></param>
        /// <param name="entropy"></param>
        public static PluginUserCredentials StorePassword(byte[] securePassword)
        {
            byte[] tempentropy = new byte[20];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(tempentropy);
            }

            byte[] ciphertext = ProtectedData.Protect(securePassword.ToArray(), tempentropy,
                DataProtectionScope.CurrentUser);
            return new PluginUserCredentials(tempentropy, ciphertext);
        }

        /// <summary>
        /// Decrypts the stored password and returns it as byte array
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="entropy"></param>
        /// <returns></returns>
        public byte[] GetStoredPassword()
        {
            try
            {
                return ProtectedData.Unprotect(CipherText, Entropy, DataProtectionScope.CurrentUser);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}