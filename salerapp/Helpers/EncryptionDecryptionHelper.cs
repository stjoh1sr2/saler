using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace salerapp.Helpers
{
	/// <summary>
	/// Contains encryption and decryption mechanisms for strings.
	/// </summary>
	public class EncryptionDecryptionHelper
	{
		private static string key = "AAECAwQFBgcICQoLDA0ODw==";
		
		/// <summary>
		/// Encrypts a plain text string.
		/// </summary>
		/// <param name="plainText">Plain text to be encrypted.</param>
		/// <returns>The encrypted string.</returns>
		public static string Encrypt(string plainText)
		{
			return Convert.ToBase64String((new AesManaged { Key = Encoding.UTF8.GetBytes(key), Mode = CipherMode.ECB }).CreateEncryptor().TransformFinalBlock(Encoding.UTF8.GetBytes(plainText), 0, Encoding.UTF8.GetBytes(plainText).Length));
		}

        /// <summary>
        /// Decrypts a plain text string.
        /// </summary>
        /// <param name="plainText">Plain text to be decrypted.</param>
        /// <returns>The decrypted string.</returns>
        public static string Decrypt(string cipherText)
		{
			return Encoding.UTF8.GetString((new AesManaged { Key = Encoding.UTF8.GetBytes(key), Mode = CipherMode.ECB }).CreateDecryptor().TransformFinalBlock(Convert.FromBase64String(cipherText), 0, Convert.FromBase64String(cipherText).Length));
		}	
	}
}