#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using LazyJedi.Editors.Internal;
using UnityEditor;

namespace LazyJedi.Editors.MenuItems
{
    public static class CreateFolders
    {
        #region VARIABLES

        private static string _basePath = "Assets";

        #endregion

        #region METHODS

        [MenuItem("File/Create Project Folders #&F", priority = 150)]
        private static void CreateProjectFolders()
        {
            ProjectSetup projectSetup = new ProjectSetup();
            projectSetup.LoadSettings();

            CreateProjectFolders(projectSetup.Folders);
        }

        public static void CreateProjectFolders(List<string> folders)
        {
            foreach (string folder in folders)
            {
                string path = Path.Combine(_basePath, folder);
                if (Directory.Exists(path)) continue;

                Directory.CreateDirectory(path);
            }

            AssetDatabase.Refresh();
        }

        #endregion
    }
}
#endif