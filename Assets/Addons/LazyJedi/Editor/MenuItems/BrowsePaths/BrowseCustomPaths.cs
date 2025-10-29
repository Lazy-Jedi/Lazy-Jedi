#if UNITY_EDITOR
using System.IO;
using LazyJedi.Editors.Common;
using LazyJedi.Editors.Globals;
using UnityEditor;
using UnityEngine;
using LazyJedi.Utilities;

namespace LazyJedi.Editors.MenuItems
{
    public static class BrowseCustomPaths
    {
        #region METHODS

        [MenuItem("Lazy-Jedi/Open/Application Paths/Data Path", priority = 200)]
        public static void OpenApplicationDataPath()
        {
            EditorUtility.RevealInFinder(Application.dataPath);
        }

        [MenuItem("Lazy-Jedi/Open/Application Paths/Persistant Path", priority = 200)]
        public static void OpenApplicationPersistantPath()
        {
            EditorUtility.RevealInFinder(Application.persistentDataPath);
        }

        [MenuItem("Lazy-Jedi/Open/Resources Folder %#O", priority = 200)]
        public static void OpenPersonalResourcesFolder()
        {
            SystemProcessUtility.AdvancedProcess("explorer.exe", new Project().Load().ResourcesFolder.Replace("/", "\\"));
        }

        [MenuItem("Lazy-Jedi/Open/Temporary Folder %#T", priority = 200)]
        public static void OpenTemporaryFolder()
        {
            string path = new Project().Load().TempFolder.Replace("/", "\\");
            SystemProcessUtility.AdvancedProcess("explorer.exe", Path.Combine(string.IsNullOrEmpty(path) ? StringGlobals.SYS_TEMP_PATH : path));
        }

        #endregion
    }
}
#endif