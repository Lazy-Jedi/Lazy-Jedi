#if UNITY_EDITOR
using System.IO;
using UnityEditor;

namespace LazyJedi.Editors.Internal
{
    public static class EditorSelection
    {
        #region METHODS

        public static string GetSelectedFolderPath()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (File.Exists(path)) path = Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(path) || !Directory.Exists(path)) return "Assets";
            return path;
        }

        public static string GetSelectedFilePath()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            return File.Exists(path) ? path : string.Empty;
        }

        #endregion
    }
}
#endif