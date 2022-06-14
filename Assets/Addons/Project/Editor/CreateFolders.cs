#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Directory = UnityEngine.Windows.Directory;

namespace Uee.ProjectSetup
{
    public static class CreateFolders
    {
        #region VARIABLES

        private static string _basePath = "Assets";
        
        // Edit this list by adding your own folders that should be created.
        private static List<string> _folders = new List<string>
        {
            "_Projects/Animations",
            "_Projects/Artwork/Materials",
            "_Projects/Artwork/UI",
            "_Projects/Artwork/Icons",
            "_Projects/Artwork/Fonts",
            "_Projects/Audio/SFX",
            "_Projects/Audio/BGM",
            "_Projects/Prefabs",
            "_Projects/Scenes",
            "_Projects/ScriptableObjects",
            "_Projects/Scripts/Runtime",
            "_Projects/Scripts/Editor",
        };
        
        #endregion

        #region METHODS

        [MenuItem("File/Create Project Folders", priority = 150)]
        public static void CreateProjectFolders()
        {
            foreach (string folder in _folders)
            {
                string path = Path.Combine(_basePath, folder);
                if (Directory.Exists(path)) continue;
                Directory.CreateDirectory(path);
                Debug.Log($"Created Folder - {path}");
            }
            AssetDatabase.Refresh();
        }

        #endregion
    }
}
#endif