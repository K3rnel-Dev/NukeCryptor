using NukeBuilder.Algorithms.Cryptor;
using System;
using System.Collections.Generic;
using System.IO;

namespace NukeBuilder.Algorithms.EncryptHelper
{
    internal class XOREncrypt
    {
        public static byte[] EncryptFile(string filePath, byte[] xorKey)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);
            byte[] encryptedBytes = new byte[fileBytes.Length];

            for (int i = 0; i < fileBytes.Length; i++)
            {
                encryptedBytes[i] = (byte)(fileBytes[i] ^ xorKey[i % xorKey.Length]);
            }
            return encryptedBytes;
        }
        public static byte GenerateRandomByte()
        {
            Random random = new Random();
            return (byte)random.Next(0, 256);
        }

        public static string EncryptChar { get; set; } = Obfuscator.RandomUtils.RandomChar();

        public static readonly byte XorKey = GenerateRandomByte();

        public static string XorEncryptToNumbers(string input, byte key)
        {
            var encrypted = new List<int>();
            foreach (char c in input)
            {
                encrypted.Add(c ^ key);
            }
            return string.Join(EncryptChar, encrypted);
        }
    }
}
