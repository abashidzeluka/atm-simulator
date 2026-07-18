using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Helpers
{
    public class Hasher
    {
        public static string Hash(string value)
        {
            using MD5 md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(value));
            return Convert.ToHexString(bytes);
        }
    }
}
