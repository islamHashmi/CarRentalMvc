using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CarRental.Helpers
{
    public class HelperClass
    {
        private static readonly string EncryptionKey = "MAKV2SPBNI99212";

        public static string Encrypt(object id)
        {
            string _value = string.Empty;

            byte[] clearBytes = Encoding.Unicode.GetBytes(id.ToString());
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[]
                                                                                {
                                                                                    0x49,
                                                                                    0x76,
                                                                                    0x61,
                                                                                    0x6e,
                                                                                    0x20,
                                                                                    0x4d,
                                                                                    0x65,
                                                                                    0x64,
                                                                                    0x76,
                                                                                    0x65,
                                                                                    0x64,
                                                                                    0x65,
                                                                                    0x76
                                                                                });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    _value = Convert.ToBase64String(ms.ToArray());
                }
            }

            return _value;
        }

        public static string Decrypt(object id)
        {
            string _value = string.Empty;

            byte[] cipherBytes = Convert.FromBase64String(id.ToString());
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[]
                                                                                {
                                                                                    0x49,
                                                                                    0x76,
                                                                                    0x61,
                                                                                    0x6e,
                                                                                    0x20,
                                                                                    0x4d,
                                                                                    0x65,
                                                                                    0x64,
                                                                                    0x76,
                                                                                    0x65,
                                                                                    0x64,
                                                                                    0x65,
                                                                                    0x76
                                                                                });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    _value = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return _value;
        }
    }
}