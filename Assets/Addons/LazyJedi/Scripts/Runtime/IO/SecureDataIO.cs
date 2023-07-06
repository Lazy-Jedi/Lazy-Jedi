using System.IO;
using System.Security.Cryptography;
using LazyJedi.Extensions;
using LazyJedi.Utility;
using UnityEngine;

namespace LazyJedi.IO
{
    public static class SecureDataIO
    {
        #region FIELDS

        /// <summary>
        /// Slot Prefix for the Save Folders
        /// </summary>
        public static string SlotPrefix = "Slot_";

        /// <summary>
        /// Default Path for the Save and Settings Files and Folders.
        /// </summary>
        public static string DefaultPath = Application.persistentDataPath;

        /// <summary>
        /// Save Path for the Save Files.
        /// </summary>
        public static string SavePath = Path.Combine(DefaultPath, "SaveData");

        /// <summary>
        /// Settings Path for the Settings Files.
        /// </summary>
        public static string SettingsPath = Path.Combine(DefaultPath, "Settings");

        /// <summary>
        /// Default Extension for the Save Files.
        /// </summary>
        public static string Extension = "bin";

        /// <summary>
        /// The Cipher Mode for the AES Encryption.
        /// CipherMode.CBC is the default.
        /// The Cipher Mode determines how the AES Encryption will be done.
        /// </summary>
        public static CipherMode CipherMode = CipherMode.CBC;

        /// <summary>
        /// The Padding Mode for the AES Encryption.
        /// PaddingMode.PKCS7 is the default.
        /// Padding is used to fill the last block of the message if it is not long enough to fill the block.
        /// </summary>
        public static PaddingMode PaddingMode = PaddingMode.PKCS7;

        private const string SAVE_NO_KEY_OR_IV_MESSAGE = "Your AES Key or IV is not valid, they will be generated automatically.\n";
        private const string LOAD_NO_KEY_OR_IV_MESSAGE = "Your AES Key and IV is not valid, you will not be able to load your save.";

        #endregion

        #region AES AND RSA METHODS

        public static void GenerateRSAKeyPair(out RSAParameters publicKey, out RSAParameters privateKey)
        {
            SecurityUtility.GenerateRSAKeyPair(out publicKey, out privateKey);
        }

        public static void GenerateAESKeyAndIV(out byte[] aesKey, out byte[] aesIV)
        {
            SecurityUtility.GenerateAESKeyAndIV(out aesKey, out aesIV);
        }

        public static byte[] EncryptAESKey(RSAParameters publicKey, byte[] aesKey, bool fOAEP = false)
        {
            return SecurityUtility.RSAEncryption(aesKey, publicKey, fOAEP);
        }

        #endregion

        #region SIMPLE SAVE AND LOAD METHODS

        /// <summary>
        /// Saves the data object to the save file.
        /// </summary>
        /// <param name="data">Serializable Class or Struct or ScriptableObject</param>
        /// <param name="filename">Custom Filename</param>
        /// <param name="pathType">Default Location of the Save File.</param>
        /// <param name="prettyPrint">Pretty Print the Json data</param>
        public static void Save<T>(
            T data,
            string filename = "",
            PathType pathType = PathType.DefaultFolder,
            bool prettyPrint = false)
        {
            string path = GetFilePathHelper<T>(pathType, filename: filename);
            string parentPath = GetParentFolderHelper(pathType);
            if (!Directory.Exists(parentPath))
            {
                Directory.CreateDirectory(parentPath);
            }
            File.WriteAllText(path, data.ToJson(prettyPrint).ToBase64());
        }

        /// <summary>
        /// Loads the data object from the save file. <br/>
        /// Use LoadAndOverwrite if you are loading a ScriptableObject.
        /// </summary>
        /// <param name="filename">Custom Filename</param>
        /// <param name="pathType">Default Location of the Save File.</param>
        /// <returns>Returns the Object created from the loaded data or returns the data object instance</returns>
        public static T Load<T>(string filename = "", PathType pathType = PathType.DefaultFolder)
        {
            string path = GetFilePathHelper<T>(pathType, filename: filename);
            return !File.Exists(path) ? default : JsonUtility.FromJson<T>(File.ReadAllText(path).FromBase64());
        }

        /// <summary>
        /// Overwrites the data object with the data from the save file. <br/>
        /// Works best with ScriptableObjects.
        /// </summary>
        /// <param name="data">Serializable Object</param>
        /// <param name="filename">Custom Filename</param>
        /// <param name="pathType">Default Location of the Save File.</param>
        public static void LoadAndOverwrite<T>(T data, string filename = "", PathType pathType = PathType.DefaultFolder)
        {
            string path = GetFilePathHelper<T>(pathType, filename: filename);
            if (!File.Exists(path))
            {
                return;
            }
            JsonUtility.FromJsonOverwrite(File.ReadAllText(path), data);
        }

        #endregion

        #region AES SAVE AND LOAD METHODS

