using System.IO;
using UnityEditor;
using UnityEngine;

namespace LazyJedi.Editors.Globals
{
    public static class StringGlobals
    {
        #region FIELDS

        private const string CREATOR_ALIAS = "Uee";
        private const string LAZY_JEDI = "LazyJedi";

        /// <summary>
        /// Application.persistentDataPath {User}/AppData/LocalLow/{Company}/{Product}
        /// </summary>
        public static readonly string PERSISTENT_PATH = Application.persistentDataPath;

        /// <summary>
        /// Application.dataPath {Project}/Assets
        /// </summary>
        public static readonly string PROJECT_PATH = Application.dataPath;

        /// <summary>
        /// Local User Temporary Folder Path {User}/AppData/Local/Temp
        /// </summary>
        public static readonly string SYS_TEMP_PATH = Path.Combine(Path.GetTempPath(), PlayerSettings.productName);

        /// <summary>
        /// Path to the temporary folder of the project
        /// </summary>
        public static readonly string PROJ_TEMP_PATH = Path.Combine(PROJECT_PATH, "Temp~");

        #endregion

        #region PROPERTIES

        public static string PROJ_SETUP_SETTINGS_PATH
        {
            get
            {
                string settingsPath = Directory.GetParent(Directory.GetParent(Application.persistentDataPath).FullName).FullName;
                settingsPath = Path.Combine(settingsPath, CREATOR_ALIAS, LAZY_JEDI);
                if (Directory.Exists(settingsPath))
                {
                    return settingsPath;
                }

                Directory.CreateDirectory(settingsPath);
                Debug.unityLogger.Log($"Settings Path - {settingsPath} has been created!");
                return settingsPath;
            }
        }

        #endregion
    }
}