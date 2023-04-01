using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace salerapp.Helpers
{
	public class EncryptionDecryptionHelper
	{
		private static string key = "AAECAwQFBgcICQoLDA0ODw==";
		public static string Encrypt(string plainText)
		{
			return Convert.ToBase64String((new AesManaged { Key = Encoding.UTF8.GetBytes(key), Mode = CipherMode.ECB }).CreateEncryptor().TransformFinalBlock(Encoding.UTF8.GetBytes(plainText), 0, Encoding.UTF8.GetBytes(plainText).Length));
		}

		public static string Decrypt(string cipherText)
		{
			return Encoding.UTF8.GetString((new AesManaged { Key = Encoding.UTF8.GetBytes(key), Mode = CipherMode.ECB }).CreateDecryptor().TransformFinalBlock(Convert.FromBase64String(cipherText), 0, Convert.FromBase64String(cipherText).Length));
		}	
	}
}