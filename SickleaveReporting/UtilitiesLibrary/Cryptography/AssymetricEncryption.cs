using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
/// <summary>
/// Vinberg: Help functions related to Asymmetric encryption
/// </summary>
namespace Utilities.AsymmetricEncryption
{
    /// <summary>
    /// Vinberg: RSA encryption related help functions 
    /// </summary>
    public static class RSAEncryption
    {
        private static int keySize = 4096;

        public class RSAKeys
        {
            public string PublicKey { get; set; }
            public string PublicAndPrivateKey { get; set; }
        }

        //public static RSAKeys GenerateKeys()
        //{
        //    RSAKeys keys = new RSAKeys();
        //    RSACryptoServiceProvider RSAprovider = new RSACryptoServiceProvider(keySize);
        //    keys.PublicKey = RSAprovider.ToXmlString(false);
        //    keys.PublicAndPrivateKey = RSAprovider.ToXmlString(true);
        //    return keys;
        //}

        public static string EncryptText(string text, string publicKeyXml)
        {
            var encrypted = Encrypt(Encoding.UTF8.GetBytes(text), publicKeyXml);
            return Convert.ToBase64String(encrypted);
        }
        private static byte[] Encrypt(byte[] data, string publicKeyXml)
        {
            using (var provider = new RSACryptoServiceProvider(keySize))
            {
                provider.FromXmlString(publicKeyXml);
                return provider.Encrypt(data, true);
            }
        }

        public static string DecryptText(string text, string publicAndPrivateKeyXml)
        {
            var decrypted = Decrypt(Convert.FromBase64String(text), publicAndPrivateKeyXml);
            return Encoding.UTF8.GetString(decrypted);
        }
        private static byte[] Decrypt(byte[] data, string publicAndPrivateKeyXml)
        {
            using (var provider = new RSACryptoServiceProvider(keySize))
            {
                provider.FromXmlString(publicAndPrivateKeyXml);
                return provider.Decrypt(data, true);
            }
        }


    }
}
