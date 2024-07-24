using System;
using System.Collections.Generic;
using System.IO;
using LazyJedi.Common.Extensions;
using LazyJedi.Editors.Globals;

namespace LazyJedi.Editors.Common
{
    public class Project
    {
        #region FIELDS

        private const string FILENAME = "project-setup.json";

        public string ResourcesFolder = String.Empty;
        public bool UseCustomTempFolder;
        public string TempFolder = String.Empty;
        public bool AutoSave;

        public List<string> Folders = new()
        {
            "_Project/Animations",
            "_Project/Artwork",
            "_Project/Audio/BGM",
            "_Project/Audio/SFX",
            "_Project/Prefabs/Player",
            "_Project/ScriptableObjects",
            "_Project/Scripts/Editor",
            "_Project/Scripts/Runtime",
            "_Project/UI/Fonts",
            "_Project/UI/Icons",
        };

        #endregion

        #region METHODS

        public void Save()
        {
            string json = this.ToJson(true);
            File.WriteAllText(Path.Combine(StringGlobals.PROJ_SETUP_SETTINGS_PATH, FILENAME), json);
        }

        public Project Load()
        {
            string path = Path.Combine(StringGlobals.PROJ_SETUP_SETTINGS_PATH, FILENAME);
            if (!File.Exists(path))
            {
                return this;
            }

            string json = File.ReadAllText(path);
            json.FromJsonOverwrite(this);
            return this;
        }

        #endregion
    }
}