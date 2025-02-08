using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MsMedico.Shared.Extensions
{
    public static class StringExtension
    {
        private static readonly string EncryptionKey = "FABIO_FIAP_FLORIPA_FERNANDES_FIVE_FABIO_FIAP_FLORIPA_FERNANDES_FIVE";

        public static string Encrypt(this string texto)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = GetEncryptionKey();
                aes.GenerateIV();
                var iv = aes.IV;
                var encryptor = aes.CreateEncryptor(aes.Key, iv);
                using (var ms = new System.IO.MemoryStream())
                {
                    ms.Write(iv, 0, iv.Length);
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var sw = new System.IO.StreamWriter(cs))
                    {
                        sw.Write(texto);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
        public static string Decrypt(this string texto)
        {
            var fullCipher = Convert.FromBase64String(texto);
            using (var aes = Aes.Create())
            {
                aes.Key = GetEncryptionKey();
                var iv = new byte[aes.BlockSize / 8];
                var cipher = new byte[fullCipher.Length - iv.Length];
                System.Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
                System.Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);
                var decryptor = aes.CreateDecryptor(aes.Key, iv);
                using (var ms = new System.IO.MemoryStream(cipher))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var sr = new System.IO.StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
        private static byte[] GetEncryptionKey()
        {
            var keyBytes = Encoding.UTF8.GetBytes(EncryptionKey);
            Array.Resize(ref keyBytes, 32);
            return keyBytes;
        }
    }
}
