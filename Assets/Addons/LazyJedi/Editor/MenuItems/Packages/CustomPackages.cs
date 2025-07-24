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

        private const string _newtonsoftPackage = "com.unity.nuget.newtonsoft-json";
        private const string _nuGetUrl = "https://github.com/GlitchEnzo/NuGetForUnity";
        private const string _nuGetPackage = "https://github.com/GlitchEnzo/NuGetForUnity.git?path=/src/NuGetForUnity";
        private const string _uniTaskUrl = "https://github.com/Cysharp/UniTask";
        private const string _uniTaskPackage = "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask";
        private const string zLinqURL = "https://github.com/Cysharp/ZLinq?tab=readme-ov-file#unity";
        private const string _zLinqPackage = "https://github.com/Cysharp/ZLinq.git?path=src/ZLinq.Unity/Assets/ZLinq.Unity";

        #endregion

        #region METHODS

        [MenuItem("Lazy-Jedi/Import Packages/Newtonsoft.Json", priority = 120)]
        public static void ImportNewtonsoftJson()
        {
            _request = Client.Add(_newtonsoftPackage);
            EditorApplication.update += Progress;
        }
        
        [MenuItem("Lazy-Jedi/Import Packages/NugetForUnity", priority = 121)]
        public static void ImportNuGet()
        {
            _request = Client.Add(_nuGetPackage);
            EditorApplication.update += Progress;
            Application.OpenURL(_nuGetUrl);
        }
        
        [MenuItem("Lazy-Jedi/Import Packages/UniTask", priority = 122)]
        public static void ImportUniTask()
        {
            _request = Client.Add(_uniTaskPackage);
            EditorApplication.update += Progress;
            Application.OpenURL(_uniTaskUrl);
        }
        
        [MenuItem("Lazy-Jedi/Import Packages/ZLinq", priority = 123)]
        public static void ImportZLinq()
        {
            _request = Client.Add(_zLinqPackage);
            EditorApplication.update += Progress;
            Application.OpenURL(zLinqURL);
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