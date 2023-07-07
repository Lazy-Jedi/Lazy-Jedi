using System.IO;
using System.Security.Cryptography;
using LazyJedi.Extensions;
using Microsoft.Win32;
using UnityEngine;

namespace LazyJedi.Utility
{
    public static class SecurityUtility
    {
        #region KEY, IV & RSA PARAMETER GENERATION METHODS

        /// <summary>
        /// Generate a unique byte array.<br/>
        /// The length of the byte array is 16 bytes by default, 16 bytes = 128 bits.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] GenerateUniqueByteArray(int length = 16)
        {
            byte[] bytes = new byte[length];
            using RandomNumberGenerator generator = RandomNumberGenerator.Create();
            generator.GetBytes(bytes);
            return bytes;
        }

        /// <summary>
        /// Generate a unique AES Key and IV.<br/>
        /// Please store the Key and IV in a safe place. You will need them to decrypt the data.<br/>
        /// You can encrypt the Key and IV using the RSAEncryption method. <br/>
        /// You will need a valid RSA Public Key to encrypt the Key and IV. <br/>
        /// To decrypt the Key and IV, you will need the RSA Private Key.
        /// </summary>
        /// <param name="key">Unique AES Key</param>
        /// <param name="iv">Unique AES IV</param>
        public static void GenerateAESKeyAndIV(out byte[] key, out byte[] iv)
        {
            using Aes aes = Aes.Create();
            key = aes.Key;
            iv = aes.IV;
        }

        /// <summary>
        /// Use this method to generate a RSA Key Pair.
        /// Please note that the key size is 2048 bits by default.<br/>
        /// Please keep your private key safe and secure.
        /// </summary>
        /// <param name="publicKey">The generated public key</param>
        /// <param name="privateKey">The generated private key</param>
        /// <param name="keySize">The size of the key, default size is 2048</param>
        public static void GenerateRSAKeyPair(out RSAParameters publicKey, out RSAParameters privateKey, int keySize = 2048)
        {
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.KeySize = keySize;
            publicKey = rsa.ExportParameters(false); // false to get the public key
            privateKey = rsa.ExportParameters(true); // true to get the private key
        }

        #endregion

        #region AES METHODS

        /// <summary>
        /// Use AES to encrypt string data.<br/>
        /// </summary>
        /// <param name="data">String data to encrypt</param>
        /// <param name="key">The AES Key</param>
        /// <param name="iv">The AES IV</param>
        /// <param name="cipherMode">The Cipher Mode used for Encryption</param>
        /// <param name="paddingMode">The Padding Mode used for Encryption</param>
        /// <returns></returns>
        public static byte[] AESEncryption(
            string data, ref byte[] key, ref byte[] iv,
            CipherMode cipherMode = CipherMode.CBC,
            PaddingMode paddingMode = PaddingMode.PKCS7)
        {
            using Aes aes = Aes.Create();
            ValidateAESSettingsHelper(cipherMode, paddingMode, aes);
            ValidateAESKeyIVHelper(ref key, ref iv, aes);
            using ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);
            using MemoryStream memoryStream = new MemoryStream();
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
            {
                streamWriter.Write(data);
            }
            return memoryStream.ToArray();
        }

        /// <summary>
        /// Use AES to decrypt an encrypted byte data.<br/>
        /// </summary>
        /// <param name="data">The encrypted byte data</param>
        /// <param name="key">Your AES Key</param>
        /// <param name="iv">Your AES IV</param>
        /// <param name="cipherMode">The Cipher Mode determines how the AES Encryption will be done</param>
        /// <param name="paddingMode">The Padding Mode used for Decryption</param>
        /// <returns>The decrypted string</returns>
        public static string AESDecryption(
            byte[] data, byte[] key, byte[] iv,
            CipherMode cipherMode = CipherMode.CBC,
            PaddingMode paddingMode = PaddingMode.PKCS7)
        {
            using Aes aes = Aes.Create();
            ValidateAESSettingsHelper(cipherMode, paddingMode, aes);
            if (!IsAESKeyAndIVValid(key, iv))
            {
                Debug.unityLogger.LogError("Invalid", "The AES Key and IV are not valid.");
                return string.Empty;
            }

            aes.Key = key;
            aes.IV = iv;

            using ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using MemoryStream memoryStream = new MemoryStream(data);
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using StreamReader streamReader = new StreamReader(cryptoStream);
            return streamReader.ReadToEnd();
        }

        #endregion

        #region RSA MEHTODS

        /// <summary>
        /// RSA Encryption is used for encrypting small amounts of data.<br/>
        /// You can use the RSA Encryption to encrypt the AES Key and IV.<br/>
        /// If you do not have a RSA Public or Private Key, you can generate one using the GenerateRSAKeyPair method.<br/>
        /// </summary>
        /// <param name="data">The data to encrypt</param>
        /// <param name="publicKey">The RSA public key used to Encrypt data</param>
        /// <param name="fOAEP">True - Use OAEP Padding ; False - use PKCS#1 v1.5 Padding</param>
        public static byte[] RSAEncryption(byte[] data, RSAParameters publicKey, bool fOAEP = false)
        {
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(publicKey);
            return rsa.Encrypt(data, fOAEP);
        }

        /// <summary>
        /// RSA Decryption is used for decrypting small amounts of data.<br/>
        /// You can use the RSA Decryption to decrypt the AES Key and IV if they were encrypted using RSA.<br/>
        /// If you do not have a RSA Private or Public Key, you can generate one using the GenerateRSAKeyPair method.<br/>
        /// </summary>
        /// <param name="data"></param>
        /// <param name="privateKey"></param>
        /// <param name="fOAEP">True - Use OAEP Padding ; False - use PKCS#1 v1.5 Padding</param>
        /// <returns></returns>
        public static byte[] RSADecryption(byte[] data, RSAParameters privateKey, bool fOAEP = false)
        {
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(privateKey);
            return rsa.Decrypt(data, fOAEP);
        }

