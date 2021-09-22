using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace hospi_web_project.Utils
{
    public class EncryptionTool
    {
        public static string SHA256Hash(string pw, string id)
        {

            SHA256 sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(pw));
            byte[] salt = sha.ComputeHash(Encoding.ASCII.GetBytes(id));
            byte[] DesSaltHash = SaltHash(hash, salt);
            StringBuilder stringBuilder = new StringBuilder(DesSaltHash.Length * 2 + (DesSaltHash.Length / 8));
            for (int i = 0; i < DesSaltHash.Length; i++)
            {
                stringBuilder.Append(BitConverter.ToString(DesSaltHash, i, 1));
            }
            string rKey = stringBuilder.ToString().TrimEnd(new char[] { ' ' }).ToLower();
            return rKey;
        }

        public static byte[] SaltHash(byte[] hash, byte[] salt)
        {
            HashAlgorithm sha256 = new SHA256CryptoServiceProvider();
            byte[] combined = salt.Concat(hash).ToArray();
            byte[] hashed = sha256.ComputeHash(combined);
            return hashed;
        }
    }
}
