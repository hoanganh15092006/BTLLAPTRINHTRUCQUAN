using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace QLBanMayViTinh.Classes
{
    public static class SecurityHelper
    {
        private static readonly byte[] AesKey = Encoding.UTF8.GetBytes("QLBanMayViTinh@SecureKey2026!789");
        private static readonly byte[] AesIv = Encoding.UTF8.GetBytes("InitVector@12345");

        public static string HashPassword(string password, out string salt)
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            salt = Convert.ToBase64String(saltBytes);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(32);
                return Convert.ToBase64String(hash);
            }
        }

        public static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            if (string.IsNullOrEmpty(storedHash) || string.IsNullOrEmpty(storedSalt))
                return false;

            try
            {
                byte[] saltBytes = Convert.FromBase64String(storedSalt);
                using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000))
                {
                    byte[] hash = pbkdf2.GetBytes(32);
                    string computedHash = Convert.ToBase64String(hash);
                    return computedHash == storedHash;
                }
            }
            catch
            {
                return false;
            }
        }

        public static string EncryptAES(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return string.Empty;

            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = AesKey;
                    aes.IV = AesIv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter sw = new StreamWriter(cs))
                            {
                                sw.Write(plainText);
                            }
                        }
                        return "ENC:" + Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch
            {
                return plainText;
            }
        }

        public static string DecryptAES(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return string.Empty;

            if (!cipherText.StartsWith("ENC:"))
                return cipherText;

            try
            {
                string rawCipher = cipherText.Substring(4);
                byte[] cipherBytes = Convert.FromBase64String(rawCipher);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = AesKey;
                    aes.IV = AesIv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    using (MemoryStream ms = new MemoryStream(cipherBytes))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader sr = new StreamReader(cs))
                            {
                                return sr.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch
            {
                return cipherText;
            }
        }
    }
}
