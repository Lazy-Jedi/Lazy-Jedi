using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

namespace LazyJedi.Editors.Internal
{
    [Serializable]
    public class ProjectSetup
    {
        #region VARIABLES

        [Header("Project Properties")]
        public string CompanyName = string.Empty;

        [Header("Resources Folder")]
        public string ResourcesFolder = string.Empty;

        [FormerlySerializedAs("ChangeTemporaryFolder")]
        [Header("Custom Folders")]
        public bool UseCustomTemporaryFolder;
        public string TemporaryFolder;

        [Header("Folders")]
        public List<string> Folders = new List<string>()
        {
            "_Project/Animations",
            "_Project/Artwork/UI",
            "_Project/Audio/SFX",
            "_Project/Audio/BGM",
            "_Project/Prefabs",
            "_Project/ScriptableObjects",
            "_Project/Scripts/Editor",
            "_Project/Scripts/Runtime",
        };

        private const string FILENAME = "project-setup.json";

        #endregion

        #region PROPERTIES

        private string SettingsPath
        {
            get
            {
                string settingsPath = LazyStrings.PERSISTANT_PARENT_PATH;
                settingsPath = Path.Combine(settingsPath, LazyStrings.CREATOR_ALIAS, LazyStrings.LAZY_JEDI);
                if (Directory.Exists(settingsPath)) return settingsPath;

                Directory.CreateDirectory(settingsPath);
                Debug.unityLogger.Log($"Settings Path - {settingsPath} has been created!");

                return settingsPath;
            }
        }

        #endregion

        #region METHODS

        public ProjectSetup SaveSettings()
        {
            string json = JsonUtility.ToJson(this, true);
            File.WriteAllText(Path.Combine(SettingsPath, FILENAME), json);
            return this;
        }

        public ProjectSetup LoadSettings()
        {
            string path = Path.Combine(SettingsPath, FILENAME);
            if (!File.Exists(path))
            {
                return SaveSettings();
            }

            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, this);
            return this;
        }

        #endregion
    }
}