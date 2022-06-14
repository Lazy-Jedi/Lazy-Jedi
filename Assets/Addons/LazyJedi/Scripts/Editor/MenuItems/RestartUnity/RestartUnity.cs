#if UNITY_EDITOR

using System.IO;
using UnityEditor;

namespace LazyJedi.Editors.MenuItems
{
    public static class RestartUnity
    {
        #region METHODS

        [MenuItem("File/Restart Editor #r", priority = 190)]
        public static void ReopenProject()
        {
            if (EditorUtility.DisplayDialog("Restart Editor", "Do you want to Restart the Editor?", "Yes", "Cancel"))
            {
                EditorApplication.OpenProject(Directory.GetCurrentDirectory());
            }
        }

        #endregion
    }
}
#endif