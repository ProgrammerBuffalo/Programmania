using System.Security.Cryptography;

namespace Programmania.Utilities
{
    public static class Encryptor
    {
        private static string key = "b14ca5898a4e4133bbce2ea2315a1916";

        public static string EncryptString(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = System.Text.Encoding.UTF8.GetBytes(key);
                aes.IV = new byte[16];
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                            return System.Convert.ToBase64String(memoryStream.ToArray());
                        }
                    }
                }
            }
        }
        public static string DecryptString(string cipherText)
        {
            byte[] buffer = System.Convert.FromBase64String(cipherText);
            using (Aes aes = Aes.Create())
            {
                aes.Key = System.Text.Encoding.UTF8.GetBytes(key);
                aes.IV = new byte[16]; ;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (System.IO.StreamReader streamReader = new System.IO.StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
