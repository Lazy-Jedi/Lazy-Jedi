#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace LazyJedi.Editors.MenuItems
{
    public static class ApplicationPaths
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

        #endregion
    }
}
#endif