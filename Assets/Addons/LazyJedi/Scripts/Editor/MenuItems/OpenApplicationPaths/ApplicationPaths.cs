#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace LazyJedi.Editors.MenuItems
{
    public static class ApplicationPaths
    {
        #region METHODS

        [MenuItem("System/Paths/Data Path", priority = 0)]
        public static void OpenApplicationDataPath()
        {
            EditorUtility.RevealInFinder(Application.dataPath);
        }

        [MenuItem("System/Paths/Persistant Path", priority = 1)]
        public static void OpenApplicationPersistantPath()
        {
            EditorUtility.RevealInFinder(Application.persistentDataPath);
        }

        #endregion
    }
}
#endif