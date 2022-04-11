using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;

namespace Library
{
    public static class Utility
    {
        public static string CalculateSHA(string userName, string password)
        {
            byte[] data = Encoding.ASCII.GetBytes(userName + password);
            byte[] hashedData;
            SHA256 sHA256 = new SHA256Managed();
            string hashedStr;

            hashedData = sHA256.ComputeHash(data);
            hashedStr = BitConverter.ToString(hashedData).Replace("-", "");
            return hashedStr;
        }
    }
}