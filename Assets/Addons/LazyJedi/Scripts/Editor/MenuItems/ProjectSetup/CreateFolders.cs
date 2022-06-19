#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace LazyJedi.Editors.MenuItems
{
    public static class CreateFolders
    {
        #region VARIABLES

        private static string _basePath = "Assets";

        #endregion

        public static List<string> Folders = new List<string>()
        {
            "_Projects/",
            "_Projects/Artwork",
            "_Projects/Scripts",
            "_Projects/Prefabs",
            "_Projects/Scripts/Runtime",
            "_Projects/Scripts/Editor",
            "_Projects/Audio",
            "_Projects/Audio/SFX",
            "_Projects/Audio/BGM",
            "_Projects/ScriptableObjects",
        };

        #region METHODS

        [MenuItem("File/Create Project Folders #&F", priority = 150)]
        private static void CreateProjectFolders()
        {
            CreateProjectFolders(Folders);
        }

        public static void CreateProjectFolders(List<string> folders, bool log = true)
        {
            foreach (string folder in folders)
            {
                string path = Path.Combine(_basePath, folder);
                if (Directory.Exists(path)) continue;

                Directory.CreateDirectory(path);
                if (log) Debug.Log($"Created Folder - {folder}");
            }

            AssetDatabase.Refresh();
        }

        #endregion
    }
}
#endif