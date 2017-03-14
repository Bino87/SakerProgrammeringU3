using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Vinberg: Hashing related help functions
/// </summary>
namespace Utilities.Hashing
{
     /// <summary>
     /// SHA256 hashing related functions
     /// </summary>
    public static class SHA256Hashing
    { 
        /// <summary>
        /// Hash a string value
        /// </summary>
        /// <param name="input">String to hash</param>
        /// <param name="salt">Salt</param>
        /// <returns>The hashed string</returns>
        public static string Hash(string input, string salt)
        {
            input = input + salt;
            string hashedPassword = "";
            SHA256Managed hashProvider = new SHA256Managed();
            hashProvider.Initialize();
            byte[] passwordBytes;
            passwordBytes = Encoding.Unicode.GetBytes(input);
            passwordBytes = hashProvider.ComputeHash(passwordBytes);
            hashedPassword = Convert.ToBase64String(passwordBytes);

            if (hashProvider != null)
            {
                hashProvider.Clear();
                hashProvider = null;
            }
            return hashedPassword;
        }
    }
  
}
