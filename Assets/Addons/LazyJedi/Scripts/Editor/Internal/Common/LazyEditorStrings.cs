#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace LazyJedi.Editors.Internal
{
    public static class LazyEditorStrings
    {
        #region FIELDS

        public const string CREATOR_ALIAS = "Uee";
        public const string LAZY_JEDI = "LazyJedi";

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Parent path of Application.persistentDataPath {User}/AppData/LocalLow/
        /// </summary>
        public static string PERSISTANT_PARENT_PATH => Directory.GetParent(Directory.GetParent(Application.persistentDataPath).FullName).FullName;

        /// <summary>
        /// Application.persistentDataPath {User}/AppData/LocalLow/{Company}/{Product}
        /// </summary>
        public static string PROJECT_DIRECTORY => Application.persistentDataPath;

        /// <summary>
        /// Application.dataPath {Project}/Assets
        /// </summary>
        public static string ROOT_PROJECT_DIRECTORY => Application.dataPath;

        /// <summary>
        /// Local User Temporary Folder Path {User}/AppData/Local/Temp
        /// </summary>
        public static string DEFAULT_TEMPORARY_PATH => Path.Combine(Path.GetTempPath(), PlayerSettings.productName);

        /// <summary>
        /// Path to the temporary folder of the project
        /// </summary>
        public static string PROJECT_TEMPORARY_PATH => Path.Combine(ROOT_PROJECT_DIRECTORY, "Temp~");

        #endregion
    }
}
#endif