        /// <summary>
        /// Use AES Encryption to save the data object to the save file.
        /// </summary>
        /// <param name="data">Serializable Object - Serializable Class or Struct or ScriptableObject</param>
        /// <param name="key">The AES Key</param>
        /// <param name="iv">THE AES IV</param>
        /// <param name="filename">Custom Filename</param>
        /// <param name="pathType">Default Location of the Save File.</param>
        /// <param name="prettyPrint">Pretty Print the JSON data</param>
        /// <typeparam name="T"></typeparam>
        public static void Save<T>(
            T data,
            ref byte[] key,
            ref byte[] iv,
            string filename = "",
            PathType pathType = PathType.DefaultFolder,
            bool prettyPrint = false)
        {
            if (!HasKeyAndIV(key, iv))
            {
                Debug.unityLogger.LogWarning("AES", SAVE_NO_KEY_OR_IV_MESSAGE);
                GenerateAESKeyAndIV(out key, out iv);
            }

            string path = GetFilePathHelper<T>(pathType, filename: filename);
            string parentPath = GetParentFolderHelper(pathType);
            if (!Directory.Exists(parentPath))
            {
                Directory.CreateDirectory(parentPath);
            }
            byte[] encryptedJson = SecurityUtility.AESEncryption(data.ToJson(prettyPrint), ref key, ref iv, CipherMode, PaddingMode);
            File.WriteAllBytes(path, encryptedJson);
        }
        
        /// <summary>
        /// Use AES Decryption to load the data object from the save file. <br/>
        /// Use the Overwrite methods if you are loading a ScriptableObject.
        /// </summary>
        /// <param name="filename">Custom Filename</param>
        /// <param name="key">The AES Key.</param>
        /// <param name="iv">The AES IV</param>
        /// <param name="pathType">Default Location of the Save File.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Returns the Object created from the loaded data or returns the data object instance</returns>
        public static T Load<T>(
            byte[] key,
            byte[] iv,
            string filename = "",
            PathType pathType = PathType.DefaultFolder)
        {
            if (!HasKeyAndIV(key, iv))
            {
                Debug.unityLogger.LogError("Invalid", LOAD_NO_KEY_OR_IV_MESSAGE);
                return default;
            }
            string path = GetFilePathHelper<T>(pathType, filename: filename);
            return !File.Exists(path)
                ? default
                : JsonUtility.FromJson<T>(SecurityUtility.AESDecryption(File.ReadAllBytes(path), key, iv, CipherMode, PaddingMode));
        }

        /// <summary>
        /// Use AES Decryption to overwrite the data object with the data from the save file. <br/>
        /// Works best with ScriptableObjects.
        /// </summary>
        /// <param name="data">Serializable Object</param>
        /// <param name="key">The AES Key</param>
        /// <param name="iv">The AES IV</param>
        /// <param name="filename">Custom Filename</param>
        /// <param name="pathType">Default Location of the Save File.</param>
        /// <typeparam name="T"></typeparam>
        public static void LoadAndOverwrite<T>(
            T data,
            byte[] key,
            byte[] iv,
            string filename = "",
            PathType pathType = PathType.DefaultFolder)
        {
            string path = GetFilePathHelper<T>(pathType, filename: filename);
            if (!File.Exists(path))
            {
                return;
            }
            JsonUtility.FromJsonOverwrite(SecurityUtility.AESDecryption(File.ReadAllBytes(path), key, iv), data);
        }

        #endregion

        #region SAVE AND LOAD SLOT METHODS

        /// <summary>
        /// Use AES Encryption to save the data object to a save slot. <br/>
        /// </summary>
        /// <param name="data">Serializable Object</param>
        /// <param name="key">The AES Key</param>
        /// <param name="iv">The AES IV</param>
        /// <param name="slotIndex"> This is the index of the Save Slot, this value needs to be greater than 0. </param>
        /// <param name="filename">Custom Filename</param>
        /// <param name="pathType">Default Location of the Save File.</param>
        /// <param name="prettyPrint">Pretty Print the JSON data</param>
        public static void SaveToSlot<T>(
            T data,
            ref byte[] key,
            ref byte[] iv,
            int slotIndex = 1,
            string filename = "",
            PathType pathType = PathType.DefaultFolder,
            bool prettyPrint = false)
        {
            if (!HasKeyAndIV(key, iv))
            {
                Debug.unityLogger.LogError("Invalid", SAVE_NO_KEY_OR_IV_MESSAGE);
            }

            string path = GetFilePathHelper<T>(pathType, slotIndex, filename);
            string parentPath = GetParentFolderHelper(pathType, slotIndex);
            if (!Directory.Exists(parentPath))
            {
                Directory.CreateDirectory(parentPath);
            }
            byte[] jsonBytes = SecurityUtility.AESEncryption(data.ToJson(prettyPrint), ref key, ref iv, CipherMode, PaddingMode);
            File.WriteAllBytes(path, jsonBytes);
        }

