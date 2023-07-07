using LazyJedi.Extensions;
using System.IO;
using UnityEngine;

namespace LazyJedi.IO
{
    public static class DataIO
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
        public static string Extension = "json";

        #endregion

        #region SIMPLE SAVE AND LOAD METHODS

        /// <summary>
        /// Saves the data object to the save file.
        /// </summary>
        /// <param name="data">Serializable Object - Serializable Class or Struct or ScriptableObject</param>
        /// <param name="filename">Custom Filename</param>
        /// <param name="pathType">Default Location of the Save File.</param>
        /// <param name="prettyPrint">Pretty Print the JSON data</param>
        public static void Save<T>(T data, string filename = "", PathType pathType = PathType.DefaultFolder, bool prettyPrint = false)
        {
            string path = GetFilePathHelper<T>(pathType, filename: filename);
            string parentPath = GetParentFolderHelper(pathType);
            if (!Directory.Exists(parentPath))
            {
                Directory.CreateDirectory(parentPath);
            }
            string json = data.ToJson(prettyPrint);
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Loads the data object from the save file.
        /// Use SimpleOverwrite if you are loading a ScriptableObject.
        /// </summary>
        /// <param name="filename">Custom Filename</param>
        /// <param name="pathType">Default Location of the Save File.</param>
        /// <returns>Returns the Object created from the loaded data or returns the data object instance</returns>
        public static T Load<T>(string filename = "", PathType pathType = PathType.DefaultFolder)
        {
            string path = GetFilePathHelper<T>(pathType, filename: filename);
            if (File.Exists(path))
            {
                return JsonUtility.FromJson<T>(File.ReadAllText(path));
            }
            Debug.unityLogger.LogError("Load", "File does not exist, cannot load.");
            return default;
        }

        /// <summary>
        /// Overwrites the data object with the data from the save file.
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
                Debug.unityLogger.LogError("Load And Overwrite", "File does not exist, cannot load and overwrite.");
                return;
            }
            JsonUtility.FromJsonOverwrite(File.ReadAllText(path), data);
        }

        #endregion

        #region SAVE AND LOAD SLOT METHODS

        /// <summary>
        /// Save the data object to a slot.
        /// </summary>
        /// <param name="data">Serializable Object</param>
        /// <param name="slotIndex"> This is the index of the Save Slot, this value needs to be greater than 0. </param>
        /// <param name="filename">Custom Filename</param>
        /// <param name="pathType">Default Location of the Save File.</param>
        /// <param name="prettyPrint">Pretty Print the JSON data</param>
        public static void SaveToSlot<T>(T data, int slotIndex = 1, string filename = "", PathType pathType = PathType.DefaultFolder, bool prettyPrint = false)
        {
            string path = GetFilePathHelper<T>(pathType, slotIndex, filename);
            string parentPath = GetParentFolderHelper(pathType, slotIndex);
            if (!Directory.Exists(parentPath))
            {
                Directory.CreateDirectory(parentPath);
            }
            string json = data.ToJson(prettyPrint);
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Load the data from the slot.
        /// Use LoadSlotOverwrite if you are loading a ScriptableObject.
        /// </summary>
        /// <param name="slotIndex"> This is the index of the Save Slot, this value needs to be greater than 0. </param>
        /// <param name="filename">Custom Filename</param>
        /// <param name="pathType">Default Location of the Save File.</param>
        /// <returns>Returns the Object created from the loaded data or returns the data object instance</returns>
        public static T LoadFromSlot<T>(int slotIndex = 1, string filename = "", PathType pathType = PathType.DefaultFolder)
        {
            string path = GetFilePathHelper<T>(pathType, slotIndex, filename);
            if (File.Exists(path))
            {
                return JsonUtility.FromJson<T>(File.ReadAllText(path));
            }
            Debug.unityLogger.LogError("Load", "File does not exist, cannot load from slot.");
            return default;
        }

        /// <summary>
        /// Overwrite the data object with the data loaded from the slot.
        /// Works best with ScriptableObjects.
        /// </summary>
        /// <param name="data">Serializable Object</param>
        /// <param name="slotIndex"> This is the index of the Save Slot, this value needs to be greater than 0. </param>
        /// <param name="filename">Custom Filename</param>
        /// <param name="pathType">Default Location of the Save File.</param>
        public static void LoadAndOverwriteFromSlot<T>(T data, int slotIndex = 1, string filename = "", PathType pathType = PathType.DefaultFolder)
        {
            string path = GetFilePathHelper<T>(pathType, slotIndex, filename);
            if (!File.Exists(path))
            {
                Debug.unityLogger.LogError("Load And Overwrite", "File does not exist, cannot load from slot.");
                return;
            }
            JsonUtility.FromJsonOverwrite(File.ReadAllText(path), data);
        }

        #endregion

        #region DELETE METHODS

        /// <summary>
        /// Delete the save file.
        /// </summary>
        /// <param name="filename">Filename of the Save File</param>
        /// <param name="pathType">Path of the File</param>
        public static void Delete<T>(string filename = "", PathType pathType = PathType.DefaultFolder)
        {
            string path = GetFilePathHelper<T>(pathType, filename: filename);
            if (!File.Exists(path))
            {
                Debug.unityLogger.LogError("Delete", "File does not exist, cannot delete.");
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
                Debug.unityLogger.LogError("Delete From Slot", "File does not exist, cannot delete.");
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