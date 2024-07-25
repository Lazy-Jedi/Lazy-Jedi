#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using LazyJedi.Editors.Common;
using UnityEditor;
using UnityEngine;

namespace LazyJedi.Editors.MenuItems
{
    public static class CreateThings
    {
        #region METHODS

        [MenuItem("Lazy-Jedi/Create/Folders", priority = 120)]
        public static void CreateProjectFolders()
        {
            List<string> folders = new Project().Load().Folders;
            string basePath = Application.dataPath;
            foreach (string folderPath in folders)
            {
                string fullPath = Path.Combine(basePath, folderPath);
                if (Directory.Exists(fullPath))
                {
                    continue;
                }

                Directory.CreateDirectory(fullPath);
            }

            AssetDatabase.Refresh();
        }

        #endregion
    }
}
#endif