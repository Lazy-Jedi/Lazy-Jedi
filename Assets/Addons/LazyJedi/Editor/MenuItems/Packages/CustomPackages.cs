#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

namespace LazyJedi.Editors.MenuItems
{
    public static class CustomPackages
    {
        #region FIELDS

        private static AddRequest _request;

        private const string _newtonsoft = "com.unity.nuget.newtonsoft-json";

        #endregion

        #region METHODS

        [MenuItem("Lazy-Jedi/Import Packages/Newtonsoft.Json", priority = 120)]
        public static void ImportNewtonsoftJson()
        {
            _request = Client.Add(_newtonsoft);
            EditorApplication.update += Progress;
        }

        #endregion

        #region HELPER METHODS

        private static void Progress()
        {
            if (_request == null)
            {
                return;
            }

            if (_request.IsCompleted)
            {
                if (_request.Status == StatusCode.Success)
                {
                    Debug.Log("Imported package successfully");
                }
                else if (_request.Status >= StatusCode.Failure)
                {
                    Debug.LogError("Failed to import package");
                }

                EditorApplication.update -= Progress;
                _request = null;
            }
        }

        #endregion
    }
}
#endif