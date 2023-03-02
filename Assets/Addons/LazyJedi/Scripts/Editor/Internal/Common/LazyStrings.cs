using System.IO;
using UnityEditor;
using UnityEngine;

namespace LazyJedi.Editors.Internal
{
    public static class LazyStrings
    {
        #region FIELDS

        public const string CREATOR_ALIAS = "Uee";
        public const string LAZY_JEDI = "LazyJedi";

        #endregion

        #region PROPERTIES

        public static string PROJECT_PARENT_DIRECTORY
        {
            get => Directory.GetParent(Directory.GetParent(Application.persistentDataPath).FullName).FullName;
        }

        public static string PROJECT_DIRECTORY
        {
            get => Application.persistentDataPath;
        }

        public static string DEFAULT_TEMPORARY_PATH => Path.Combine(Path.GetTempPath(), PlayerSettings.productName);

        #endregion
    }
}