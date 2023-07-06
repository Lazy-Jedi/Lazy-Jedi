using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using LazyJedi.Extensions;
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
        /// <param name="mode">The Cipher Mode used for Encryption</param>
        /// <param name="padding">The Padding Mode used for Encryption</param>
        /// <returns></returns>
        public static byte[] AESEncryption(
            string data, ref byte[] key, ref byte[] iv,
            CipherMode mode = CipherMode.CBC,
            PaddingMode padding = PaddingMode.PKCS7)
        {
            byte[] encryptedData;
            using Aes aes = Aes.Create();
            
            if (mode != CipherMode.CBC)
            {
                aes.Mode = mode;
            }
            if (padding != PaddingMode.PKCS7)
            {
                aes.Padding = padding;
            }
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


            using ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);
            using MemoryStream memoryStream = new MemoryStream();
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            using StreamWriter writer = new StreamWriter(cryptoStream);
            writer.Write(data);
            encryptedData = memoryStream.ToArray();
            return encryptedData;
        }

        /// <summary>
        /// Use AES to decrypt an encrypted byte data.<br/>
        /// </summary>
        /// <param name="data">The encrypted byte data</param>
        /// <param name="key">Your AES Key</param>
        /// <param name="iv">Your AES IV</param>
        /// <param name="mode">The Cipher Mode determines how the AES Encryption will be done</param>
        /// <param name="padding">The Padding Mode used for Decryption</param>
        /// <returns>The decrypted string</returns>
        public static string AESDecryption(
            byte[] data, byte[] key, byte[] iv,
            CipherMode mode = CipherMode.CBC,
            PaddingMode padding = PaddingMode.PKCS7)
        {
            using Aes aes = Aes.Create();
            if (mode != CipherMode.CBC)
            {
                aes.Mode = mode;
            }
            if (padding != PaddingMode.PKCS7)
            {
                aes.Padding = padding;
            }
            if (!IsAESKeyAndIVValid(key, iv))
            {
                Debug.unityLogger.LogError("Invalid", "The AES Key and IV are not valid.");
                return string.Empty;
            }

            aes.Key = key;
            aes.IV = iv;

            using ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using MemoryStream msDecrypt = new MemoryStream(data);
            using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader reader = new StreamReader(csDecrypt);
            return reader.ReadToEnd();
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
        public static (RSAParameters, RSAParameters) ImportRSAKeys(byte[] publicKey, byte[] privateKey)
        {
            if (publicKey == null || privateKey == null)
            {
                Debug.unityLogger.LogError("RSA", "RSA public or private key is null. Please provide both public and private keys.");
                return default;
            }
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportRSAPublicKey(publicKey, out int @public);
            rsa.ImportRSAPrivateKey(privateKey, out int @private);
            return (rsa.ExportParameters(true), rsa.ExportParameters(false));
        }

        /// <summary>
        /// Exports the Public and Private RSAParameters as a byte[] Tuple.<br/>
        /// Key.Item1 = Public Key<br/>
        /// Key.Item2 = Private Key<br/>
        /// </summary>
        /// <param name="publicKey">Public RSA Key</param>
        /// <param name="privateKey">Private RSA Key</param>
        /// <returns>Byte[] Tuple</returns>
        public static (byte[], byte[]) ExportRSAParameters(RSAParameters publicKey, RSAParameters privateKey)
        {
            if (publicKey.IsNull() || privateKey.IsNull())
            {
                Debug.unityLogger.LogError("RSA", "RSAParameters public or private keys are null. Please provide both public and private keys.");
                return (null, null);
            }
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(privateKey);
            byte[] privateKeyBytes = rsa.ExportCspBlob(true);
            rsa.ImportParameters(publicKey);
            byte[] publicKeyBytes = rsa.ExportCspBlob(false);
            return (publicKeyBytes, privateKeyBytes);
        }

        #endregion

        #region HELPER METHODS

        private static bool IsAESKeyAndIVValid(byte[] key, byte[] iv)
        {
            return key != null && key.Length != 0 && iv != null && iv.Length != 0;
        }

        #endregion
    }
}