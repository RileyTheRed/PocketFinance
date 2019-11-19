using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PocketFinance.Utilities
{
    class Security
    {

        /// <summary>
        /// Returns the hash of the parameter
        /// 
        /// TODO : Change this to SecureString
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string getHashSha256(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

    }
}