        #endregion

        #region IMPORT OR EXPORT RSA KEYS METHODS

        /// <summary>
        /// Imports the byte[] RSA Public and Private Keys and returns a RSAParameters Tuple.<br/>
        /// Key.Item1 = Public RSAParameters<br/>
        /// Key.Item2 = Private RSAParameters<br/>
        /// </summary>
        /// <param name="publicKey">The RSA Public key</param>
        /// <param name="privateKey">The RSA Private key</param>
        /// <returns>RSAParameters Tuple</returns>
        public static (RSAParameters, RSAParameters) ImportRSAXMLKeys(string publicKey, string privateKey)
        {
            if (publicKey.IsNullOrEmpty() || privateKey.IsNullOrEmpty())
            {
                Debug.unityLogger.LogError("RSA", "RSA public or private key is not valid. Please provide both public and private keys.");
                return default;
            }
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);
            RSAParameters publicParameters = rsa.ExportParameters(false);
            rsa.FromXmlString(privateKey);
            RSAParameters privateParameters = rsa.ExportParameters(true);
            return (publicParameters, privateParameters);
        }

        /// <summary>
        /// Exports the Public and Private RSAParameters as a byte[] Tuple.<br/>
        /// Key.Item1 = Public Key<br/>
        /// Key.Item2 = Private Key<br/>
        /// </summary>
        /// <param name="publicKey">Public RSA Key</param>
        /// <param name="privateKey">Private RSA Key</param>
        /// <returns>String Tuple</returns>
        public static (string, string) ExportRSAXMLKeys(RSAParameters publicKey, RSAParameters privateKey)
        {
            if (publicKey.IsNull() || privateKey.IsNull())
            {
                Debug.unityLogger.LogError("RSA", "RSAParameters public or private keys are null. Please provide both public and private keys.");
                return (null, null);
            }
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(privateKey);
            string privateKeyString = rsa.ToXmlString(true);
            rsa.ImportParameters(publicKey);
            string publicKeyString = rsa.ToXmlString(false);
            return (publicKeyString, privateKeyString);
        }

        /// <summary>
        /// Stores the RSA Public or Private Keys in the Windows Registry.<br/>
        /// </summary>
        /// <param name="key">Registry Key Name</param>
        /// <param name="value">Registry Value Name</param>
        /// <param name="rsaKey">RSA Public or Private Key</param>
        public static void StoreRSAKey_Registry(string key, string value, string rsaKey)
        {
            using RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(key);
            registryKey?.SetValue(value, rsaKey.ToBase64());
        }

        /// <summary>
        /// Stores the RSA Public or Private Keys using Player Prefs.<br/>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void StoreRSAKey_PlayerPrefs(string key, string value)
        {
            PlayerPrefs.SetString(key, value.ToBase64());
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Get the RSA Public or Private Keys from the Windows Registry.<br/>
        /// </summary>
        /// <param name="key">Registry Key Name</param>
        /// <param name="value">Registry Value Name</param>
        /// <returns>RSA Public or Private Key</returns>
        public static string GetRSAKey_Registry(string key, string value)
        {
            using RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(key);
            if (registryKey?.GetValue(value) != null)
            {
                return registryKey.GetValue(value).ToString().FromBase64();
            }
            Debug.LogError("The RSA Key is not stored in the registry.");
            return string.Empty;
        }

        /// <summary>
        /// Get the RSA Public or Private Keys from Player Prefs.<br/>
        /// </summary>
        /// <param name="keyID">Player Prefs Key ID</param>
        /// <returns></returns>
        public static string GetRSAKey_PlayerPrefs(string keyID)
        {
            if (!PlayerPrefs.HasKey(keyID))
            {
                Debug.unityLogger.LogError("Invalid Key", "The RSA Key is not stored in PlayerPrefs.");
                return string.Empty;
            }
            return PlayerPrefs.GetString(keyID).FromBase64();
        }

        /// <summary>
        /// Deletes the RSA Key from the Windows Registry.<br/>
        /// </summary>
        /// <param name="keyID">Registry Key ID</param>
        public static void DeleteRSAKey_Registry(string keyID)
        {
            using RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(keyID);
            registryKey?.DeleteValue(keyID);
        }

        /// <summary>
        /// Deletes the RSA Key from Player Prefs.<br/>
        /// </summary>
        /// <param name="keyID">Player Prefs Key ID</param>
        public static void DeleteRSAKey_PlayerPrefs(string keyID)
        {
            PlayerPrefs.DeleteKey(keyID);
        }

        #endregion

        #region HELPER METHODS

        private static bool IsAESKeyAndIVValid(byte[] key, byte[] iv)
        {
            return key != null && key.Length != 0 && iv != null && iv.Length != 0;
        }

        private static void ValidateAESKeyIVHelper(ref byte[] key, ref byte[] iv, Aes aes)
        {
            if (key.IsNull())
            {
                Debug.unityLogger.LogWarning("Key", "The AES Key is null. Generating a new AES Key.");
                key = aes.Key;
            }

            if (iv.IsNull())
            {
                Debug.unityLogger.LogWarning("IV", "The AES IV is null. Generating a new AES IV.");
                iv = aes.IV;
            }
        }

        private static void ValidateAESSettingsHelper(CipherMode mode, PaddingMode padding, Aes aes)
        {
            if (mode != CipherMode.CBC)
            {
                aes.Mode = mode;
            }
            if (padding != PaddingMode.PKCS7)
            {
                aes.Padding = padding;
            }
        }

        #endregion
    }
}