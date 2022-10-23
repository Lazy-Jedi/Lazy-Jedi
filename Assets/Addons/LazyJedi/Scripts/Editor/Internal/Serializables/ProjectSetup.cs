using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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

        [Header("Custom Folders")]
        public string TemporaryFolder = string.Empty;

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

        private const string CREATOR_ALIAS = "Uee";
        private const string PRODUCT_NAME = "LazyJedi";
        private const string FILENAME = "project-setup.json";

        #endregion

        #region PROPERTIES

        private string SettingsPath
        {
            get
            {
                string projectPath  = Application.persistentDataPath;
                string settingsPath = Directory.GetParent(Directory.GetParent(projectPath).FullName).FullName;
                settingsPath = Path.Combine(settingsPath, CREATOR_ALIAS, PRODUCT_NAME);
                if (Directory.Exists(settingsPath)) return settingsPath;

                Directory.CreateDirectory(settingsPath);
                Debug.unityLogger.Log($"Settings Path - {settingsPath} has been created!");

                return settingsPath;
            }
        }

        #endregion

        #region METHODS

        public void SaveSettings()
        {
            string json = JsonUtility.ToJson(this, true);
            File.WriteAllText(Path.Combine(SettingsPath, FILENAME), json);
        }

        public void LoadSettings()
        {
            string path = Path.Combine(SettingsPath, FILENAME);
            if (!File.Exists(path))
            {
                SaveSettings();
                return;
            }

            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, this);
        }

        #endregion
    }
}