        /// <summary>
        /// Use AES Decryption to load the data object from the save slot. <br/>
        /// Use the Overwrite method if you are loading a ScriptableObject.
        /// </summary>
        /// <param name="key">The AES Key</param>
        /// <param name="iv">The AES IV</param>
        /// <param name="slotIndex"> This is the index of the Save Slot, this value needs to be greater than 0. </param>
        /// <param name="filename">Custom Filename</param>
        /// <param name="pathType">Default Location of the Save File.</param>
        /// <returns>Returns the Object created from the loaded data or returns the data object instance</returns>
        public static T LoadFromSlot<T>(
            byte[] key,
            byte[] iv,
            int slotIndex = 1,
            string filename = "",
            PathType pathType = PathType.DefaultFolder)
        {
            if (!HasKeyAndIV(key, iv))
            {
                Debug.unityLogger.LogError("Invalid", LOAD_NO_KEY_OR_IV_MESSAGE);
                return default;
            }

            string path = GetFilePathHelper<T>(pathType, slotIndex, filename);
            return !File.Exists(path)
                ? default
                : JsonUtility.FromJson<T>(SecurityUtility.AESDecryption(File.ReadAllBytes(path), key, iv, CipherMode, PaddingMode));
        }

        /// <summary>
        /// Use AES Decryption to overwrite the data object with the data from the save slot. <br/>
        /// Works best with ScriptableObjects.
        /// </summary>
        /// <param name="data">Serializable Object</param>
        /// <param name="key">The AES Key</param>
        /// <param name="iv">The AES IV</param>
        /// <param name="slotIndex"> This is the index of the Save Slot, this value needs to be greater than 0. </param>
        /// <param name="filename">Custom Filename</param>
        /// <param name="pathType">Location of the Save File.</param>
        public static void LoadFromSlotAndOverwrite<T>(
            T data,
            byte[] key,
            byte[] iv,
            int slotIndex = 1,
            string filename = "",
            PathType pathType = PathType.DefaultFolder)
        {
            if (!HasKeyAndIV(key, iv))
            {
                Debug.unityLogger.LogError("Invalid", LOAD_NO_KEY_OR_IV_MESSAGE);
                return;
            }
            string path = GetFilePathHelper<T>(pathType, slotIndex, filename);
            if (!File.Exists(path))
            {
                return;
            }
            JsonUtility.FromJsonOverwrite(SecurityUtility.AESDecryption(File.ReadAllBytes(path), key, iv), data);
        }

        #endregion

        #region DELETE METHODS

        /// <summary>
        /// Deletes the save file.
        /// </summary>
        /// <param name="filename">Custom Filename</param>
        /// <param name="pathType">Default Location of the Save File.</param>
        public static void Delete<T>(string filename = "", PathType pathType = PathType.DefaultFolder)
        {
            string path = GetFilePathHelper<T>(pathType, filename: filename);
            if (!File.Exists(path))
            {
                return;
            }
            File.Delete(path);
        }

        /// <summary>
        /// Delete the save file from the slot.
        /// </summary>
        /// <param name="slotIndex">Index of the Save File</param>
        /// <param name="filename">Filename of the Save File</param>
        /// <param name="pathType">Path of the File</param>
        public static void DeleteFromSlot<T>(int slotIndex = 1, string filename = "", PathType pathType = PathType.DefaultFolder)
        {
            string path = GetFilePathHelper<T>(pathType, slotIndex, filename);
            if (!File.Exists(path))
            {
                return;
            }
            File.Delete(path);
        }

        #endregion

        #region HELPER METHODS

        private static string GetPathHelper(PathType pathType, int slotIndex = 0)
        {
            return pathType switch
            {
                PathType.SaveFolder => Path.Combine(SavePath, slotIndex <= 0 ? "" : $"{SlotPrefix}{slotIndex}"),
                PathType.OptionsFolder => Path.Combine(SettingsPath, slotIndex <= 0 ? "" : $"{SlotPrefix}{slotIndex}"),
                _ => Path.Combine(DefaultPath, slotIndex <= 0 ? "" : $"{SlotPrefix}{slotIndex}")
            };
        }

        private static string GetParentFolderHelper(PathType pathType, int slotIndex = 0)
        {
            return GetPathHelper(pathType, slotIndex);
        }

        private static string GetFilePathHelper<T>(PathType pathType, int slotIndex = 0, string filename = "")
        {
            return Path.Combine(GetPathHelper(pathType, slotIndex), $"{(filename.IsNullOrEmpty() ? typeof(T).Name : filename)}.{Extension}");
        }

        private static bool HasKeyAndIV(byte[] key, byte[] iv)
        {
            return key != null && key.Length != 0 && iv != null && iv.Length != 0;
        }

        #endregion

        #region DEBUG METHODS

        public static void PrintAllPaths()
        {
            Debug.unityLogger.Log($"Default Path: {DefaultPath}");
            Debug.unityLogger.Log($"Save Path: {SavePath}");
            Debug.unityLogger.Log($"Options Path: {SettingsPath}");
        }

        #endregion
    }
}