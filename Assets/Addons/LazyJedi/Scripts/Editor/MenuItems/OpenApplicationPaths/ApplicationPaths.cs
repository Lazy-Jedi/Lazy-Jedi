#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace LazyJedi.Editors.MenuItems
{
    public static class ApplicationPaths
    {
        #region METHODS

        [MenuItem("Lazy-Jedi/Open/Data Path", priority = 10)]
        public static void OpenApplicationDataPath()
        {
            EditorUtility.RevealInFinder(Application.dataPath);
        }

        [MenuItem("Lazy-Jedi/Open/Persistant Path", priority = 11)]
        public static void OpenApplicationPersistantPath()
        {
            EditorUtility.RevealInFinder(Application.persistentDataPath);
        }

        #endregion
    }
}
#endif