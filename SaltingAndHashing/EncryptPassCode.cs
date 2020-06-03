using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace SaltingAndHashing
{
    public class EncryptPassCode
    {
        public void GenerateSaltSHA256()
        {
            var salt = CreateSalt(10);
            var hashpassword = GenerateSHA256Hash("test", salt);
            Console.WriteLine(hashpassword);
        }

        public string CreateSalt(int size)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public string GenerateSHA256Hash(string input, string salt)
        {
            
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            SHA256Managed sha256String = new SHA256Managed();
            byte[] hash = sha256String.ComputeHash(bytes);
            return ByteArrayToHexString(hash);
        }
        static byte[] GenerateSaltedHash(string input, int size)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            byte[] salt = new byte[size];
            rng.GetBytes(salt);
            
            
            byte[] plainText = Encoding.UTF8.GetBytes(input);
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes = 
                new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);            
        }
        public string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (var b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
                
            }
            return hex.ToString();
        }

        public byte[] HextStringTOByteArray(string hex)
        {
            int numberChars = hex.Length;
            byte[]bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i++)
            {
                bytes[1 / 2] = Convert.ToByte(hex.Substring(1, 2), 16);
            }

            return bytes;
        }
    }
}